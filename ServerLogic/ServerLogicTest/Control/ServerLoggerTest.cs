using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Control;
using ServerLogic.Properties;

//TODO refactor according to microsoft unit-test best practices


namespace ServerLogicTest.Control
{
    [TestClass]
    public class ServerLoggerTest
    {
        // [yyyy-MM-dd HH:mm:ss] [] ERROR:
        private const string logErrorPattern =
            @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sERROR:";

        // [yyyy-MM-dd HH:mm:ss] [] WARNING:
        private const string logWarningPattern =
            @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sWARNING:";

        // [yyyy-MM-dd HH:mm:ss] [] INFO:
        private const string logInformationPattern =
            @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sINFO:";

        // [yyyy-MM-dd HH:mm:ss] [] DEBUG:
        private const string logDebugPattern =
            @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sDEBUG:";

        /// <summary>
        /// Sets the path variable for the log file from the settings to a test log
        /// file to prevent changes to the actual log file by the tests.
        /// Since the changed value is not saved, the actual settings value is only
        /// temporarily overwritten and has the original value again the next time the application is started.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            Settings.Default.LogFilePath = "TestLog.txt";
            ServerLogger.CreateServerLogger();
            ServerLogger.SetLogLevel(0);
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.WipeLogFile();
        }

        /// <summary>
        /// Deletes any log files that may have been created after a test has been executed.
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            ServerLogger.WipeLogFile();
        }

        /// <summary>
        /// Checks if the SetLogLevel() handles input correctly.
        /// </summary>
        [TestMethod]
        public void InvalidLogLevelInputTest()
        {
            //illegitimate value
            ServerLogger.SetLogLevel(5);
            //LogLevel was set to 0 by the Initalize()-Method
            Assert.AreEqual(0, Settings.Default.LogLevel);
            //illegitimate value
            ServerLogger.SetLogLevel(-1);
            Assert.AreEqual(0, Settings.Default.LogLevel);
        }

        /// <summary>
        /// Tries the possible LogLevels through.
        /// </summary>
        [TestMethod]
        public void ValidLogLevelInputTest()
        {
            Assert.AreEqual(0, Settings.Default.LogLevel);
            //Legitimate value
            ServerLogger.SetLogLevel(4);
            Assert.AreEqual(4, Settings.Default.LogLevel);
            //Legitimate value
            ServerLogger.SetLogLevel(3);
            Assert.AreEqual(3, Settings.Default.LogLevel);
            //Legitimate value
            ServerLogger.SetLogLevel(2);
            Assert.AreEqual(2, Settings.Default.LogLevel);
            //Legitimate value
            ServerLogger.SetLogLevel(1);
            Assert.AreEqual(1, Settings.Default.LogLevel);
        }

        /// <summary>
        /// Creates a fresh log file and set the level to 4 aka "None".
        /// Writes log messages at all possible levels, at "None" none of them should be written in the log,
        /// which is checked by the Assertion.
        ///
        /// Note: Even if an empty string is passed to the ServerLogger-Class in the test, it will still be written to the log,
        /// depending on the LogLevel, because the ServerLogger-Class adds timestamp and type to the passed strings.
        /// </summary>
        [TestMethod]
        public void LogLevelNoneTest()
        {
            ServerLogger.SetLogLevel(4);
            ServerLogger.WipeLogFile();

            //Visible for lvl 0 as "[yyyy-MM-dd HH:mm:ss] [4] DEBUG:"
            ServerLogger.LogDebug("");
            //Visible for lvl 0, 1 as "[yyyy-MM-dd HH:mm:ss] [4] INFO:"
            ServerLogger.LogInformation("");
            //Visible for lvl 0, 1, 2 as "[yyyy-MM-dd HH:mm:ss] [4] WARNING:"
            ServerLogger.LogWarning("");
            //Visible for lvl 0, 1, 2, 3 as "[yyyy-MM-dd HH:mm:ss] [4] ERROR:"
            ServerLogger.LogError("");

            //LogLevel 4 means that logger is deactivated, so the LogFile should be empty.
            Assert.AreEqual("", ServerLogger.LogFileToString());
        }

        /// <summary>
        /// Creates a fresh log file and sets the LogLevel to 3 aka "Error".
        /// Writes log messages at all possible levels, at "Error" only Messages marked as Error should be written into the log,
        /// which is checked by the Assertion.
        ///
        /// Note: Even if an empty string is passed to the ServerLogger-Class in the test, it will still be written to the log,
        /// depending on the LogLevel, because the ServerLogger-Class adds timestamp and type to the passed strings.
        /// </summary>
        [TestMethod]
        public void LogLevelErrorTest()
        {
            ServerLogger.SetLogLevel(3);
            ServerLogger.WipeLogFile();

            //Visible for lvl 0 as "[yyyy-MM-dd HH:mm:ss] [3] DEBUG:"
            ServerLogger.LogDebug("");
            //Visible for lvl 0, 1 as "[yyyy-MM-dd HH:mm:ss] [3] INFO:"
            ServerLogger.LogInformation("");
            //Visible for lvl 0, 1, 2 as "[yyyy-MM-dd HH:mm:ss] [3] WARNING:"
            ServerLogger.LogWarning("");
            //Visible for lvl 0, 1, 2, 3 as "[yyyy-MM-dd HH:mm:ss] [3] ERROR:"
            ServerLogger.LogError("");

            //LogLevel 3 means only Errors
            Assert.IsTrue(Regex.IsMatch(ServerLogger.LogFileToString(), logErrorPattern));
        }

        /// <summary>
        /// Creates a fresh log file and sets the LogLevel to 2 aka "Warning".
        /// Writes log messages at all possible levels, at "Warning" only Messages marked as Error or Warning should be written into the log,
        /// which is checked by the Assertion.
        ///
        /// Note: Even if an empty string is passed to the ServerLogger-Class in the test, it will still be written to the log,
        /// depending on the LogLevel, because the ServerLogger-Class adds timestamp and type to the passed strings.
        /// </summary>
        [TestMethod]
        public void LogLevelWarningTest()
        {
            ServerLogger.SetLogLevel(2);
            ServerLogger.WipeLogFile();

            //Visible for lvl 0 as "[yyyy-MM-dd HH:mm:ss] [2] DEBUG:"
            ServerLogger.LogDebug("");
            //Visible for lvl 0, 1 as "[yyyy-MM-dd HH:mm:ss] [2] INFO:"
            ServerLogger.LogInformation("");
            //Visible for lvl 0, 1, 2 as "[yyyy-MM-dd HH:mm:ss] [2] WARNING:"
            ServerLogger.LogWarning("");
            //Visible for lvl 0, 1, 2, 3 as "[yyyy-MM-dd HH:mm:ss] [2] ERROR:"
            ServerLogger.LogError("");

            //LogLevel 2 means only Errors and Warnings
            string[] loggedLines = ServerLogger.LogFileToString().Split("\n");
            Assert.IsTrue(Regex.IsMatch(loggedLines[0], logWarningPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[1], logErrorPattern));
        }

        /// <summary>
        /// Creates a fresh log file and sets the LogLevel to 2 aka "Information".
        /// Writes log messages at all possible levels, at "Information" only Messages marked as Error, Warning or Information should be written into the log,
        /// which is checked by the Assertion.
        ///
        /// Note: Even if an empty string is passed to the ServerLogger-Class in the test, it will still be written to the log,
        /// depending on the LogLevel, because the ServerLogger-Class adds timestamp and type to the passed strings.
        /// </summary>
        [TestMethod]
        public void LogLevelInformationTest()
        {
            ServerLogger.SetLogLevel(1);
            ServerLogger.WipeLogFile();

            //Visible for lvl 0 as "[yyyy-MM-dd HH:mm:ss] [1] DEBUG:"
            ServerLogger.LogDebug("");
            //Visible for lvl 0, 1 as "[yyyy-MM-dd HH:mm:ss] [1] INFO:"
            ServerLogger.LogInformation("");
            //Visible for lvl 0, 1, 2 as "[yyyy-MM-dd HH:mm:ss] [1] WARNING:"
            ServerLogger.LogWarning("");
            //Visible for lvl 0, 1, 2, 3 as "[yyyy-MM-dd HH:mm:ss] [1] ERROR:"
            ServerLogger.LogError("");

            //LogLevel 1 means Errors, Warnings and Informations
            string[] loggedLines = ServerLogger.LogFileToString().Split("\n");
            Assert.IsTrue(Regex.IsMatch(loggedLines[0], logInformationPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[1], logWarningPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[2], logErrorPattern));
        }


        /// <summary>
        /// Creates a fresh log file and sets the LogLevel to 0 aka "Debug".
        /// Writes log messages at all possible levels, at "Debug" all Messages should be written into the log,
        /// which is checked by the Assertion.
        ///
        /// Note: Even if an empty string is passed to the ServerLogger-Class in the test, it will still be written to the log,
        /// depending on the LogLevel, because the ServerLogger-Class adds timestamp and type to the passed strings.
        /// </summary>
        [TestMethod]
        public void LogLevelDebugTest()
        {
            ServerLogger.SetLogLevel(0);
            ServerLogger.WipeLogFile();

            //Visible for lvl 0 as "[yyyy-MM-dd HH:mm:ss] [0] DEBUG:"
            ServerLogger.LogDebug("");
            //Visible for lvl 0, 1 as "[yyyy-MM-dd HH:mm:ss] [0] INFO:"
            ServerLogger.LogInformation("");
            //Visible for lvl 0, 1, 2 as "[yyyy-MM-dd HH:mm:ss] [0] WARNING:"
            ServerLogger.LogWarning("");
            //Visible for lvl 0, 1, 2, 3 as "[yyyy-MM-dd HH:mm:ss] [0] ERROR:"
            ServerLogger.LogError("");

            //LogLevel 0 means Errors, Warnings, Informations and Debugs
            string[] loggedLines = ServerLogger.LogFileToString().Split("\n");
            Assert.IsTrue(Regex.IsMatch(loggedLines[0], logDebugPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[1], logInformationPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[2], logWarningPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[3], logErrorPattern));
            ServerLogger.WipeLogFile();
        }

        /// <summary>
        /// Checks whether the method behaves robustly with miscellaneous passed parameters.
        /// </summary>
        [TestMethod]
        public void ValidLoggingOutputTypeInputTest()
        {
            ServerLogger.ChangeLoggingOutputType(2);
            Assert.AreEqual(2, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(1);
            Assert.AreEqual(1, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(0);
            Assert.AreEqual(0, Settings.Default.LogOutPutType);
        }

        [TestMethod]
        public void InvalidLoggingOutputTypeInputTest()
        {
            //Incorret Inputs
            ServerLogger.ChangeLoggingOutputType(3);
            //LogOutputType was set to 0 by the Initialize()-Method
            Assert.AreEqual(0, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(-1);
            Assert.AreEqual(0, Settings.Default.LogOutPutType);
        }

        /// <summary>
        /// Checks whether the logs are written to a file as specified by the LogOutput-Type 0.
        /// </summary>
        [TestMethod]
        public void LoggingOutputToFile()
        {
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.WipeLogFile();
            //LogOutputType 0 -> logs are only written into File
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                ServerLogger.LogError("");
                //Something was written in the file?
                Assert.IsTrue(Regex.IsMatch(ServerLogger.LogFileToString(), logErrorPattern));
                //Nothing was written in the console?
                Assert.IsFalse(Regex.IsMatch(stringWriter.ToString(), logErrorPattern));
            }
        }


        /// <summary>
        /// Checks whether the logs are written to a file and console as specified by the LogOutput-Type 2.
        /// </summary>
        [TestMethod]
        public void LoggingOutputToFileAndConsole()
        {
            ServerLogger.ChangeLoggingOutputType(2);
            ServerLogger.WipeLogFile();
            //LogOutputType 2 -> logs are written into File and Console
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                ServerLogger.LogError("");
                //Something was written in the file?
                Assert.IsTrue(Regex.IsMatch(ServerLogger.LogFileToString(), logErrorPattern));
                //Something was written in the console?
                Assert.IsTrue(Regex.IsMatch(stringWriter.ToString(), logErrorPattern));
            }
        }

        /// <summary>
        /// Checks whether the logs are written to console as specified by the LogOutput-Type 1.
        /// </summary>
        [TestMethod]
        public void LoggingOutputToConsole()
        {
            ServerLogger.ChangeLoggingOutputType(1);
            ServerLogger.WipeLogFile();
            //LogOutputType 2 -> logs are only written into Console
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                ServerLogger.LogError("");
                //Nothing was written in the file?
                Assert.IsFalse(Regex.IsMatch(ServerLogger.LogFileToString(), logErrorPattern));
                //Something was written in the console?
                Assert.IsTrue(Regex.IsMatch(stringWriter.ToString(), logErrorPattern));
            }
        }
    }
}