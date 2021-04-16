using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fleck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using ServerLogic.Control;
using Pose;
using ServerLogic.Model.Messages;
using ServerLogic.Properties;
using Shim = Pose.Shim;

namespace ServerLogicTest.Control
{
    [TestClass]
    public class MainServerLogicTest
    {
        private Shim playerAudienceApiShim;
        private Shim webSocketShim;
        private MainServerLogic mainServerLogic;
        private const string openSessionFormat = "";

        [TestInitialize]
        public void Initialize()
        {
            Settings.Default.LogFilePath = "TestLog.txt";
            ServerLogger.CreateServerLogger();
            ServerLogger.SetLogLevel(0);
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.WipeLogFile();
            mainServerLogic = new MainServerLogic();
        }

        [TestCleanup]
        public void CleanUp()
        {
            ServerLogger.WipeLogFile();
        }

        [TestMethod]
        [TestCategory("HelperMethods")]
        public void CheckGeneratedSessionKeyFormat()
        {
            //The Format specified in FR54
            Assert.IsTrue(Regex.IsMatch(mainServerLogic.GenerateSessionKey(4), @"[A-Z0-9]{6}"));
        }

        [TestMethod]
        [TestCategory("HelperMethods")]
        public void CheckModeratorClientIsKickedAfterThreeViolations()
        {
            Guid mc1 = new Guid();
            mainServerLogic._connectedModeratorClients.Add(mc1, new ModeratorClientManager(mc1, null, null));
            mainServerLogic.AddStrike(mc1);
            Assert.AreEqual(mainServerLogic._connectedModeratorClients[mc1].Strikes, 1);

            mainServerLogic.AddStrike(mc1);
            mainServerLogic.AddStrike(mc1);
            Assert.IsFalse(mainServerLogic._connectedModeratorClients.TryGetValue(mc1,out _));
        }

        /// <summary>
        /// Checks the correct format of the SessionOpenedMessage.
        /// </summary>
        [TestMethod]
        public void CheckValidOpenSessionResponseFormat()
        {
            Guid mc1 = new Guid();
            mainServerLogic._connectedModeratorClients.Add(mc1, new ModeratorClientManager(mc1, null, null));
            string response =
                mainServerLogic.CheckStringMessage(
                    JsonConvert.SerializeObject(new RequestOpenSessionMessage(mc1, "!Password123#")));
            Assert.AreEqual(response, openSessionFormat);
            //todo add tests for session already exists and wrong password
        }
    }
}
