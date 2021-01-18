﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ServerLogic.Control
{
    class ServerLogger
    {
        private static ServerLogger _serverLogger;
        private int logLevel; //todo evtl. als property umsetzen

        /// <summary>
        /// ServerLogger implements the Singleton-Pattern to ensure there is only one Logger active.
        /// To get a ServerLogger object, use CreateServerLogger().
        /// </summary>
        private ServerLogger()
        {
            //default Logging-Level
            logLevel = 2;
            writeToFile("New Session started. Default LogLevel is "+logLevel+".");
        }

        /// <summary>
        /// Checks whether there is already an instance of the ServerLogger and creates one if necessary.
        /// </summary>
        /// <returns>ServerLogger instance.</returns>
        public static ServerLogger CreateServerLogger()
        {
            //"return _serverLogger == NULL ? new ServerLogger() : _serverLogger;" would be the Java-Equivalent
            return _serverLogger ??= new ServerLogger();
        }

        /// <summary>
        /// Sets to which grade information gets logged. Every Level includes the Information of the Levels below it, e.g. Warning Level also displays Error-Logs.
        /// 
        /// Used for:   |Level: |   Method:         |   Description:
        /// -----------------------------------------------------------------------------------------------------------------------------
        /// Debug       |   0   |   LogDebug        |   For debugging and development. Use with caution in production due to the high volume.
        /// Information |   1   | 	LogInformation  | 	Tracks the general flow of the app. May have long-term value.
        /// Warning     |   2   |   LogWarning      | 	For abnormal or unexpected events. Typically includes errors or conditions that don't cause the app to fail.
        /// Error       |   3   |   LogError        |   For errors and exceptions that cannot be handled. These messages indicate a failure in the current operation or request, not an app-wide failure.
        /// None        |   4   | 		            |   Specifies that a logging category should not write any messages.
        /// todo use some kind of list for comment
        /// </summary>
        /// <param name="level"></param>
        public void setLogLevel(int level)
        {
            writeToFile("logLevel is changed from "+logLevel+" to "+level+".");
            logLevel = level;
        }

        private void writeToFile(string logMessage)
        {
            
            var logRecord = string.Format("{0} [{1}] {2}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), logMessage);

            //todo: it should be clarified whether there is one general log-file or several, session-wise or date-wise.
            using var streamWriter = new StreamWriter(Properties.Resources.LogFilePath, true);
            streamWriter.WriteLine(logRecord);
            streamWriter.Close();
        }

        public string logFileToString()
        {
            using var streamReader = new StreamReader(Properties.Resources.LogFilePath, true);
            string fileToString = streamReader.ReadToEnd();
            return fileToString;
        }

        public void wipeLogFile()
        {

        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
           
            /*
            if (!Directory.Exists(logFilePath))
            {
                Directory.CreateDirectory(logFilePath);
            }
            var fullFilePath = _loggerProvider.logFilePath;
            var logRecord = string.Format("{0} [{1}] {2} {3}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }*/
        }

     
    }
}