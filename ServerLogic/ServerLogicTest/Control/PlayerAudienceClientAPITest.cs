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
    public class PlayerAudienceClientAPITest
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
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            Assert.IsFalse(p.IsServerActive());
            p.StartServer(testPort);
            Assert.IsTrue(p.IsServerActive());

            Assert.ThrowsException<InvalidOperationException>(() => p.StartServer(testPort));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StopServerTest()
        {
            // Can't easily test for actually stopping the server due to issues 
            // with IHost and Threads.

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
            Assert.ThrowsException<InvalidOperationException>(() => p.StartNewSession(testKey_1));
            p.StartServer(testPort);
            Assert.IsTrue(p.StartNewSession(testKey_1));
            Assert.IsFalse(p.StartNewSession(testKey_1));
            Assert.IsFalse(p.StartNewSession(testKey_2));
            Assert.IsFalse(p.StartNewSession(testKey_3));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartNewVoteTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            Assert.ThrowsException<InvalidOperationException>(() => p.StartNewVote(testKey_1, testPrompt_1, testOptions_1));
            p.StartServer(testPort);
            p.StartNewSession(testKey_1);
            Assert.IsTrue(p.StartNewVote(testKey_1, testPrompt_1, testOptions_1));
            //Console.WriteLine(p.StartNewVote(testKey_1, testPrompt_1, testOptions_1));
            Assert.IsFalse(p.StartNewVote(testKey_1, testPrompt_1, testOptions_1)); // ????
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void EndSessionTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            Assert.ThrowsException<InvalidOperationException>(() => p.EndSession(testKey_1));
            p.StartServer(testPort);
            p.StartNewSession(testKey_1);
            Assert.ThrowsException<SessionNotFoundException>(() => p.EndSession(testKey_2));
            Assert.IsNotNull(p.EndSession(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetVotingResultsTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            Assert.ThrowsException<InvalidOperationException>(() => p.GetVotingResults(testKey_1, testPrompt_1));
            p.StartServer(testPort);
            p.StartNewSession(testKey_1);
            Assert.IsNull(p.GetVotingResults(testKey_1, testPrompt_1));
            p.StartNewVote(testKey_1, testPrompt_1, testOptions_1);
            Assert.IsNotNull(p.GetVotingResults(testKey_1, testPrompt_1));
        }
    }
}