using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Fleck;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Control;
using Pose;
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

        [TestInitialize]
        public void Initialize()
        {
            Settings.Default.LogFilePath = "TestLog.txt";
            ServerLogger.CreateServerLogger();
            ServerLogger.SetLogLevel(0);
            ServerLogger.ChangeLoggingOutputType(0);
            ServerLogger.WipeLogFile();
            //We don't need to really start a server, but 
            //playerAudienceApiShim = Shim.Replace(()=> new PlayerAudienceClientAPI().StartServer(7777)).With(delegate(PlayerAudienceClientAPI @this){/*todo or nothing?*/});
            //webSocketShim = Shim.Replace(()=> new WebSocketServer().Start());
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

        /// <summary>
        /// Checks whether the GenerateSessionKey method still terminates if for some reason the random generation is no longer random and therefore the adjustment to already generated session keys repeatedly fails.
        /// </summary>
        /*  [TestMethod]
          [TestCategory("HelperMethods")]
          public void CheckSessionKeyGenerationIsStoppedInCaseOfDeadlock()
          {
              //Random.Next will always return 1 
              Shim randomNotRandomShim = Shim.Replace(() => new Random().Next()).With(() => 1);
  
              //As random isn't random anymore, the generated sessionKey is always the same
              PoseContext.Isolate(() =>
              {
                  mainServerLogic._connectedModeratorClients.Add(null, new ModeratorClientManager(new Guid(), mainServerLogic.GenerateSessionKey(4), null, null));
                  mainServerLogic._connectedModeratorClients.Add(null, new ModeratorClientManager(new Guid(), mainServerLogic.GenerateSessionKey(4), null, null));
              }, randomNotRandomShim );
              //If the generation process was aborted because too many attempts were made, this should be logged.
              Assert.IsTrue(Regex.IsMatch(ServerLogger.LogFileToString(),@"Session-Key [A-Z0-9]{6} might be duplicate"));
          }*/
    }
}
