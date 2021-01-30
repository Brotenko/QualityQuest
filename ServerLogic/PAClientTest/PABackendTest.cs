using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;
using PAClient;

namespace PAClientTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class PABackendTest
    {
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
        public void CountNewVoteTest()
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
        public void AddNewSessionTest()
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
        public void RemoveSessionTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddConnectionTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void RemoveConnectionTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetVotingResultTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SendPushMessageTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SendPushClearTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void CreatePageContentTest()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ServerStartTest()
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
