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
        public void StartNewVoteTest()
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