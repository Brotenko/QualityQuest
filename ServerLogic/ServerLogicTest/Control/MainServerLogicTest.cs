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
using ServerLogic.Model;
using ServerLogic.Model.Messages;
using ServerLogic.Properties;
using Shim = Pose.Shim;

namespace ServerLogicTest.Control
{
    [TestClass]
    public class MainServerLogicTest
    {
        private MainServerLogic mainServerLogic;

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
            mainServerLogic.Stop();
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
            Guid mc1 = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(mc1, new ModeratorClientManager(mc1, null, null));
            
            mainServerLogic.AddStrike(mc1);
            mainServerLogic.AddStrike(mc1);
            mainServerLogic.AddStrike(mc1);

            Assert.AreEqual(mainServerLogic._connectedModeratorClients[mc1].Strikes, 1);
            Assert.IsFalse(mainServerLogic._connectedModeratorClients.TryGetValue(mc1,out _));
        } //TODO check for clearing of strikes after sending valid message

        /// <summary>
        /// Checks if a openSession-Message is returned when using a valid Password.
        /// </summary>
        [TestMethod]
        [TestCategory("MessageHandling")]
        public void CheckValidOpenSessionPasswordAndValidSessionResponse()
        {
            Guid mc1 = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(mc1, new ModeratorClientManager(mc1, null, null));
            //Since Settings.Default.Save() is not used, the password is only changed for the instance of this test. 
            string password = "Password!123#";
            Settings.Default.PWHash = ServerShell.StringToSHA256Hash(password);
            
            string response = mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestOpenSessionMessage(mc1, password)));
            MessageContainer responseAsObject = JsonConvert.DeserializeObject<MessageContainer>(response);
            SessionOpenedMessage sessionOpenedMessage = JsonConvert.DeserializeObject<SessionOpenedMessage>(response);

            Assert.AreEqual(MessageType.SessionOpened, responseAsObject.Type);
            Assert.AreEqual(mainServerLogic._connectedModeratorClients[mc1].SessionKey, sessionOpenedMessage.SessionKey);
        }

        [TestMethod]
        [TestCategory("MessageHandling")]
        public void CheckValidOpenSessionPasswordAndAlreadyExistingSessionResponse()
        {
            Guid mc1 = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(mc1, new ModeratorClientManager(mc1, null, null));
            //Since Settings.Default.Save() is not used, the password is only changed for the instance of this test. 
            string password = "Password!123#";
            Settings.Default.PWHash = ServerShell.StringToSHA256Hash(password);
            //registration of a session with mc1
            mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestOpenSessionMessage(mc1, password)));

            //Try registration again with same ModeratorGuid
            string response = mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestOpenSessionMessage(mc1, password)));
            MessageContainer responseAsObject = JsonConvert.DeserializeObject<MessageContainer>(response);
            SessionOpenedMessage sessionOpenedMessage = JsonConvert.DeserializeObject<SessionOpenedMessage>(response);

            Assert.AreEqual(ErrorType.WrongSession, responseAsObject.Type);
            Assert.AreEqual(mainServerLogic._connectedModeratorClients[mc1].SessionKey, sessionOpenedMessage.SessionKey);
        }

        [TestMethod]
        [TestCategory("MessageHandling")]
        public void CheckRequestGameStartMessageJsonConversion()
        {
            Guid mc1 = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(mc1, new ModeratorClientManager(mc1, null, null));
            
            string response = mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestGameStartMessage(mc1)));
            MessageContainer responseAsObject = JsonConvert.DeserializeObject<MessageContainer>(response);

            Assert.AreEqual(MessageType.GameStarted, responseAsObject.Type);
            Assert.AreEqual( mc1, responseAsObject.ModeratorID);
        }
    }
}
