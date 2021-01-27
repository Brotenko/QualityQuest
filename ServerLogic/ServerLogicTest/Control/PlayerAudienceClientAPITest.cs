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
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartNewSessionTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            //Assert.IsTrue(p.StartNewSession(testKey_1));
            //Assert.IsFalse(p.StartNewSession(testKey_2));
            //Assert.IsFalse(p.StartNewSession(testKey_3));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StartNewVoteTest()
        {
            PlayerAudienceClientAPI p = new PlayerAudienceClientAPI();
            p.StartNewSession(testKey_1);
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
        public void GetVotingResultsTest()
        {

        }
    }
}