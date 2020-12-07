using ServerLogic.Model;
using ServerLogic.Model.Messages;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Resources;
using System.Text.RegularExpressions;

namespace ServerLogic.Control
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerShell
    {
        //public Logger logger = [...];
        //private MainServerLogic mainServerLogic = new MainServerLogic();
        private int _port;
        private string _password; //Passwort des Servers 
        private readonly ServerShell serverShell;
        private bool serverIsRunning = false;
        private bool commandRequestsHelpMessage = false;

        /// <summary>
        /// 
        /// </summary>
        public int Port
        {
            get => _port;
            private set
            {
                if (value <= 1023 || value > 65535)
                {
                    throw new ArgumentException(message: Properties.Resources.InvalidPortExceptionMessage);
                }

                _port = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Password
        {
            get => _password;
            private set
            {
                if (value.Length >= 8 && value.Length <= 32)
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
        /// 
        /// </summary>
        public ServerShell()
        {
            this.serverShell = this;
            this.Password = "!Password123";
            this.Port = 7777;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="password"></param>
        /// <param name="port"></param>
        public ServerShell(string password, int port)
        {
            this.serverShell = this;
            this.Password = password;
            this.Port = port;

            Console.WriteLine("Password: " + password + " | Port: " + port);

            RunShell();
        }

        /// <summary>
        /// 
        /// </summary>
        private void RunShell()
        {
            while (true)
            {
                Console.Write(value: "qq >> ");
                string command = Console.ReadLine();

                Console.WriteLine(ParseCommand(command));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void StopShell()
        {
            Environment.Exit(exitCode: 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="debugInput"></param>
        /// <returns></returns>
        public string ParseCommandDebugger(string debugInput)
        {
            return ParseCommand(debugInput);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string ParseCommand(string input)
        {
            input = Regex.Replace(input, @"\s+", " ");
            input = input.Trim();
            string[] splitInput = input.Split(' ');
            string command = splitInput[0].ToLower(CultureInfo.CurrentCulture);
            string[] commandParameters = new string[splitInput.Length - 1];
            string ret = "";

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
                    ret = ShowLogs();
                    break;
                case "clear":
                    ret = ClearLogs();
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
        /// 
        /// </summary>
        /// <param name="commandList"></param>
        /// <returns></returns>
        private string ParsePort(string[] commandList)
        {
            // command: port -> Returns the currently set port.
            if (commandList.Length == 0)
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
                        tempPort = Convert.ToInt32(commandList[0], CultureInfo.CurrentCulture);
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
        /// 
        /// </summary>
        /// <param name="commandList"></param>
        /// <returns></returns>
        private string ParsePassword(string[] commandList)
        {
            // command: password -> Returns the currently set password.
            if (commandList.Length == 0)
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
                        Password = commandList[0];
                    }
                    // password string violates the password rules.
                    catch (ArgumentException)
                    {
                        return Properties.Resources.InvalidPasswordExceptionMessage;
                    }

                    return "The password has been set to " + commandList[0] + " successfully.";
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandList"></param>
        /// <returns></returns>
        private string StartServer(string[] commandList)
        {
            foreach (string item in commandList)
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

            Console.WriteLine("Password: " + Password + "; Port: " + Port);

            // Need MainServerLogic first
            serverIsRunning = true;
            return "The server has been started successfully with port: " + Port;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string StopServer()
        {
            // Need MainServerLogic first
            serverIsRunning = false;
            return "The server has been shut down successfully.";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string ShowHelp(string command)
        {
            string ret = "";

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
            case "clear":
                ret = Properties.Resources.ClearHelpMessage;
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
        /// 
        /// </summary>
        /// <returns></returns>
        private string ShowVersion()
        {
            return Properties.Resources.CurrentVersion;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ShowLogs()
        {
            // Need logger first
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string ClearLogs()
        {
            // Need logger first
            return "Cleared logs.";
        }

        private static string[] checkMainMethodArgs(string[] args)
        {
            if (args.Length == 0)
            {
                throw new ArgumentException(message: Properties.Resources.InvalidPasswordExceptionMessage);
            }

            string password = args[0];
            int port = -1;

            if (password.Length == 0)
            {
                throw new ArgumentException(message: Properties.Resources.InvalidPasswordExceptionMessage);
            }

            if (args[0].ToLower(CultureInfo.CurrentCulture) == "--version")
            {
                Console.WriteLine(Properties.Resources.CurrentVersion);
                return Array.Empty<string>();
            }
            else if (args[0].ToLower(CultureInfo.CurrentCulture) == "--help")
            {
                Console.WriteLine(Properties.Resources.HelpHelpMessage);
                return Array.Empty<string>();
            }
            else if (args[0].ToLower(CultureInfo.CurrentCulture).StartsWith("--", StringComparison.CurrentCulture))
            {
                throw new ArgumentException(message: Properties.Resources.InvalidPasswordExceptionMessage);
            }

            if (args.Length == 1)
            {
                port = 7777;
            }

            if (args.Length >= 2)
            {
                try
                {
                    port = Convert.ToInt32(value: args[1], provider: CultureInfo.CurrentCulture);
                }
                catch (Exception)
                {
                    throw new ArgumentException(message: Properties.Resources.InvalidPortExceptionMessage);
                }

                if (port <= 1023 || port > 65535)
                {
                    throw new ArgumentException(message: Properties.Resources.InvalidPortExceptionMessage);
                }
            }

            return new string[] {password, port.ToString(CultureInfo.CurrentCulture)};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            string[] returnValue = checkMainMethodArgs(args);

            if (returnValue == null)
            {
                return;
            }

            ServerShell serverShell = new ServerShell(password: returnValue[0], port: Convert.ToInt32(returnValue[1], CultureInfo.CurrentCulture));
        }
    }
}