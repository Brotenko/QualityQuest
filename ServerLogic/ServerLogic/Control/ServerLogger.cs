using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;

namespace ServerLogic.Control
{
    class ServerLogger
    {
        private static ServerLogger _serverLogger;

        /// <summary>
        /// ServerLogger implements the Singleton-Pattern to ensure there is only one Logger active.
        /// Therefore, there is no need for a ServerLogger object.
        /// </summary>
        private ServerLogger()
        {
            //default Logging-Level
            LogInformation("New Session started. LogLevel is " + Properties.Settings.Default.LogLevel + ", LogOutputType is "+ Properties.Settings.Default.LogOutPutType+".");
        }

        /// <summary>
        /// Checks whether there is already an instance of the ServerLogger and creates one if necessary. Should be called at least once before using the ServerLogger.
        /// </summary>
        public static void CreateServerLogger()
        {
            //"if (_severLogger == NULL) _serverLogger = new ServerLogger();" is the same
            _serverLogger ??= new ServerLogger();
        }

        /// <summary>
        /// Sets to which grade information gets logged. Every Level includes the Information of the Levels below it, e.g. Warning Level also logs Error-Logs.
        /// <list type="bullet">
        /// <item><param> 0 </param><term>Debug </term><description> For debugging and development. Use with caution in production due to the high volume. </description></item>
        /// <item><param> 1 </param><term>Information</term><description>Tracks the general flow of the app. May have long-term value.</description></item>
        /// <item><param> 2 </param><term>Warning</term><description>For abnormal or unexpected events. Typically includes errors or conditions that don't cause the app to fail.</description></item>
        /// <item><param> 3 </param><term>Error</term><description>For errors and exceptions that cannot be handled. These messages indicate a failure in the current operation or request, not an app-wide failure.</description></item>
        /// <item><param> 4 </param><term>None</term><description>Logs won't be recorded. Deactivates the logger.</description></item>
        /// </list>
        /// </summary>
        /// <param name="level">Which level to use.</param>
        public static void SetLogLevel(int level)
        {
            if (level >= 0 && level <= 4)
            {
                Properties.Settings.Default.LogLevel = level;
                Properties.Settings.Default.Save();
                LogInformation("LogLevel was set to " + level + ".");
            }
            else
            {
                Console.WriteLine(Properties.Resources.InvalidLogLevelMessage);
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
                Properties.Settings.Default.LogLevel.ToString(),
                logMessage);

            if (Properties.Settings.Default.LogOutPutType == 1)
            {
                Console.WriteLine(logRecord);
            }
            else if(Properties.Settings.Default.LogOutPutType==0)
            {
                try
                {
                    using var streamWriter = new StreamWriter(Properties.Settings.Default.LogFilePath, true);
                    streamWriter.WriteLine(logRecord);
                    streamWriter.Close();
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    //todo relevant in testing case
                }
                
            }
            else
            {
                try
                {
                    using var streamWriter = new StreamWriter(Properties.Settings.Default.LogFilePath, true);
                    streamWriter.WriteLine(logRecord);
                    streamWriter.Close();
                    Console.WriteLine(logRecord);
                }
                catch (System.IO.DirectoryNotFoundException)
                {
                    //todo relevant in testing case
                }
            }
        }

        /// <summary>
        /// Reads the created log file and returns the content as a string.
        /// </summary>
        /// <returns></returns>
        public static string LogFileToString()
        {
            string fileToString = "";
            try
            {
                using var streamReader = new StreamReader(Properties.Settings.Default.LogFilePath, true);
                fileToString = streamReader.ReadToEnd();
                streamReader.Close();
            }
            catch (FileNotFoundException)
            {
                //If Log.txt was deleted before, this will occur. No need for Exception-Handling, as a new file is created during the next writing process.
            }

            return fileToString;
        }

        /// <summary>
        /// Deletes the Log-File.
        /// </summary>
        public static void WipeLogFile()
        {
            File.Delete(Properties.Resources.LogFilePath);
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
            if (val <= 2 && val >= 0)
            {
                Properties.Settings.Default.LogOutPutType = val;
                Properties.Settings.Default.Save();
            }
            else
            {
                Console.WriteLine(Properties.Resources.InvalidLoggingOutputType);
            }
        }

        private class ServerLogicLog
        {
            public string SessionKey;
            public Guid ModeratorId;
            public Guid[] audienceClients;
            public Dictionary<Guid, int> VotingResults;
        }
        public static void CreateServerSessionLog(string sessionKey, Guid moderatorId)
        {
            //todo this method is a draft right now, as it heavily relies on information provided by the yet-not-finished AudienceClient
            ServerLogicLog log = new ServerLogicLog();
            log.SessionKey = sessionKey;
            log.ModeratorId = moderatorId;
            string jsonString = JsonSerializer.Serialize<ServerLogicLog>(log);
            File.WriteAllText(Properties.Settings.Default.ServerLogicLogFilePath+"Session_"+sessionKey+".txt",jsonString);
        }

        public static Guid GetModeratorIdFromSessionLog(string sessionKey)
        {
            string jsonString = File.ReadAllText(Properties.Settings.Default.ServerLogicLogFilePath + "Session_" +
                                                       sessionKey + ".txt");
            ServerLogicLog logicLog = JsonSerializer.Deserialize<ServerLogicLog>(jsonString);
            return logicLog.ModeratorId;
        }

        public static void AddStatsToSession(string sessionKey, Dictionary<Guid,int> votingResults)
        {
            string jsonString = File.ReadAllText(Properties.Settings.Default.ServerLogicLogFilePath + "Session_" +
                                                 sessionKey + ".txt");
            ServerLogicLog logicLog = JsonSerializer.Deserialize<ServerLogicLog>(jsonString);
            if (logicLog.VotingResults != null)
            {
                foreach (KeyValuePair<Guid, int> entry in votingResults)
                {
                    logicLog.VotingResults.Add(entry.Key, entry.Value);
                }
            }
            else
            {
                logicLog.VotingResults = votingResults;
            }
            jsonString = JsonSerializer.Serialize<ServerLogicLog>(logicLog);
            File.WriteAllText(Properties.Settings.Default.ServerLogicLogFilePath + "Session_" + sessionKey + ".txt", jsonString);
        }

        public static Dictionary<Guid, int> GetStatsFromSession(string sessionKey)
        {
            string jsonString = File.ReadAllText(Properties.Settings.Default.ServerLogicLogFilePath + "Session_" +
                                                 sessionKey + ".txt");
            ServerLogicLog logicLog = JsonSerializer.Deserialize<ServerLogicLog>(jsonString);
            return logicLog.VotingResults;
        }

        public static void ClearSessionLog(string sessionKey)
        {
            File.Delete(Properties.Settings.Default.ServerLogicLogFilePath + "Session_" +
                        sessionKey + ".txt");
        }

        public static void DeleteAllSessionLogs()
        {
            //Directory.Delete(Properties.Settings.Default.ServerLogicLogFilePath, true);
        }


        /// <summary>
        /// Saves the passed string as a "debug" log.
        /// </summary>
        /// <param name="record">What should be logged.</param>
        public static void LogDebug(string record)
        {
            if (Properties.Settings.Default.LogLevel == 0)
            {
                WriteLog("DEBUG: " + record);
            }
        }

        /// <summary>
        /// Saves the passed string as a Information.
        /// </summary>
        /// <param name="record">What should be logged.</param>
        public static void LogInformation(string record)
        {
            if (Properties.Settings.Default.LogLevel <= 1)
            {
                WriteLog("INFO: " + record);
            }
        }

        /// <summary>
        /// Saves the passed string as a Warning.
        /// </summary>
        /// <param name="record">What should be logged.</param>
        public static void LogWarning(string record)
        {
            if (Properties.Settings.Default.LogLevel <= 2)
            {
                WriteLog("WARNING: " + record);
            }
        }

        /// <summary>
        /// Saves the passed string as an Error.
        /// </summary>
        /// <param name="record">What should be logged.</param>
        public static void LogError(string record)
        {
            if (Properties.Settings.Default.LogLevel <= 3)
            {
                WriteLog("ERROR: " + record);
            }
        }
    }
}