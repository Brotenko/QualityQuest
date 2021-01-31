using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using ServerLogic.Control;
using PAClient;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;

namespace ServerLogicTests.Control
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public sealed class PlayerAudienceClientAPITest
    {
        private const int testPort = 7777;
        private const string testKey_1 = "TU7ROU";
        private const string testKey_2 = "d5er0b";
        private const string testKey_3 = "GH&b_Z";
        private KeyValuePair<Guid, string> testPrompt_1 = KeyValuePair.Create(Guid.NewGuid(), "This is a test prompt!");
        private KeyValuePair<Guid, string>[] testOptions_1 = new KeyValuePair<Guid, string>[]
            {
                KeyValuePair.Create(Guid.NewGuid(), "Nothing"),
                KeyValuePair.Create(Guid.NewGuid(), "Quite a lot"),
                KeyValuePair.Create(Guid.NewGuid(), "Perhaps a little something"),
                KeyValuePair.Create(Guid.NewGuid(), "Everything")
            };

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartServerTest()
        {
            // Start server when no server is active
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            Assert.IsFalse(p.IsServerActive());
            p.StartServer(testPort);
            Assert.IsTrue(p.IsServerActive());

            // Start server when the server is already active
            Assert.IsTrue(p.IsServerActive());
            Assert.ThrowsException<InvalidOperationException>(() => p.StartServer(testPort));
            Assert.IsTrue(p.IsServerActive());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StopServerTest()
        {
            // Can't easily test for actually stopping the server due to issues 
            // with IHost and Threads.

            // Stop server when the server is not active
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            Assert.ThrowsException<InvalidOperationException>(() => p.StopServer());
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartNewSessionTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();

            // Start new session when the server is not active
            Assert.ThrowsException<InvalidOperationException>(() => p.StartNewSession(testKey_1));

            p.StartServer(testPort);

            // Start new session when server is active
            Assert.IsTrue(p.StartNewSession(testKey_1));

            // Start new session with the same key
            Assert.IsFalse(p.StartNewSession(testKey_1));

            // Start new session with invalid key-pattern
            Assert.IsFalse(p.StartNewSession(testKey_2));

            // Start new session with invalid key-pattern 2
            Assert.IsFalse(p.StartNewSession(testKey_3));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartNewVoteTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();

            
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void EndSessionTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();

            // End a session when the server is not running
            Assert.ThrowsException<InvalidOperationException>(() => p.EndSession(testKey_1));

            p.StartServer(testPort);
            p.StartNewSession(testKey_1);

            // End a session that is not currently active
            Assert.ThrowsException<SessionNotFoundException>(() => p.EndSession(testKey_2));

            // End a session that is currently active
            Assert.IsNotNull(p.EndSession(testKey_1));

            // End a session that has already been ended
            Assert.ThrowsException<SessionNotFoundException>(() => p.EndSession(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetVotingResultsTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();

            // Get results when the server is not active
            Assert.ThrowsException<InvalidOperationException>(() => p.GetVotingResults(testKey_1, testPrompt_1));
            
            p.StartServer(testPort);
            p.StartNewSession(testKey_1);

            // Get results for an invalid prompt
            Assert.IsNull(p.GetVotingResults(testKey_1, testPrompt_1));

            p.StartNewVote(testKey_1, testPrompt_1, testOptions_1);

            // Get results for a valid prompt
            Assert.IsNotNull(p.GetVotingResults(testKey_1, testPrompt_1));

            // Get results for a valid prompt again
            Assert.IsNotNull(p.GetVotingResults(testKey_1, testPrompt_1));
        }
    }
}