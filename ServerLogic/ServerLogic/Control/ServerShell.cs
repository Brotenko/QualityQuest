using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using ServerLogic.Properties;

namespace ServerLogic.Control
{
    /// <summary>
    /// Main class to launch, control and monitor the server.
    /// </summary>
    public class ServerShell
    {
        //public Logger logger = [...];
        private readonly MainServerLogic _mainServerLogic = new MainServerLogic();
        private bool _serverIsRunning = false;
        private bool _commandRequestsHelpMessage = false;
        private static bool _isDebug;


        /// <summary>
        /// This is a direct copy from the Password-setter:
        /// 
        /// Makes sure to set a password that doesn't start with a dash ("-"), 
        /// is at least 8 characters in length, but no more than 32, and satisfies 
        /// 3 out of the following 4 rules:
        ///  At least one digit
        ///  At least one lowercase character
        ///  At least one uppercase character
        ///  At least one special character
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        private string CheckPasswordConditions(string password)
        {
            if (password.Length >= 8 && password.Length <= 32 && !password.StartsWith('-'))
            {
                int count = 0;

                if (Regex.IsMatch(password, @".*\d.*"))
                {
                    count++;
                }
                if (Regex.IsMatch(password, @".*[a-z].*"))
                {
                    count++;
                }
                if (Regex.IsMatch(password, @".*[A-Z].*"))
                {
                    count++;
                }
                if (Regex.IsMatch(password, @".*[^a-zA-Z0-9 ].*"))
                {
                    count++;
                }

                if (count < 3)
                {
                    throw new ArgumentException(message: Properties.Resources.InvalidPasswordExceptionMessage);
                }
            }
            else
            {
                throw new ArgumentException(message: Properties.Resources.InvalidPasswordExceptionMessage);
            }

            return password;
        }

        /// <summary>
        /// Adds a salt from the settings-file to the passed string and returns the SHA256 hash from it.
        /// </summary>
        /// <param name="hashMe"></param>
        /// <returns>A SHA256-Hash</returns>
        public static string StringToSHA256Hash(string hashMe)
        {
            hashMe += Settings.Default.Salt;
            //source: https://stackoverflow.com/questions/16999361/obtain-sha-256-string-of-a-string
            StringBuilder stringBuilder = new();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(hashMe));
                foreach (Byte b in result)
                {
                    stringBuilder.Append(b.ToString("x2"));
                }
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Generates a random string with length 16. May be used to salt a password before hashing.
        /// </summary>
        /// <returns>A string of length 16.</returns>
        private static string SaltGen()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            var rand = new Random();
            string salt = new(Enumerable.Repeat(chars, 16).Select(s => s[rand.Next(s.Length)]).ToArray());
            return salt;
        }

        /// <summary>
        /// A basic method for secure password retrieval. The requested password is stored as a hash in the settings file.
        /// </summary>
        private void SetPasswordDialog()
        {
            string backupSalt = Settings.Default.Salt;
            string backupPW = Settings.Default.PWHash;
            Console.WriteLine("Please enter your password:");
            string password = Console.ReadLine();
            try
            {
                //FR37, new PW, new Salt
                Settings.Default.Salt = SaltGen();
                Settings.Default.PWHash = StringToSHA256Hash(CheckPasswordConditions(password));

            }
            catch (ArgumentException)
            {
                Console.WriteLine("\n" + Resources.InvalidPasswordExceptionMessage);
                Settings.Default.Salt = backupSalt;
                Settings.Default.PWHash = backupPW;
                return;
            }
            Console.WriteLine("Password was changed successfully.");
            Settings.Default.Save();
        }

        /// <summary>
        /// Constructs a new ServerShell with a predefined password and the standard port.
        /// </summary>
        public ServerShell()
        {
            if (Settings.Default.PWHash.Equals(""))
            {
                Console.WriteLine("Currently no password is specified.");
                SetPasswordDialog();
            }
            RunShell();
        }

        /// <summary>
        /// Constructs a new debug version of ServerShell with a predefined password and 
        /// the standard port.
        /// </summary>
        public static ServerShell DebugServerShell()
        {
            _isDebug = true;
            return new ServerShell();
        }


        /// <summary>
        /// The Main method of the ServerShell.
        /// </summary>
        /// 
        /// <param name="args">Command-line parameters.</param>
        /// 
        /// <exception cref="ArgumentException">Thrown when either the supposed 
        /// password or port violate the rules for setting them.</exception>
        public static void Main(string[] args)
        {
            new ServerShell();
        }

        /// <summary>
        /// A debug function that is only used by the unit-tests to test the ServerShell
        /// and its methods thoroughly.
        /// </summary>
        /// 
        /// <param name="debugInput">Unaltered input sent from the unit-test.</param>
        /// 
        /// <returns>The output of the parsed command.</returns>
        public string ParseCommandDebugger(string debugInput)
        {
            return ParseCommand(debugInput);
        }

        /// <summary>
        /// The main-loop of the shell application that prompts new inputs and returns the
        /// output of the parsed command.
        /// </summary>
        private void RunShell()
        {
            Console.WriteLine(Properties.Resources.StartupMessage);

            if (!_isDebug)
            {
                while (true)
                {
                    Console.Write(value: "qq >> ");
                    string command = Console.ReadLine();

                    Console.WriteLine(ParseCommand(command));
                }
            }
        }

        /// <summary>
        /// Exits the main-loop and closes the shell application.
        /// </summary>
        private void StopShell()
        {
            Environment.Exit(exitCode: 0);
        }

        /// <summary>
        /// This function serves the following purposes:
        /// <list type="bullet">
        /// <item>Removes excess whitespaces from the input.</item>
        /// <item>Trims the input.</item>
        /// <item>Splits the input into an array at the whitespaces.</item>
        /// <item>Extracts the first array entry, e.g. the actual command, transform it to 
        /// lowercase, and save it as a separate string.</item>
        /// <item>Check if the user requested help regarding the command and return the
        /// help-text regarding the command.</item>
        /// <item>If no help was requested, the command is parsed to check which function
        /// should be executed next</item>
        /// <item>Return the outcome of the function.</item>
        /// </list>
        /// </summary>
        /// 
        /// <param name="input">Unaltered input from the user.</param>
        /// 
        /// <returns>The output of the other functions called by ParseCommand. Alternatively
        /// it might also return an error message if the input command is unknown.</returns>
        private string ParseCommand(string input)
        {
            input = Regex.Replace(input, @"\s+", " "); // Remove repeating whitespaces.
            input = input.Trim();
            string[] splitInput = input.Split(' ');
            string command = splitInput[0].ToLower(CultureInfo.CurrentCulture);
            string[] commandParameters = new string[splitInput.Length - 1];
            string ret = "";

            // Checks if the entered command has at least one parameter. In case there is at
            // least one, it checks if that first parameter is "--help" specifically, and 
            // returns the help-text for the specific command.
            if (commandParameters.Length != 0)
            {
                Array.Copy(splitInput, 1, commandParameters, 0, commandParameters.Length);

                if (commandParameters[0] == "--help")
                {
                    _commandRequestsHelpMessage = true;
                    ret = ShowHelp(command);
                }
            }

            if (!_commandRequestsHelpMessage)
            {
                switch (command)
                {
                    case "password":
                        SetPasswordDialog();
                        break;
                    case "start":
                        ret = StartServer(commandParameters);
                        break;
                    case "stop":
                        ret = StopServer();
                        break;
                    case "version":
                        ret = ShowVersion();
                        break;
                    case "help":
                        ret = ShowHelp("help");
                        break;
                    case "log":
                        ret = ShowLogs(commandParameters);
                        break;
                    case "sess":
                        ret = _mainServerLogic.ActiveConnections;
                        break;
                    case "exit":
                        StopShell();
                        break;
                    default:
                        ret = "'" + command + "' is not a valid command. See 'help'.";
                        break;
                }
            }

            _commandRequestsHelpMessage = false;
            return ret;
        }

        /// <summary>
        /// This function serves the following purposes:
        /// <list type="bullet">
        /// <item>Start the server with the currently set port and password.</item>
        /// <item>Start the server with the currently set port and a new password.</item>
        /// <item>Start the server with the currently set password and a new port.</item>
        /// <item>Start the server with a new port and password.</item>
        /// </list>
        /// </summary>
        /// 
        /// <param name="parameterList">List of all parameters the command has been called 
        /// with</param>
        /// 
        /// <returns>Confirmation that the server has been started and with which port it 
        /// has been started, if no errors occurred.</returns>
        private string StartServer(string[] parameterList)
        {
            if (!_serverIsRunning)
            {
                try
                {
                    if (!_isDebug)
                    {
                        _mainServerLogic.Start();
                    }
                }
                catch (Exception e)
                {
                    return "The server could not be started due to following Exception: \n"
                        + e.StackTrace;
                }
                _serverIsRunning = true;
                return "The server has been started successfully with port: " + Settings.Default.PAWebPagePort;
            }

            return "The server is already running with port: " + Settings.Default.PAWebPagePort;
        }

        /// <summary>
        /// This function stops the server and thus terminates any kind of communication
        /// with the Moderator-Client or any PlayerAudience-Client.
        /// </summary>
        /// 
        /// <returns>Confirmation hat the server has been stopped successfully.</returns>
        private string StopServer()
        {
            try
            {
                _mainServerLogic.Stop();
                _serverIsRunning = false;
                return "The server has been shut down successfully.";
            }
            catch (InvalidOperationException e)
            {
                _serverIsRunning = false;
                return e.ToString();
            }
        }

        /// <summary>
        /// This function loads and returns the help-text for the specified command, or
        /// returns an error message that the specified command is unknown.
        /// </summary>
        /// 
        /// <param name="command">The command that the user needs help with.</param>
        /// 
        /// <returns>The help-text for the specified command if no error occurred.</returns>
        private string ShowHelp(string command)
        {
            string ret;
            switch (command)
            {
                case "port":
                    ret = Properties.Resources.PortHelpMessage;
                    break;
                case "password":
                    ret = Properties.Resources.PasswordHelpMessage;
                    break;
                case "start":
                    ret = Properties.Resources.StartHelpMessage;
                    break;
                case "stop":
                    ret = Properties.Resources.StopHelpMessage;
                    break;
                case "version":
                    ret = Properties.Resources.VersionHelpMessage;
                    break;
                case "help":
                    ret = Properties.Resources.HelpHelpMessage;
                    break;
                case "log":
                    ret = Properties.Resources.LogHelpMessage;
                    break;
                case "sess":
                    ret = Properties.Resources.SessHelpMessage;
                    break;
                case "exit":
                    ret = Properties.Resources.ExitHelpMessage;
                    break;
                default:
                    return "'" + command + "' is not a valid command. See 'help'.";
            }

            return ret;
        }

        /// <summary>
        /// This function returns the current version of the ServerLogic.
        /// </summary>
        /// 
        /// <returns>The current version of the ServerLogic.</returns>
        private string ShowVersion()
        {
            return Properties.Resources.CurrentVersion;
        }

        /// <summary>
        /// Parses the parameters the "log" command was called with.
        /// Depending on the type of argument, the following services are provided.
        /// <list type="bullet">
        /// <item>
        /// <term>NULL</term>
        /// <description>Shows the contents of the log file.</description>
        /// </item>
        /// <item>
        /// <term>--clear</term>
        /// <description>Deletes the log file.</description>
        /// </item>
        /// <item>
        /// <term>--setLevel [0,1,2,3,4]</term>
        /// <description>Sets the logging level according to the additional parameter.<See cref="Properties.Resources.LogHelpMessage"/></description>
        /// </item>
        /// <item>
        /// <term>--getLevel</term>
        /// <description>Shows the current logging level.</description>
        /// </item>
        /// <item>
        /// <term>--setLogOutput [0,1,2]</term>
        /// <description>0 sets LogOutput to File, 1 to Console, and 2 to both.</description>
        /// </item>
        /// <item>
        /// <term>--help</term>
        /// <description>Shows the help-text for the Logger.</description>
        /// </item>
        /// </list> 
        /// </summary>
        /// <param name="parameterList"></param>
        /// <returns></returns>
        private string ShowLogs(string[] parameterList)
        {
            if (parameterList.Length == 0)
            {
                return ServerLogger.LogFileToString();
            }
            else
            {
                switch (parameterList[0])
                {
                    case "--clear":
                        ServerLogger.WipeLogFile();
                        return "Logs were cleared.";
                    case "--setLevel" or "--sl":
                        try
                        {
                            ServerLogger.SetLogLevel(short.Parse(parameterList[1]));
                        }
                        catch (System.FormatException)
                        {
                            ServerLogger.LogDebug(
                                "Caught Format Exception which occurred by using: log --setLevel " + parameterList[1] +
                                "\nCurrent LogLevel is " + Settings.Default.LogLevel + ".");
                            return Resources.InvalidLogLevelMessage;
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            ServerLogger.LogDebug(
                                "Caught IndexOutOfRange Exception, caused by using 'log --setLevel' without a parameter.");
                            return Resources.InvalidLogLevelMessage;
                        }
                        catch (System.OverflowException)
                        {
                            ServerLogger.LogDebug(
                                "Caught Overflow Exception, caused by using 'log --setLevel' with a parameter to small or to big for short.");
                            return Resources.InvalidLogLevelMessage;
                        }

                        //return should be empty in case of wrong Input, which is handled inside ServerLogger class.
                        return "";
                    case "--setLogOutput" or "--slo":
                        try
                        {
                            ServerLogger.ChangeLoggingOutputType(short.Parse(parameterList[1]));
                        }
                        catch (System.FormatException)
                        {
                            ServerLogger.LogDebug("Caught Format-Exception which occurred by using: log --setLogOutput " + parameterList[1] + "\nCurrent LogOutputType is " + Settings.Default.LogOutPutType + ".");
                            return Resources.InvalidLoggingOutputType;
                        }
                        catch (System.IndexOutOfRangeException)
                        {
                            ServerLogger.LogDebug("Caught IndexOutOfRange-Exception, caused by using 'log --setLogOutput' without a parameter.");
                            return Resources.InvalidLoggingOutputType;
                        }
                        catch (System.OverflowException)
                        {
                            ServerLogger.LogDebug(
                                "Caught Overflow Exception, caused by using 'log --setLogOutput' with a parameter to small or to big for short.");
                            return Resources.InvalidLogLevelMessage;
                        }

                        //return should be empty in case of wrong Input, which is handled inside ServerLogger class.
                        return "";
                    case "--getLevel":
                        return "Current LogLevel is " + Settings.Default.LogLevel + ".";
                    default:
                        return "Command " + parameterList[0] + " is unknown, use 'log --help' for more information";
                }
            }
        }
    }
}