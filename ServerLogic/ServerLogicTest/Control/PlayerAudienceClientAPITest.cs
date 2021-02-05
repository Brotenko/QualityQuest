using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Control;
using PAClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServerLogicTests.Control
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public sealed class PlayerAudienceClientAPITest
    {
        private PlayerAudienceClientAPI p;
        private const int testPort = 7777;
        private const string testKey_1 = "TU7ROU";
        private const string testKey_2 = "G9EL40";
        private const string testKey_Invalid_1 = "7g/vß`";
        private KeyValuePair<Guid, string> testPrompt_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "This is a test prompt!");
        private KeyValuePair<Guid, string> testPrompt_NullString = new KeyValuePair<Guid, string>(Guid.NewGuid(), null);
        private KeyValuePair<Guid, string> testPrompt_Valid_2 = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Another very valid prompt!");
        private static KeyValuePair<Guid, string> testPair_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "Nothing");
        private static KeyValuePair<Guid, string> testPair_Valid_2 = KeyValuePair.Create(Guid.NewGuid(), "Quite a lot");
        private static KeyValuePair<Guid, string> testPair_Valid_3 = KeyValuePair.Create(Guid.NewGuid(), "Perhaps a little something");
        private static KeyValuePair<Guid, string> testPair_Valid_4 = KeyValuePair.Create(Guid.NewGuid(), "Everything");
        private static KeyValuePair<Guid, string> testPair_Valid_5 = KeyValuePair.Create(Guid.NewGuid(), "This one is not supposed to work! 451%51s0-.ÜPOK$%");
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

        /************************ StartServer Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartServer"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_ValidInputTest()
        {
            p = new PlayerAudienceClientAPI();

            p.DebugStartServer(testPort);
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartServer"/> method
        /// throws an <see cref="InvalidOperationException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartServer")]
        public void StartServer_ServerActiveTest()
        {
            p = new PlayerAudienceClientAPI();

            p.DebugStartServer(testPort);
            Assert.ThrowsException<InvalidOperationException>(() => p.DebugStartServer(testPort));
        }

        /************************ StartNewSession Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewSession"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_ValidInputTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            p.StartNewSession(testKey_1);

            Assert.AreEqual(testKey_1, PABackend.PAVotingResults.GetSessionKeys()[0]);
            Assert.IsNotNull(PABackend.ConnectionList.GetValueOrDefault(testKey_1));
            Assert.IsInstanceOfType(PABackend.ConnectionList.GetValueOrDefault(testKey_1), typeof(List<string>));
            Assert.AreEqual(0, PABackend.ConnectionList.GetValueOrDefault(testKey_1).Count);
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewSession"/> method
        /// throws an <see cref="ArgumentException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_InvalidSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            Assert.ThrowsException<ArgumentException>(() => p.StartNewSession(testKey_Invalid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewSession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_NullSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.StartNewSession(null));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewSession"/> method
        /// throws an <see cref="ArgumentException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_SameSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            p.StartNewSession(testKey_1);
            Assert.ThrowsException<ArgumentException>(() => p.StartNewSession(testKey_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewSession"/> method
        /// throws an <see cref="InvalidOperationException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_ServerInactiveTest()
        {
            p = new PlayerAudienceClientAPI();

            Assert.ThrowsException<InvalidOperationException>(() => p.StartNewSession(testKey_1));
        }

        /************************ StartNewVote Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_ValidInputTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
        }


        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_InvalidSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<SessionNotFoundException>(() => p.StartNewVote(testKey_2, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_NullSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(null, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws an <see cref="ArgumentException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_SameValidPromptTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws an <see cref="ArgumentException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_DoubleAssignPromptTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_2));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_NullStringPromptTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(testKey_1, testPrompt_NullString, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_SameValidOptionsTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await p.StartNewVote(testKey_1, testPrompt_Valid_2, testOptions_Valid_1);
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_NullOptionsTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, null));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_NullStringOptionTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Invalid));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StartNewVote"/> method
        /// throws an <see cref="InvalidOperationException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task StartNewVote_InactiveServerTest()
        {
            p = new PlayerAudienceClientAPI();

            await Assert.ThrowsExceptionAsync<InvalidOperationException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /************************ GetVotingResult Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.GetVotingResult"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_ValidInputTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            // TODO
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.GetVotingResult"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_InvalidSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            Assert.ThrowsException<SessionNotFoundException>(() => p.GetVotingResult(testKey_1, testPrompt_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.GetVotingResult"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_NullSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.GetVotingResult(null, testPrompt_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.GetVotingResult"/> method
        /// throws an <see cref="ArgumentException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_InvalidPromptTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            Assert.ThrowsException<ArgumentException>(() => p.GetVotingResult(testKey_1, testPrompt_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.GetVotingResult"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_NullPromptDescriptionTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.GetVotingResult(testKey_1, testPrompt_NullString));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.GetVotingResult"/> method
        /// throws an <see cref="InvalidOperationException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_InactiveServerTest()
        {
            p = new PlayerAudienceClientAPI();

            Assert.ThrowsException<InvalidOperationException>(() => p.GetVotingResult(testKey_1, testPrompt_Valid_1));
        }

        /************************ EndSession Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.EndSession"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_ValidInputTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);
            p.StartNewSession(testKey_1);

            Assert.IsNotNull(p.EndSession(testKey_1));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.EndSession"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_InvalidSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            Assert.ThrowsException<SessionNotFoundException>(() => p.EndSession(testKey_2));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.EndSession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_NullSessionkeyTest()
        {
            p = new PlayerAudienceClientAPI();
            p.DebugStartServer(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.EndSession(null));
        }

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.EndSession"/> method
        /// throws an <see cref="InvalidOperationException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_InactiveTest()
        {
            p = new PlayerAudienceClientAPI();

            Assert.ThrowsException<InvalidOperationException>(() => p.EndSession(testKey_1));
        }

        /************************ StopServer Test ****************************/

        /// <summary>
        /// Validates that the <see cref="PlayerAudienceClientAPI.StopServer"/> method
        /// throws an <see cref="InvalidOperationException"/> exception when
        /// </summary>
        [TestMethod]
        [TestCategory("StopServer")]
        public void StopServer_InactiveServerTest()
        {
            p = new PlayerAudienceClientAPI();

            Assert.ThrowsException<InvalidOperationException>(() => p.StopServer());
        }
    }
}