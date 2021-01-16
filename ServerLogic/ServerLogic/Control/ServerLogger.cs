using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace ServerLogic.Control
{
    class ServerLogger: ILogger  
    {
        private static ServerLogger _serverLogger;
        private readonly LoggerProvider _loggerProvider;


        /// <summary>
        /// ServerLogger implements the Singleton-Pattern to ensure there is only one Logger active.
        /// To get a ServerLogger object, use CreateServerLogger().
        /// </summary>
        private ServerLogger([NotNull] LoggerProvider loggerProvider)
        {
            _loggerProvider = loggerProvider;
            _serverLogger = this;
        }

        /// <summary>
        /// Checks whether there is already an instance of the ServerLogger and creates one if necessary.
        /// </summary>
        /// <returns>A ServerLogger instance.</returns>
        public static ServerLogger CreateServerLogger()
        {
            if (_serverLogger == null)
            {
                _serverLogger = new ServerLogger(new LoggerProvider());
            }
            
            return _serverLogger;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            var fullFilePath = _loggerProvider.logFilePath;
            var logRecord = string.Format("{0} [{1}] {2} {3}", "[" + DateTimeOffset.UtcNow.ToString("yyyy-MM-dd HH:mm:ss+00:00") + "]", logLevel.ToString(), formatter(state, exception), exception != null ? exception.StackTrace : "");

            using (var streamWriter = new StreamWriter(fullFilePath, true))
            {
                streamWriter.WriteLine(logRecord);
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel != LogLevel.None;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        /// <summary>
        /// This class is needed for setting Logoutput etc.
        /// </summary>
        private class LoggerProvider : ILoggerProvider
        {
            //todo path should be stored in the Resources.resx
            public string logFilePath = "..\\Properties\\logFiles";

            public LoggerProvider()
            {
                if (!Directory.Exists(logFilePath))
                {
                    Directory.CreateDirectory(logFilePath);
                }
            }
            /// <summary>
            /// This method is needed for implementing the ILoggerProvider Interface
            /// </summary>
            /// <param name="categoryName"> not used, not relevant </param>
            /// <returns></returns>
            public ILogger CreateLogger(string categoryName)
            {
                return ServerLogger.CreateServerLogger();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }
        }
    }
}