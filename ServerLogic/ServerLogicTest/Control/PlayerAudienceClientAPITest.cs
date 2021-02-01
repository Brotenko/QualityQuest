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

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartNewSessionTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartNewVoteTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetVotingResultsTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void EndSessionTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StopServerTest()
        {

        }
    }
}