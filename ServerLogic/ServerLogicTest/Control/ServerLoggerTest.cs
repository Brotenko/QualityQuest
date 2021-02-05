using System;
using System.IO;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Control;
using ServerLogic.Properties;



namespace ServerLogicTest.Control
{
    [TestClass]
    public class ServerLoggerTest
    {
        private const string logErrorPattern = @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sERROR:";
        private const string logWarningPattern = @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sWARNING:";
        private const string logInformationPattern = @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sINFO:";
        private const string logDebugPattern = @"\[[0-9]{4}-[0-9]{2}-[0-9]{2}\s[0-9]{2}:[0-9]{2}:[0-9]{2}(\.[0-9]{1,3})?]\s\[[0-4]+]\sDEBUG:";
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
        }

        /// <summary>
        /// Deletes any log files that may have been created after the tests have been executed.
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
        public void LogLevelInputTest()
        {
            //Legitimate value
            ServerLogger.SetLogLevel(0);
            Assert.AreEqual(0,Settings.Default.LogLevel);
            //illegitimate value
            ServerLogger.SetLogLevel(5);
            Assert.AreEqual(0, Settings.Default.LogLevel);
            //illegitimate value
            ServerLogger.SetLogLevel(-1);
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
        /// Checks if the relevant entries are recorded according to the current LogLevel.
        /// </summary>
        [TestMethod]
        public void LogLevelOutputTest()
        {
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.SetLogLevel(4);
            ServerLogger.WipeLogFile();
            //Visible for lvl 0,1,2,3
            ServerLogger.LogDebug("");
            //Visible for lvl 0,1,2
            ServerLogger.LogInformation("");
            //Visible for lvl 0,1
            ServerLogger.LogWarning("");
            //Visible for lvl 0
            ServerLogger.LogError("");
            //LogLevel 4 means that logger is deactivated, so nothing should come back.
            Assert.AreEqual("",ServerLogger.LogFileToString());

            ServerLogger.SetLogLevel(3);
            ServerLogger.WipeLogFile();
            //Visible for lvl 0,1,2,3
            ServerLogger.LogDebug("");
            //Visible for lvl 0,1,2
            ServerLogger.LogInformation("");
            //Visible for lvl 0,1
            ServerLogger.LogWarning("");
            //Visible for lvl 0
            ServerLogger.LogError("");
            //LogLevel 3 means only Errors
            Assert.IsTrue(Regex.IsMatch(ServerLogger.LogFileToString() , logErrorPattern));

            ServerLogger.SetLogLevel(2);
            ServerLogger.WipeLogFile();
            //Visible for lvl 0,1,2,3
            ServerLogger.LogDebug("");
            //Visible for lvl 0,1,2
            ServerLogger.LogInformation("");
            //Visible for lvl 0,1
            ServerLogger.LogWarning("");
            //Visible for lvl 0
            ServerLogger.LogError("");
            //LogLevel 2 means only Errors and Warnings
            string[] loggedLines = ServerLogger.LogFileToString().Split("\n");
            Assert.IsTrue(Regex.IsMatch(loggedLines[0], logWarningPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[1], logErrorPattern));

            ServerLogger.SetLogLevel(1);
            ServerLogger.WipeLogFile();
            //Visible for lvl 0,1,2,3
            ServerLogger.LogDebug("");
            //Visible for lvl 0,1,2
            ServerLogger.LogInformation("");
            //Visible for lvl 0,1
            ServerLogger.LogWarning("");
            //Visible for lvl 0
            ServerLogger.LogError("");
            //LogLevel 1 means Errors, Warnings and Informations
            loggedLines = ServerLogger.LogFileToString().Split("\n");
            Assert.IsTrue(Regex.IsMatch(loggedLines[0], logInformationPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[1], logWarningPattern));
            Assert.IsTrue(Regex.IsMatch(loggedLines[2], logErrorPattern));

            ServerLogger.SetLogLevel(0);
            ServerLogger.WipeLogFile();
            //Visible for lvl 0,1,2,3
            ServerLogger.LogDebug("");
            //Visible for lvl 0,1,2
            ServerLogger.LogInformation("");
            //Visible for lvl 0,1
            ServerLogger.LogWarning("");
            //Visible for lvl 0
            ServerLogger.LogError("");
            //LogLevel 1 means Errors, Warnings, Informations and Debugs
            loggedLines = ServerLogger.LogFileToString().Split("\n");
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
        public void LoggingOutputTypeInputTest()
        {
            //Correct Inputs
            ServerLogger.ChangeLoggingOutputType(2);
            Assert.AreEqual(2, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(1);
            Assert.AreEqual(1, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(0);
            Assert.AreEqual(0, Settings.Default.LogOutPutType);

            //Incorret Inputs
            ServerLogger.ChangeLoggingOutputType(3);
            Assert.AreEqual(0, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(-1);
            Assert.AreEqual(0, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(421);
            Assert.AreEqual(0, Settings.Default.LogOutPutType);
            ServerLogger.ChangeLoggingOutputType(-545);
            Assert.AreEqual(0, Settings.Default.LogOutPutType);
        }

        /// <summary>
        /// Checks whether the logs are output according to the LogOutputType.
        /// </summary>
        [TestMethod]
        public void LoggingOutputTypeOutputTest()
        {
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.SetLogLevel(0);
            ServerLogger.WipeLogFile();
            //LogOutputType 0 -> logs are only written into File
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ServerLogger.LogError("");
                //File is true
                Assert.IsTrue(Regex.IsMatch(ServerLogger.LogFileToString(), logErrorPattern));
                //console is false
                Assert.IsFalse(Regex.IsMatch(sw.ToString(), logErrorPattern));
            }
            
            
            ServerLogger.ChangeLoggingOutputType(2);
            ServerLogger.WipeLogFile();
            //LogOutputType 2 -> logs are written into File and Console
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ServerLogger.LogError("");
                //File is true
                Assert.IsTrue(Regex.IsMatch(ServerLogger.LogFileToString(), logErrorPattern));
                //Console is true
                Assert.IsTrue(Regex.IsMatch(sw.ToString(), logErrorPattern));
            }


            ServerLogger.ChangeLoggingOutputType(1);
            ServerLogger.WipeLogFile();
            //LogOutputType 2 -> logs are only written into Console
            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);
                ServerLogger.LogError("");
                //File is False
                Assert.IsFalse(Regex.IsMatch(ServerLogger.LogFileToString(), logErrorPattern));
                //Terminal is True
                Assert.IsTrue(Regex.IsMatch(sw.ToString(), logErrorPattern));
            }
        }
    }
}
