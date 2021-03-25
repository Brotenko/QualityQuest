using System;
using System.IO;
using System.Runtime.CompilerServices;
using ServerLogic.Properties;


namespace ServerLogic.Control
{
    public class ServerLogger
    {
        //Necessary for Singleton pattern
        private static ServerLogger _serverLogger;

        
        /// <summary>
        /// ServerLogger implements the Singleton-Pattern to ensure there is only one Logger active.
        /// Therefore, there is no need for a ServerLogger object.
        /// </summary>
        private ServerLogger()
        {
            //default Logging-Level
            LogInformation("New Logger-Instance started. LogLevel is " + Settings.Default.LogLevel +
                           ", LogOutputType is " + Settings.Default.LogOutPutType + ".");
        }


        /// <summary>
        /// Checks whether there is already an instance of the ServerLogger and creates one if necessary. Should be called at least once before using the ServerLogger.
        /// </summary>
        public static void CreateServerLogger()
        {
            //Only creates new ServerLogger when field is NULL
            _serverLogger ??= new ServerLogger();
        }


        /// <summary>
        /// Sets to which grade information gets logged. Every Level includes the Information of the Levels below it, e.g. Warning Level also logs Error-Logs.
        /// <list type="bullet">
        /// <item><param> 0 </param><term>Debug </term><description> For debugging and development. Use with caution in production due to the high volume. </description></item>
        /// <item><param> 1 </param><term>Information</term><description>Tracks the general flow of the app. </description></item>
        /// <item><param> 2 </param><term>Warning</term><description>For abnormal or unexpected events. Typically includes errors or conditions that don't cause the app to fail.</description></item>
        /// <item><param> 3 </param><term>Error</term><description>For errors and exceptions that cannot be handled. These messages indicate a failure in the current operation or request, not an app-wide failure.</description></item>
        /// <item><param> 4 </param><term>None</term><description>Logs won't be recorded. Deactivates the logger.</description></item>
        /// </list>
        /// </summary>
        /// <param name="level">Which level to use.</param>
        public static void SetLogLevel(int level)
        {
            if (level is >= 0 and <= 4)
            {
                Settings.Default.LogLevel = level;
                Settings.Default.Save();
                LogInformation("LogLevel was set to " + level + ".");
            }
            else
            {
                Console.WriteLine(Resources.InvalidLogLevelMessage);
                LogInformation("Invalid LogLevel: "+level);
            }
        }

        /// <summary>
        /// Writes the received string, according to the set Logging Output, either in a File or prints it on the Console. Adds the date and the current logging level to the message.
        /// </summary>
        /// <param name="logMessage">What should be logged.</param>
        private static void WriteLog(string logMessage)
        {
            string logRecord = string.Format("{0} [{1}] {2}",
                "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "]",
                Settings.Default.LogLevel.ToString(),
                logMessage);

            if (Settings.Default.LogOutPutType == 1)
            {
                Console.WriteLine(logRecord);
            }
            else if (Settings.Default.LogOutPutType == 0)
            {
                using var streamWriter = new StreamWriter(Settings.Default.LogFilePath, true);
                streamWriter.WriteLine(logRecord);
                streamWriter.Close();
            }
            else
            {
                using var streamWriter = new StreamWriter(Settings.Default.LogFilePath, true);
                streamWriter.WriteLine(logRecord);
                streamWriter.Close();
                Console.WriteLine(logRecord);
            }
        }

        /// <summary>
        /// Reads the created log file and returns the content as a string.
        /// </summary>
        /// <returns>Returns the Log-File as a string.</returns>
        public static string LogFileToString()
        {
            string fileToString = "";
            try
            {
                using var streamReader = new StreamReader(Settings.Default.LogFilePath, true);
                fileToString = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (FileNotFoundException)
            {
                //If Log.txt was deleted before, this will occur. No need for further Exception-Handling, as a new file is created during the next writing process.
            }

            return fileToString;
        }

        /// <summary>
        /// Deletes the Log-File.
        /// </summary>
        public static void WipeLogFile()
        {
            File.Delete(Settings.Default.LogFilePath);
        }

        /// <summary>
        /// Defines the way the logs are output. The logs can be saved to a file, output to the console or both.
        /// </summary>
        /// <param name="val">
        /// <list type="bullet">
        /// <item><param name="val">0  </param><description>Logs are written to the Log.txt file.</description></item>
        /// <item><param name="val">1 </param><description>Logs are output directly on the console.</description></item>
        /// <item><param name="val">2 </param><description>Logs are output directly on the console and written to the Log.txt file.</description></item>
        /// </list>
        /// </param>
        public static void ChangeLoggingOutputType(int val)
        {
            if (val is <= 2 and >= 0)
            {
                Settings.Default.LogOutPutType = val;
                Settings.Default.Save();
                LogInformation("LoggingOutputType was set to "+val+".");
            }
            else
            {
                Console.WriteLine(Resources.InvalidLoggingOutputType);
                LogInformation("Invalid LoggingOutputType: "+val);
            }
        }

        /// <summary>
        /// Saves the passed string as a "debug" log.
        /// </summary>
        /// <param name="record">Debug-Infos which should be logged.</param>
        public static void LogDebug(string record)
        {
            if (Settings.Default.LogLevel == 0)
            {
                WriteLog("DEBUG: " + record);
            }
        }

        /// <summary>
        /// Saves the passed string as a Information.
        /// </summary>
        /// <param name="record">Informations which should be logged.</param>
        public static void LogInformation(string record)
        {
            if (Settings.Default.LogLevel <= 1)
            {
                WriteLog("INFO: " + record);
            }
        }

        /// <summary>
        /// Saves the passed string as a Warning.
        /// </summary>
        /// <param name="record">Warnings which should be logged.</param>
        public static void LogWarning(string record)
        {
            if (Settings.Default.LogLevel <= 2)
            {
                WriteLog("WARNING: " + record);
            }
        }

        /// <summary>
        /// Saves the passed string as an Error.
        /// </summary>
        /// <param name="record">Errors which should be logged.</param>
        public static void LogError(string record)
        {
            if (Settings.Default.LogLevel <= 3)
            {
                WriteLog("ERROR: " + record);
            }
        }
    }
}