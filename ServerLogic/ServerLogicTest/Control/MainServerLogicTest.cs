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
            mainServerLogic = new MainServerLogic();
            PlayerAudienceClientAPI pacApi = new PlayerAudienceClientAPI();
            mainServerLogic._playerAudienceClientApi = pacApi;
            pacApi.DebugStartServer(1111);
            ServerLogger.WipeLogFile();
        }

        [TestCleanup]
        public void CleanUp()
        {
            //mainServerLogic.Stop();
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
            Assert.AreEqual(mainServerLogic._connectedModeratorClients[mc1].Strikes, 1);

            mainServerLogic.AddStrike(mc1);
            mainServerLogic.AddStrike(mc1);

            Assert.IsFalse(mainServerLogic._connectedModeratorClients.TryGetValue(mc1, out _));
        } 

        [TestMethod]
        [TestCategory("HelperMethods")]
        public void CheckModeratorClientStrikesIsClearedAfterValidMessage()
        {
            Guid modClient = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(modClient, new ModeratorClientManager(modClient, null, null));

            mainServerLogic.AddStrike(modClient);
            mainServerLogic.AddStrike(modClient);
            Assert.AreEqual(mainServerLogic._connectedModeratorClients[modClient].Strikes, 2);

            mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestGameStartMessage(modClient)));
            Assert.AreEqual(mainServerLogic._connectedModeratorClients[modClient].Strikes, 0);
        }

        /// <summary>
        /// Checks if a openSession-Message is returned when using a valid Password.
        /// </summary>
        [TestMethod]
        [TestCategory("MessageHandling")]
        public void CheckValidOpenSessionPasswordAndValidSessionResponse()
        {
            Guid modClient = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(modClient, new ModeratorClientManager(modClient, null, mainServerLogic._playerAudienceClientApi));
            //Since Settings.Default.Save() is not used, the password is only changed for the instance of this test. 
            string password = "Password!123#";
            Settings.Default.PWHash = ServerShell.StringToSHA256Hash(password);

            string response = mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestOpenSessionMessage(modClient, password)));

            MessageContainer responseAsObject = JsonConvert.DeserializeObject<MessageContainer>(response);
            SessionOpenedMessage sessionOpenedMessage = JsonConvert.DeserializeObject<SessionOpenedMessage>(response);

            Assert.AreEqual(MessageType.SessionOpened, responseAsObject.Type);
            Assert.AreEqual(mainServerLogic._connectedModeratorClients[modClient].SessionKey, sessionOpenedMessage.SessionKey);
        }

        [TestMethod]
        [TestCategory("MessageHandling")]
        public void CheckValidOpenSessionPasswordAndAlreadyExistingSessionResponse()
        {
            Guid modClient = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(modClient, new ModeratorClientManager(modClient, null, mainServerLogic._playerAudienceClientApi));
            //Since Settings.Default.Save() is not used, the password is only changed for the instance of this test. 
            string password = "Password!123#";
            Settings.Default.PWHash = ServerShell.StringToSHA256Hash(password);
            //registration of a session with mc1
            mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestOpenSessionMessage(modClient, password)));

            //Try registration again with same ModeratorGuid
            string response = mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestOpenSessionMessage(modClient, password)));
            ErrorMessage responseAsObject = JsonConvert.DeserializeObject<ErrorMessage>(response);
            
            Assert.AreEqual(ErrorType.WrongSession, responseAsObject.ErrorMessageType);
        }

        [TestMethod]
        [TestCategory("MessageHandling")]
        public void CheckInvalidOpenSessionPasswordAndValidSessionResponse()
        {
            Guid modClient = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(modClient, new ModeratorClientManager(modClient, null, mainServerLogic._playerAudienceClientApi));
            //Since Settings.Default.Save() is not used, the password is only changed for the instance of this test. 
            string password = "Password!123#";
            Settings.Default.PWHash = ServerShell.StringToSHA256Hash(password);
            
            string response = mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestOpenSessionMessage(modClient, "isWrong")));

            ErrorMessage responseAsObject = JsonConvert.DeserializeObject<ErrorMessage>(response);

            Assert.AreEqual(ErrorType.WrongPassword, responseAsObject.ErrorMessageType);
        }

        [TestMethod]
        [TestCategory("MessageHandling")]
        public void CheckRequestGameStartMessageJsonConversion()
        {
            Guid modClient = Guid.NewGuid();
            mainServerLogic._connectedModeratorClients.Add(modClient, new ModeratorClientManager(modClient, null, null));

            string response = mainServerLogic.CheckStringMessage(JsonConvert.SerializeObject(new RequestGameStartMessage(modClient)));
            MessageContainer responseAsObject = JsonConvert.DeserializeObject<MessageContainer>(response);

            Assert.AreEqual(MessageType.GameStarted, responseAsObject.Type);
            Assert.AreEqual(modClient, responseAsObject.ModeratorID);
        }
    }
}
