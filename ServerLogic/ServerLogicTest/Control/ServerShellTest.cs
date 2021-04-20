﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Control;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;
using System.IO.Abstractions.TestingHelpers;
using ServerLogic.Properties;

namespace ServerLogicTests.Control
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public sealed class ServerShellTest
    {
        private const string portCommandPattern = @"(usage: port)";
        private const string passwordCommandPattern = @"(usage: password)";
        private const string startCommandPattern = @"(usage: start)";
        private const string stopCommandPattern = @"(usage: stop)";
        private const string versionCommandPattern = @"(usage: version)";
        private const string helpCommandPattern = @"(usage: qq)";
        private const string logCommandPattern = @"(usage: log)";
        private const string exitCommandPattern = @"(usage: exit)";
        private const string passwordExceptionPattern = @"(Please make sure to set a password)";
        private const string portExceptionPattern = @"(Please make sure the port)";
        private const string versionPattern = @"(v\d+.\d+.\d+)";
        private const string serverIsRunningPattern = @"The server is running.";
        private const string serverHasStartedPattern = @"The server has been started successfully with port: \d{1,5}";
        private const string portSetPattern = @"The port has been set to \d{1,5} successfully.";
        private const string passwordSetPattern = @"The password has been set to \S+ successfully.";
        private const string invalidCommandPattern = @"'\S+' is not a valid command. See 'help'.";

        private const string testPassword_1 = "!Password456#";
        private const string testPassword_2 = "!Password789987?";
        private const string testPassword_3 = "!PasswordDifferent123132#";
        private const int testPort_1 = 5555;
        private const int testPort_2 = 6666;
        private const int testPort_3 = 8888;


        /// <summary>
        /// Sets the path variable for the log file from the settings to a test log
        /// file to prevent changes to the actual log file by the tests.
        /// Since the changed value is not saved, the actual settings value is only
        /// temporarily overwritten and has the original value again the next time the application is started.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            ServerLogic.Properties.Settings.Default.LogFilePath = "TestLog.txt";
            ServerLogger.CreateServerLogger();
        }

        /// <summary>
        /// Deletes any log files that may have been created after the tests have been executed.
        /// As a precaution, a ServerLogger instance is created to avoid a corresponding exception should none have been created by the tests. 
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            ServerLogger.CreateServerLogger();
            ServerLogger.WipeLogFile();
        }


        /// <summary>
        /// Validates that an <c>ArgumentException</c> is thrown when a port is
        /// transmitted that violates the port rules.
        /// </summary>
        [TestMethod]
        public void IllegalPortTest()
        {
            // Port is a negative value
            Assert.ThrowsException<ArgumentException>(() => new ServerShell("!Password123", -2500));

            // Port is in "system port" range
            Assert.ThrowsException<ArgumentException>(() => new ServerShell("!Password123", 782));

            // Port is outside port range (port > 65535)
            Assert.ThrowsException<ArgumentException>(() => new ServerShell("!Password123", 65536));
        }

        /// <summary>
        /// Validates that the <c>ParseCommand</c> method acts like inteded, when 
        /// commands and options are entered.
        /// </summary>
        [TestMethod]
        public void ParseCommandTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - commandParameters.Length == 0
             * 
             * What it does:
             * Return the help message for "help".
             */
            string t = s.ParseCommandDebugger("help");
            Assert.IsTrue(Regex.IsMatch(t, helpCommandPattern));

            /*
             * Tests for the following conditions:
             * - commandParameters.Length != 0
             * - commandParameters[0] != "--help"
             * 
             * What it does:
             * Return the help message for "help" with the commandParameter
             * "5m%3mEt9#", which will be discarded by the function.
             */
            t = s.ParseCommandDebugger("help 5m%3mEt9#");
            Assert.IsTrue(Regex.IsMatch(t, helpCommandPattern));

            /* 
             * Tests for the following conditions:
             * - commandParameters.Length != 0
             * - commandParameters[0] == "--help"
             * 
             * What it does:
             * Return the help message for "help".
             */
            t = s.ParseCommandDebugger("help --help");
            Assert.IsTrue(Regex.IsMatch(t, helpCommandPattern));
        }

        /// <summary>
        /// Validates that <the c>ParsePort</c> method acts like inteded, when certain 
        /// arguments are transmitted.
        /// </summary>
        [TestMethod]
        public void ParsePortTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - commandList.Length == 0
             * 
             * What it does:
             * Returns the currently set port, which is always 7777 (standard port).
             */
            string t = s.ParseCommandDebugger("port");
            Assert.AreEqual(Settings.Default.PAWebPagePort, Convert.ToInt32(t, CultureInfo.CurrentCulture));

            /*
             * Tests for the following conditions:
             * - commandList.Length != 0
             * - port is not a numeral
             * 
             * What it does:
             * A "FormatException" is thrown that is caught to return an
             * "InvalidPortExceptionMessage" to inform the user that the port  
             * was not in form of a numeral.
             */
            t = s.ParseCommandDebugger("port 2%75Gz_f#");
            Assert.IsTrue(Regex.IsMatch(t, portExceptionPattern));

            /*
             * Tests for the following conditions:
             * - commandList.Length != 0
             * - port is outside the settable range for ports
             * 
             * What it does:
             * An "ArugmentException" is thrown that is caught to return an
             * "InvalidPortExceptionMessage" to inform the user that the port
             * was outside the settable range (aka: 1025-65535).
             */
            t = s.ParseCommandDebugger("port 8320870");
            Assert.IsTrue(Regex.IsMatch(t, portExceptionPattern));

            /*
             * Tests for the following conditions:
             * - commandList.Length != 0
             * - port is inside the settable range for ports
             * 
             * What it does:
             * Sets the port of the ServerLogic to 5555 and returns a
             * confirmation message for the user.
             */
            t = s.ParseCommandDebugger("port " + testPort_1);
            Assert.IsTrue(Regex.IsMatch(t, portSetPattern));
            Assert.AreEqual(testPort_1, s.Port);

            /*
             * Tests for the following conditions:
             * - commandList.Length != 0
             * - password does not violate the password rules
             * - more than one argument is being transmitted
             * 
             * What it does:
             * Sets the port of the ServerLogic to 5555 and returns a
             * confirmation message for the user without regarding the 
             * excess arguments.
             */
            t = s.ParseCommandDebugger("port " + testPort_2 + " g:78G#f)_F75f 2222 245664840320");
            Assert.IsTrue(Regex.IsMatch(t, portSetPattern));
            Assert.AreEqual(testPort_2, s.Port);

            /*
             * Tests for the following conditions:
             * - commandList.Length != 0
             * - serverIsRunning == true
             * 
             * What it does:
             * Returns a message, informing the user that the server is currently
             * running, and returns before even trying to set the port.
             */
            s.ParseCommandDebugger("start");
            t = s.ParseCommandDebugger("port " + testPort_3);
            Assert.IsTrue(Regex.IsMatch(t, serverIsRunningPattern));
            Assert.AreNotEqual(testPort_3, s.Port);
        }

        /// <summary>
        /// Validates that the <c>StartServer</c> method acts like inteded, 
        /// when certain options are transmitted.
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_ParameterlessTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - commandList is empty 
             * 
             * What it does:
             * Starts the ServerLogic with previously set port and password.
             */
            string t = s.ParseCommandDebugger("start");
            Assert.IsTrue(Regex.IsMatch(t, serverHasStartedPattern));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_ValidPortTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - an element of commandList matches "--port\=(\d*)"
             * 
             * What it does:
             * Starts the ServerLogic with previously set password and
             * the newly set port.
             */
            string t = s.ParseCommandDebugger("start --port=" + testPort_1);
            Assert.IsTrue(Regex.IsMatch(t, serverHasStartedPattern));
            Assert.AreEqual(testPort_1, s.Port);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_InvalidPortTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - port is outside the settable range for ports or non-numerical
             * 
             * What it does:
             * An "ArgumentException" is thrown that is caught to return 
             * an "InvalidPortExceptionMessage" to inform the user that
             * the port was not in adequate form.
             */
            string t = s.ParseCommandDebugger("start --port=4g%gL#1");
            Assert.IsTrue(Regex.IsMatch(t, portExceptionPattern));
        }


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_InvalidOptionTest()
        {
            ServerShell s = ServerShell.DebugServerShell();


            /*
             * Tests for the following conditions:
             * - unknown option used
             * 
             * What it does:
             * Starts the ServerLogic with previously set port and password,
             * disregarding the unknown options.
             */
            string t = s.ParseCommandDebugger("start --h#4tH2&5=h78/tj/)");
            Assert.IsTrue(Regex.IsMatch(t, serverHasStartedPattern));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_ValidPasswordAndPortTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - an element of commandList matches "--port\=(\d*)"
             * - an element of commandList matches "--password\=(\S*)"
             * - the order of considered options is "Password", then "Port"
             * 
             * What it does:
             * Starts the ServerLogic with newly set port and password,
             * disregarding the order of the options provided.
             */
            string t = s.ParseCommandDebugger("start --password=" + testPassword_2 + " --port=" + testPort_2);
            Assert.IsTrue(Regex.IsMatch(t, serverHasStartedPattern));
            Assert.AreEqual(testPort_2, s.Port);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_ValidPortAndPasswordTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - an element of commandList matches "--port\=(\d*)"
             * - an element of commandList matches "--password\=(\S*)"
             * - the order of considered options is "Port", then "Password"
             * 
             * What it does:
             * Starts the ServerLogic with newly set port and password,
             * disregarding the order of the options provided.
             */
            string t = s.ParseCommandDebugger("start --port=" + testPort_3 + " --password=" + testPassword_3);
            Assert.IsTrue(Regex.IsMatch(t, serverHasStartedPattern));
            Assert.AreEqual(testPort_3, s.Port);
        }

        /// <summary>
        /// Validates that the <c>StopServer</c> method acts like inteded, 
        /// when certain options are transmitted.
        /// </summary>
        [TestMethod]
        public void StopServerTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - the command "stop" is entered without any additional options
             * 
             * What it does:
             * Stops the running server.
             */
            s.ParseCommandDebugger("stop");
        }

        /// <summary>
        /// Validates that the <c>ShowHelp</c> method acts like inteded, when 
        /// certain options are transmitted.
        /// </summary>
        [TestMethod]
        public void ShowHelpTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - the command "help" is entered without any additional options
             * 
             * What it does:
             * Returns the help message for the general program.
             */
            string t = s.ParseCommandDebugger("help");
            Assert.IsTrue(Regex.IsMatch(t, helpCommandPattern));

            /*
             * Tests for the following conditions:
             * - the command "help" in entered with additional options
             * 
             * What it does:
             * Returns the help message for the general program, disregarding the
             * additional options.
             */
            t = s.ParseCommandDebugger("help --help");
            Assert.IsTrue(Regex.IsMatch(t, helpCommandPattern));

            /*
             * Tests for the following conditions:
             * - the command "help" in entered with additional options
             * 
             * What it does:
             * Returns the help message for the general program, disregarding the
             * additional options.
             */
            t = s.ParseCommandDebugger("help help");
            Assert.IsTrue(Regex.IsMatch(t, helpCommandPattern));

            /*
             * Tests for the following conditions:
             * - the command "help" in entered together with an unknown option
             * 
             * What it does:
             * Returns the help message for the general program, disregarding the
             * additional options.
             */
            t = s.ParseCommandDebugger("help t&dk4#g8");
            Assert.IsTrue(Regex.IsMatch(t, helpCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "port" is transmitted
             * 
             * What it does:
             * Returns the help message for the "port" command.
             */
            t = s.ParseCommandDebugger("port --help");
            Assert.IsTrue(Regex.IsMatch(t, portCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "port" is transmitted with
             * some additional options.
             * 
             * What it does:
             * Returns the help message for the "port" command, disregarding the
             * additional options.
             */
            t = s.ParseCommandDebugger("port --help a4T&#4/ --z&v_4p=#");
            Assert.IsTrue(Regex.IsMatch(t, portCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "password" is transmitted
             * 
             * What it does:
             * Returns the help message for the "password" command.
             */
            t = s.ParseCommandDebugger("password --help");
            Assert.IsTrue(Regex.IsMatch(t, passwordCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "start" is transmitted
             * 
             * What it does:
             * Returns the help message for the "start" command.
             */
            t = s.ParseCommandDebugger("start --help");
            Assert.IsTrue(Regex.IsMatch(t, startCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "stop" is transmitted
             * 
             * What it does:
             * Returns the help message for the "stop" command.
             */
            t = s.ParseCommandDebugger("stop --help");
            Assert.IsTrue(Regex.IsMatch(t, stopCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "version" is transmitted
             * 
             * What it does:
             * Returns the help message for the "version" command.
             */
            t = s.ParseCommandDebugger("version --help");
            Assert.IsTrue(Regex.IsMatch(t, versionCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "log" is transmitted
             * 
             * What it does:
             * Returns the help message for the "log" command.
             */
            t = s.ParseCommandDebugger("log --help");
            Assert.IsTrue(Regex.IsMatch(t, logCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for the command "exit" is transmitted
             * 
             * What it does:
             * Returns the help message for the "exit" command.
             */
            t = s.ParseCommandDebugger("exit --help");
            Assert.IsTrue(Regex.IsMatch(t, exitCommandPattern));

            /*
             * Tests for the following conditions:
             * - the "--help" option for an unknown command is transmitted
             * 
             * What it does:
             * Returns a message that informs the user that the transmitted
             * command is invalid.
             */
            t = s.ParseCommandDebugger("t#Gu7?4b --help");
            Assert.IsTrue(Regex.IsMatch(t, invalidCommandPattern));
        }

        /// <summary>
        /// Validates that the <c>ShowVersion</c> method acts like inteded, when 
        /// certain options are transmitted.
        /// </summary>
        [TestMethod]
        public void ShowVersionTest()
        {
            ServerShell s = ServerShell.DebugServerShell();

            /*
             * Tests for the following conditions:
             * - the command "version" is entered without any additional options
             * 
             * What it does:
             * Returns the current version of the ServerLogic.
             */
            string t = s.ParseCommandDebugger("version");
            Assert.IsTrue(Regex.IsMatch(t, versionPattern));

            /*
             * Tests for the following conditions:
             * - the command "version" is entered with additional options
             * 
             * What it does:
             * Returns the current version of the ServerLogic, disregarding the
             * additional options.
             */
            t = s.ParseCommandDebugger("version --h#5Tb?4 g7&2§f_ß 2557423412050");
            Assert.IsTrue(Regex.IsMatch(t, versionPattern));
        }

        /// <summary>
        /// Checks whether the previous level was retained if the format of the input was incorrect.
        /// </summary>
        [TestMethod]
        public void InvalidLogLevelInputFormatTest()
        {
            Settings.Default.LogFilePath = "TestLog.txt";
            ServerLogger.CreateServerLogger();
            ServerLogger.SetLogLevel(0);
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.WipeLogFile();
            ServerShell s = ServerShell.DebugServerShell();

            //Test for Input with a non-Integer
            s.ParseCommandDebugger("log --setLevel $");
            Assert.IsTrue(Settings.Default.LogLevel==0);

            //Test for Null-Input
            s.ParseCommandDebugger("log --setLevel");
            Assert.IsTrue(Settings.Default.LogOutPutType == 0);
            ServerLogger.WipeLogFile();
        }

        /// <summary>
        /// Checks whether the previous level was retained if the format of the input was incorrect.
        /// </summary>
        [TestMethod]
        public void InvalidLoggingOutputTypeInputFormatTest()
        {
            Settings.Default.LogFilePath = "TestLog.txt";
            ServerLogger.CreateServerLogger();
            ServerLogger.SetLogLevel(0);
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.WipeLogFile();
            ServerShell s = ServerShell.DebugServerShell();

            //Test for Input with a non-Integer
            s.ParseCommandDebugger("log --setLogOutput $");
            Assert.IsTrue(Settings.Default.LogOutPutType == 0);

            //Test for Null-Input
            s.ParseCommandDebugger("log --setLogOutput");
            Assert.IsTrue(Settings.Default.LogOutPutType == 0);
            ServerLogger.WipeLogFile();
        }
    }
}