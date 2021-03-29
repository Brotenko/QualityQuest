using System;
using System.Globalization;
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
        private MainServerLogic mainServerLogic = new MainServerLogic();
        private int _port;
        private string _password; //Passwort des Servers 
        private bool serverIsRunning = false;
        private bool commandRequestsHelpMessage = false;
        private static bool isDebug;

       // private MainServerLogic moderatorClientManager;

        public int Port
        {
            get => _port;
            private set
            {
                // Makes sure the port is not outside the range of settable ports.
                if (value <= 1023 || value > 65535)
                {
                    throw new ArgumentException(message: Properties.Resources.InvalidPortExceptionMessage);
                }

                _port = value;
            }
        }

        public string Password
        {
            get => _password;
            private set
            {
                // Makes sure to set a password that doesn't start with a dash ("-"), 
                // is at least 8 characters in length, but no more than 32, and satisfies 
                // 3 out of the following 4 rules:
                //  At least one digit
                //  At least one lowercase character
                //  At least one uppercase character
                //  At least one special character
                if (value.Length >= 8 && value.Length <= 32 && !value.StartsWith('-'))
                {
                    int count = 0;

                    if (Regex.IsMatch(value, @".*\d.*"))
                    {
                        count++;
                    }
                    if (Regex.IsMatch(value, @".*[a-z].*"))
                    {
                        count++;
                    }
                    if (Regex.IsMatch(value, @".*[A-Z].*"))
                    {
                        count++;
                    }
                    if (Regex.IsMatch(value, @".*[^a-zA-Z0-9 ].*"))
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

                _password = value;
            }
        }

        /// <summary>
        /// Constructs a new ServerShell with a predefined password and the standard port.
        /// </summary>
        public ServerShell()
        {
            this.Password = "!Password123";
            this.Port = 7777;
            ServerLogger.CreateServerLogger();
        }

        /// <summary>
        /// Constructs a new debug version of ServerShell with a predefined password and 
        /// the standard port.
        /// </summary>
        public static ServerShell DebugServerShell()
        {
            isDebug = true;
            return new ServerShell();
        }

        /// <summary>
        /// Constructs a new ServerShell with a password and a port and starts the main-loop
        /// that runs the actual shell application.
        /// </summary>
        /// 
        /// <param name="password">The password, that will be required by the Moderator-Client, 
        /// to establish a connection with the ServerLogic.</param>
        /// 
        /// <param name="port">The port that will be used to set up the WebSocket of the 
        /// ServerLogic.</param>
        public ServerShell(string password, int port)
        {
            this.Password = password;
            this.Port = port;
            ServerLogger.CreateServerLogger();

            RunShell();
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
            string[] returnValue = CheckMainMethodArgs(args);

            if (returnValue == null)
            {
                return;
            }

            _ = new ServerShell(password: returnValue[0], port: Convert.ToInt32(returnValue[1], CultureInfo.CurrentCulture));
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

            while (true)
            {
                Console.Write(value: "qq >> ");
                string command = Console.ReadLine();

                Console.WriteLine(ParseCommand(command));
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
                    commandRequestsHelpMessage = true;
                    ret = ShowHelp(command);
                }
            }

            if (!commandRequestsHelpMessage)
            {
                switch (command)
                {
                case "port":
                    ret = ParsePort(commandParameters);
                    break;
                case "password":
                    ret = ParsePassword(commandParameters);
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
                case "exit":
                    StopShell();
                    break;
                default:
                    ret = "'" + command + "' is not a valid command. See 'help'.";
                    break;
                }
            }

            commandRequestsHelpMessage = false;
            return ret;
        }

        /// <summary>
        /// Parses the parameters the "port" command was called with. Depending on the
        /// number of arguments and type of argument, the following services are provided.
        /// <list type="bullet">
        /// <item>
        /// <term>
        /// Empty parameter list
        /// </term>
        /// <description>
        /// Returns the currently set port.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// Numeric first parameter inside settable range
        /// </term>
        /// <description>
        /// Sets the port equal to the numeric value and returns a confirmation message.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// Numeric first parameter inside settable range
        /// </term>
        /// <description>
        /// Returns an error message in form of a 
        /// <see cref="Properties.Resources.InvalidPortExceptionMessage"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// Non-numeric first parameter
        /// </term>
        /// <description>
        /// Returns an error message in form of a 
        /// <see cref="Properties.Resources.InvalidPortExceptionMessage"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// Server is running
        /// </term>
        /// <description>
        /// While the server is running, the port can't be changed.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// Parameter 2...n
        /// </term>
        /// <description>
        /// Any kind of parameter, after the first one, will be ignored.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// 
        /// <param name="parameterList">List of all parameters the command has been called 
        /// with.</param>
        /// 
        /// <returns>Which port is currently set or has been set, if no errors occurred.
        /// </returns>
        private string ParsePort(string[] parameterList)
        {
            // command: port -> Returns the currently set port.
            if (parameterList.Length == 0)
            {
                return Port.ToString(CultureInfo.CurrentCulture);
            }
            // command: port number -> Sets a new port.
            else
            {
                if (serverIsRunning)
                {
                    return "The server is running. Switching ports is disabled.";
                }
                else
                {
                    int tempPort;

                    // port-input is not a numeral.
                    try
                    {
                        tempPort = Convert.ToInt32(parameterList[0], CultureInfo.CurrentCulture);
                    }
                    catch (FormatException)
                    {
                        return Properties.Resources.InvalidPortExceptionMessage;
                    }
                    catch (OverflowException)
                    {
                        return Properties.Resources.InvalidPortExceptionMessage;
                    }

                    // test for port number being inside the settable range.
                    try
                    {
                        Port = tempPort;
                    }
                    // port number outside the range.
                    catch (ArgumentException)
                    {
                        return Properties.Resources.InvalidPortExceptionMessage;
                    }

                    return "The port has been set to " + tempPort + " successfully.";
                }
            }
        }

        /// <summary>
        /// Parses the parameters the "password" command was called with. Depending on the
        /// number of arguments and type of argument, the following services are provided.
        /// <list type="bullet">
        /// <item>
        /// <term>
        /// Empty parameter list
        /// </term>
        /// <description>
        /// Returns the currently set password.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// String that does not violate the password rules
        /// </term>
        /// <description>
        /// Sets the port equal to the numeric value and returns a confirmation message.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// String that violates the password rules
        /// </term>
        /// <description>
        /// Returns an error message in form of a 
        /// <see cref="Properties.Resources.InvalidPasswordExceptionMessage"/>.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// Server is running
        /// </term>
        /// <description>
        /// While the server is running, the password can't be changed.
        /// </description>
        /// </item>
        /// <item>
        /// <term>
        /// Parameter 2...n
        /// </term>
        /// <description>
        /// Any kind of parameter, after the first one, will be ignored.
        /// </description>
        /// </item>
        /// </list>
        /// </summary>
        /// 
        /// <param name="parameterList">List of all parameters the command has been called 
        /// with</param>
        /// 
        /// <returns>Which password is currently set or has been set, if no errors 
        /// occurred.</returns>
        private string ParsePassword(string[] parameterList)
        {
            // command: password -> Returns the currently set password.
            if (parameterList.Length == 0)
            {
                return Password;
            }
            // command: password string -> Sets a new password.
            else
            {
                if (serverIsRunning)
                {
                    return "The server is running. Switching passwords is disabled.";
                }
                else
                {
                    // test if password string is of adequate length and characters.
                    try
                    {
                        Password = parameterList[0];
                    }
                    // password string violates the password rules.
                    catch (ArgumentException)
                    {
                        return Properties.Resources.InvalidPasswordExceptionMessage;
                    }

                    return "The password has been set to " + parameterList[0] + " successfully.";
                }
            }
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
            if (!serverIsRunning)
            {
                foreach (string item in parameterList)
                {
                    if (Regex.IsMatch(item, @"--port\=(\d*)"))
                    {
                        try
                        {
                            Match m = Regex.Match(item, @"--port\=(\d*)");
                            Port = Convert.ToInt32(m.Groups[1].Value, CultureInfo.CurrentCulture);
                        }
                        catch (ArgumentException)
                        {
                            return Properties.Resources.InvalidPortExceptionMessage;
                        }
                    }
                    else if (Regex.IsMatch(item, @"--password\=(\S*)"))
                    {
                        try
                        {
                            Match m = Regex.Match(item, @"--password\=(\S*)");
                            Password = m.Groups[1].Value;
                        }
                        catch (ArgumentException)
                        {
                            return Properties.Resources.InvalidPasswordExceptionMessage;
                        }
                    }
                }
                try
                {
                    if (!isDebug)
                    {
                        mainServerLogic.Start(Port);
                        mainServerLogic.SetPassword(this._password);
                    }
                }
                catch (Exception e)
                {
                    return "The server could not be started due to following Exception: \n"
                        + e.StackTrace;
                }
                serverIsRunning = true;
                return "The server has been started successfully with port: " + Port;
            }

            return "The server is already running with port: " + Port;
        }

        /// <summary>
        /// This function stops the server and thus terminates any kind of communication
        /// with the Moderator-Client or any PlayerAudience-Client.
        /// </summary>
        /// 
        /// <returns>Confirmation hat the server has been stopped successfully.</returns>
        private string StopServer()
        {
            mainServerLogic.Stop();
            serverIsRunning = false;
            return "The server has been shut down successfully.";
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
                    case "--setLevel":
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
                    case "--setLogOutput":
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
                        return "Current LogLevel is "+ Settings.Default.LogLevel +".";
                    default:
                        return "Command "+parameterList[0]+" is unknown, use 'log --help' for more information";
                }
            }
        }

        /// <summary>
        /// Wrapper-Function to check if the command-line arguments are valid and can be
        /// transmitted forward to set password and/or port of the server.
        /// </summary>
        /// 
        /// <param name="args">Command-line parameters that need to checked.</param>
        /// 
        /// <returns>To be used password and port, if no errors occurred.</returns>
        /// 
        /// <exception cref="System.ArgumentException">Thrown when either the supposed 
        /// password or port violate the rules for setting them.</exception>
        private static string[] CheckMainMethodArgs(string[] args)
        {
            int port;
            string password;

            if (args.Length == 0)
            {
                throw new ArgumentException(message: Properties.Resources.InvalidPasswordExceptionMessage);
            }
            else
            {
                password = ValidateShellPassword(args[0].ToLower(CultureInfo.CurrentCulture));

                if (args.Length == 1)
                {
                    port = 7777;
                }
                else
                {
                    port = ValidateShellPort(args[1]);
                }

                return new string[] { password, port.ToString(CultureInfo.CurrentCulture) };
            }
        }

        /// <summary>
        /// Checks if the supposed password is an actual password and if it complies with
        /// the password rules. Alternatively, if the supposed password starts with a dash,
        /// it's instead checked if a known option is being called.
        /// </summary>
        /// 
        /// <param name="password">The supposed password that needs to be checked.</param>
        /// 
        /// <returns>The entered password, if no errors occurred.</returns>
        /// 
        /// <exception cref="System.ArgumentException">Thrown when the supposed password 
        /// violates the rules.</exception>
        private static string ValidateShellPassword(string password)
        {
            if (password.Length == 0)
            {
                throw new ArgumentException(message: Properties.Resources.InvalidPasswordExceptionMessage);
            }
            else if (password.StartsWith("-", StringComparison.CurrentCulture))
            {
                if (password == "--version")
                {
                    Console.WriteLine(Properties.Resources.CurrentVersion);
                }
                else if (password == "--help")
                {
                    Console.WriteLine(Properties.Resources.HelpHelpMessage);
                }
                else
                {
                    throw new ArgumentException(message: "Unknown option: '" + password + "'");
                }

                Environment.Exit(exitCode: 0);
                return null;
            }
            else
            {
                return password;
            }
        }

        /// <summary>
        /// Checks if the supposed port is an actual password and if it complies with
        /// the port rules.
        /// </summary>
        /// 
        /// <param name="portString">The supposed port that needs to be checked.</param>
        /// 
        /// <returns>The entered port, if no errors occurred.</returns>
        /// 
        /// <exception cref="System.ArgumentException">Thrown when the supposed port
        /// violates the rules.</exception>
        private static int ValidateShellPort(string portString)
        {
            int port;

            try
            {
                port = Convert.ToInt32(value: portString, provider: CultureInfo.CurrentCulture);

                if (port <= 1023 || port > 65535)
                {
                    throw new ArgumentException(message: Properties.Resources.InvalidPortExceptionMessage);
                }
                else
                {
                    return port;
                }
            }
            catch (Exception)
            {
                throw new ArgumentException(message: Properties.Resources.InvalidPortExceptionMessage);
            }

        }
    }
}