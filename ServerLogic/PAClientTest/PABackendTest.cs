using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;
using PAClient;
using System.Threading.Tasks;
using System.Threading;

namespace PAClientTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public sealed class PABackendTest
    {
        private const int testPort = 7777;
        private const string testKey_1 = "TU7ROU";
        private const string testKey_2 = "G9EL40";
        private const string testKey_3 = "GHOU80";
        private const string testKey_Invalid_1 = "7g/vﬂ`";
        private const string testId_Valid_1 = "sQxPVXaPUuoSV_2epIFMkw";
        private const string testId_Valid_2 = "7fJem-hO8gPE8v_4rZUg5a";
        private const string testId_Invalid_1 = "u?oS‰V_2epI7.=4gZUs%";
        private const string getSessionKeysComparison = testKey_1 + ", " + testKey_2 + ", " + testKey_3;
        private KeyValuePair<Guid, string> testPrompt_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "This is a test prompt!");
        private KeyValuePair<Guid, string> testPrompt_NullString = new KeyValuePair<Guid, string>(Guid.NewGuid(), null);
        private KeyValuePair<Guid, string> testPrompt_Valid_2 = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Another very valid prompt!");
        private static KeyValuePair<Guid, string> testPair_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "Nothing");
        private static KeyValuePair<Guid, string> testPair_Valid_2 = KeyValuePair.Create(Guid.NewGuid(), "Quite a lot");
        private static KeyValuePair<Guid, string> testPair_Valid_3 = KeyValuePair.Create(Guid.NewGuid(), "Perhaps a little something");
        private static KeyValuePair<Guid, string> testPair_Valid_4 = KeyValuePair.Create(Guid.NewGuid(), "Everything");
        private static KeyValuePair<Guid, string> testPair_Valid_5 = KeyValuePair.Create(Guid.NewGuid(), "This one is not supposed to work! 451%51s0-.‹POK$%");
        private static KeyValuePair<Guid, string> testPair_NullString = new KeyValuePair<Guid, string>(Guid.NewGuid(), null);
        private KeyValuePair<Guid, string>[] testOptions_Valid_1 = new KeyValuePair<Guid, string>[]
            {
                testPair_Valid_1,
                testPair_Valid_2,
                testPair_Valid_3,
                testPair_Valid_4
            };
        private KeyValuePair<Guid, string>[] testOptions_Valid_2 = new KeyValuePair<Guid, string>[]
            {
                testPair_Valid_5
            };
        private KeyValuePair<Guid, string>[] testOptions_Invalid = new KeyValuePair<Guid, string>[]
            {
                testPair_NullString
            };

        /// <summary>
        /// 
        /// </summary>
        [TestCleanup]
        public void ClearPAVotingResults()
        {
            // Re-init PABackend after every TestMethod to clear the PAVotingResults.
            // _ = new PABackend(testPort);
        }







        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetSessionKeys")]
        public void GetSessionKeys_EmptyArrayTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.AreEqual(0, p.GetSessionKeys().Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetSessionKeys")]
        public void GetSessionKeys_FilledArrayTest()
        {
            PABackend p = new PABackend(testPort);

            PABackend.PAVotingResults.AddSessionKey(testKey_1);
            Assert.AreNotEqual(0, p.GetSessionKeys().Length);
            Assert.AreEqual(testKey_1, p.GetSessionKeys()[0]);
        }






        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_ValidInputTest()
        {
            PABackend p = new PABackend(testPort);

            p.StartNewSession(testKey_1);

            Assert.AreEqual(testKey_1, PABackend.PAVotingResults.GetSessionKeys()[0]);
            Assert.IsNotNull(PABackend.ConnectionList.GetValueOrDefault(testKey_1));
            Assert.IsInstanceOfType(PABackend.ConnectionList.GetValueOrDefault(testKey_1), typeof(List<string>));
            Assert.AreEqual(0, PABackend.ConnectionList.GetValueOrDefault(testKey_1).Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_InvalidSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentException>(() => p.StartNewSession(testKey_Invalid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_NullSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.StartNewSession(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_SameSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);

            p.StartNewSession(testKey_1);
            Assert.ThrowsException<ArgumentException>(() => p.StartNewSession(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_ToStringCorrectness()
        {
            PABackend p = new PABackend(testPort);

            // TODO
        }








        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_ValidInputTest()
        {
            PABackend p = new PABackend(testPort);

            p.DebugCreatePageContent(testPrompt_Valid_1, testOptions_Valid_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_NullPromptDescriptionTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.DebugCreatePageContent(testPrompt_NullString, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_NullOptionsTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.DebugCreatePageContent(testPrompt_Valid_1, null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_NullOptionDescriptionTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.DebugCreatePageContent(testPrompt_Valid_1, testOptions_Invalid));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_ToStringCorrectness()
        {
            PABackend p = new PABackend(testPort);

            // TODO
        }









        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
        }

        
        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_InvalidSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);
            
            await Assert.ThrowsExceptionAsync<SessionNotFoundException>(() => p.SendPushMessage(testKey_2, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_NullSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.SendPushMessage(null, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_SameValidPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_DoubleAssignPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Valid_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_NullStringPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.SendPushMessage(testKey_1, testPrompt_NullString, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_SameValidOptionsTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await p.SendPushMessage(testKey_1, testPrompt_Valid_2, testOptions_Valid_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_NullOptionsTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.SendPushMessage(testKey_1, testPrompt_Valid_1, null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_NullStringOptionTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Invalid));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("SendPushMessage")]
        public async Task SendPushMessage_ToStringCorrectness()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            // TODO
        }
        











        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public async Task CountNewVote_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);
            await p.SendPushMessage(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.AreEqual((int) PABackendErrorType.NoError, PABackend.CountNewVote(testKey_1, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_InvalidSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidSessionkeyError, PABackend.CountNewVote(testKey_2, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_NullSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.NullSessionkeyError, PABackend.CountNewVote(null, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_NoSessionStartedTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.AreEqual((int) PABackendErrorType.InvalidSessionkeyError, PABackend.CountNewVote(testKey_1, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_MissingPromptTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidArgumentError, PABackend.CountNewVote(testKey_1, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_ToStringCorrectness()
        {
            PABackend p = new PABackend(testPort);

            // TODO
        }








        

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_ValidInputTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.IsTrue(Array.Exists(p.GetSessionKeys(), element => element == testKey_1));
            Assert.IsNotNull(p.EndSession(testKey_1));
            Assert.IsFalse(Array.Exists(p.GetSessionKeys(), element => element == testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_InvalidSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentException>(() => p.EndSession(testKey_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_NullSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.EndSession(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_ToStringCorrectness()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);


        }










        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_ValidInputTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.IsFalse(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
            Assert.AreEqual((int) PABackendErrorType.NoError, PABackend.AddConnection(testKey_1, testId_Valid_1));
            Assert.IsTrue(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_InvalidSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidSessionkeyError, PABackend.AddConnection(testKey_2, testId_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_NullSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.NullSessionkeyError, PABackend.AddConnection(null, testId_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_InvalidConnectionIdTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidConnectionIdError, PABackend.AddConnection(testKey_1, testId_Invalid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_NullConnectionIdTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.NullConnectionIdError, PABackend.AddConnection(testKey_1, null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_ToStringCorrectness()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            // TODO
        }










        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveConnection")]
        public void RemoveConnection_ValidInputTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);
            PABackend.AddConnection(testKey_1, testId_Valid_1);

            Assert.IsTrue(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
            Assert.AreEqual((int) PABackendErrorType.NoError, PABackend.RemoveConnection(testId_Valid_1));
            Assert.IsFalse(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveConnection")]
        public void RemoveConnection_InvalidConnectionIdTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.AreEqual((int) PABackendErrorType.InvalidConnectionIdError, PABackend.RemoveConnection(testId_Valid_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveConnection")]
        public void RemoveConnection_NullConnectionIdTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.AreEqual((int) PABackendErrorType.NullConnectionIdError, PABackend.RemoveConnection(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveConnection")]
        public void RemoveConnection_ToString()
        {
            PABackend p = new PABackend(testPort);

            // TODO
        }







        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_ValidInputTest()
        {
            PABackend p = new PABackend(testPort);


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_InvalidSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<SessionNotFoundException>(() => p.GetVotingResult(testKey_1, testPrompt_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_NullSessionkeyTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.GetVotingResult(null, testPrompt_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_InvalidPromptTest()
        {
            PABackend p = new PABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.ThrowsException<ArgumentException>(() => p.GetVotingResult(testKey_1, testPrompt_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_NullPromptDescriptionTest()
        {
            PABackend p = new PABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.GetVotingResult(testKey_1, testPrompt_NullString));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_ToStringCorrectness()
        {
            PABackend p = new PABackend(testPort);

            // TODO
        }
    }
}
