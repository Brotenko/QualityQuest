using Microsoft.VisualStudio.TestTools.UnitTesting;
using PAClient;
using System;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Collections.Generic;

namespace PAClientTest
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public sealed class VotingResultsTest
    {
        private const string testKey_1 = "TU7ROU";
        private const string testKey_2 = "G9EL40";
        private const string testKey_3 = "GHOU80";
        private const string getSessionKeysComparison = testKey_1 + ", " + testKey_2 + ", " + testKey_3;
        private KeyValuePair<Guid, string> testPrompt_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "This is a test prompt!");
        private KeyValuePair<Guid, string> testPrompt_NullString = new KeyValuePair<Guid, string>(Guid.NewGuid(), null);
        private KeyValuePair<Guid, string> testPrompt_Valid_2 = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Another very valid prompt!");
        private static KeyValuePair<Guid, string> testPair_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "Nothing");
        private static KeyValuePair<Guid, string> testPair_Valid_2 = KeyValuePair.Create(Guid.NewGuid(), "Quite a lot");
        private static KeyValuePair<Guid, string> testPair_Valid_3 = KeyValuePair.Create(Guid.NewGuid(), "Perhaps a little something");
        private static KeyValuePair<Guid, string> testPair_Valid_4 = KeyValuePair.Create(Guid.NewGuid(), "Everything");
        private static KeyValuePair<Guid, string> testPair_Valid_5 = KeyValuePair.Create(Guid.NewGuid(), "This one is not supposed to work! 451%51s0-.�POK$%");
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
        [TestMethod]
        [TestCategory("GetSessionKeys")]
        public void GetSessionKeysTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddSessionKey(testKey_2);
            v.AddSessionKey(testKey_3);

            string keys = string.Join(", ", v.GetSessionKeys());
            Assert.IsTrue(Equals(getSessionKeysComparison, keys.ToString()));
        }






        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddSessionKey")]
        public void AddSessionKey_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            v.AddSessionKey(testKey_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddSessionKey")]
        public void AddSessionKey_SameSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            v.AddSessionKey(testKey_1);
            Assert.ThrowsException<ArgumentException>(() => v.AddSessionKey(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddSessionKey")]
        public void AddSessionKey_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            Assert.ThrowsException<ArgumentNullException>(() => v.AddSessionKey(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddSessionKey")]
        public void AddSessionKey_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            // TODO
        }











        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.AddNewPoll(testKey_2, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(null, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_SameValidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            Assert.ThrowsException<ArgumentException>(() => v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_DoubleAssignPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            Assert.ThrowsException<ArgumentException>(() => v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_NullStringPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(testKey_1, testPrompt_NullString, testOptions_Valid_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_SameValidOptionsTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_2, testOptions_Valid_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_NullOptionsTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(testKey_1, testPrompt_Valid_1, null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_NullStringOptionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Invalid));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            // TODO
        }







        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_ValdInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            v.AddVote(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_1.Key);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.AddVote(testKey_2, testPrompt_Valid_1.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentException>(() => v.AddVote(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddVote(null, testPrompt_Valid_1.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentException>(() => v.AddVote(testKey_1, testPrompt_Valid_2.Key, testPair_Valid_5.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_InvalidOptionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentException>(() => v.AddVote(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_5.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }







        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveSession")]
        public void RemoveSession_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.RemoveSession(testKey_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveSession")]
        public void RemoveSession_SameSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.RemoveSession(testKey_1);
            Assert.ThrowsException<SessionNotFoundException>(() => v.RemoveSession(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveSession")]
        public void RemoveSession_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.RemoveSession(testKey_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveSession")]
        public void RemoveSession_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.RemoveSession(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveSession")]
        public void RemoveSession_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            // TODO
        }






        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptsBySession")]
        public void GetPromptsBySession_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetPromptsBySession(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptsBySession")]
        public void GetPromptsBySession_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetPromptsBySession(testKey_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptsBySession")]
        public void GetPromptsBySession_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetPromptsBySession(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptsBySession")]
        public void GetPromptsBySession_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetPromptsBySession(testKey_1).Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptsBySession")]
        public void GetPromptsBySession_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            //TODO
        }







        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetStatistics")]
        public void GetStatistics_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetStatistics(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetStatistics")]
        public void GetStatistics_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetStatistics(testKey_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetStatistics")]
        public void GetStatistics_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetStatistics(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetStatistics")]
        public void GetStatistics_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetStatistics(testKey_1).Count);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetStatistics")]
        public void GetStatistics_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            //TODO
        }









        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptGuidsBySession")]
        public void GetPromptGuidsBySession_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetPromptGuidsBySession(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptGuidsBySession")]
        public void GetPromptGuidsBySession_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetPromptGuidsBySession(testKey_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptGuidsBySession")]
        public void GetPromptGuidsBySession_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetPromptGuidsBySession(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptGuidsBySession")]
        public void GetPromptGuidsBySession_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetPromptGuidsBySession(testKey_1).Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptGuidsBySession")]
        public void GetPromptGuidsBySession_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }








        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptStringsBySession")]
        public void GetPromptStringsBySession_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetPromptStringsBySession(testKey_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptStringsBySession")]
        public void GetPromptStringsBySession_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetPromptStringsBySession(testKey_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptStringsBySession")]
        public void GetPromptStringsBySession_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetPromptStringsBySession(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptStringsBySession")]
        public void GetPromptStringsBySession_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetPromptStringsBySession(testKey_1).Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptStringsBySession")]
        public void GetPromptStringsBySession_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }










        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsVotesPairsByPrompt")]
        public void GetOptionsVotesPairsByPrompt_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetOptionsVotesPairsByPrompt(testKey_1, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsVotesPairsByPrompt")]
        public void GetOptionsVotesPairsByPrompt_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetOptionsVotesPairsByPrompt(testKey_2, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsVotesPairsByPrompt")]
        public void GetOptionsVotesPairsByPrompt_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetOptionsVotesPairsByPrompt(null, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsVotesPairsByPrompt")]
        public void GetOptionsVotesPairsByPrompt_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNull(v.GetOptionsVotesPairsByPrompt(testKey_1, testPrompt_Valid_2.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsVotesPairsByPrompt")]
        public void GetOptionsVotesPairsByPrompt_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }











        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsByPrompt")]
        public void GetOptionsByPrompt_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetOptionsByPrompt(testKey_1, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsByPrompt")]
        public void GetOptionsByPrompt_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetOptionsByPrompt(testKey_2, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsByPrompt")]
        public void GetOptionsByPrompt_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetOptionsByPrompt(null, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsByPrompt")]
        public void GetOptionsByPrompt_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.AreEqual(0, v.GetOptionsByPrompt(testKey_1, testPrompt_Valid_2.Key).Length);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionsByPrompt")]
        public void GetOptionsByPrompt_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }








        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionStringsByPrompt")]
        public void GetOptionStringsByPrompt_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetOptionStringsByPrompt(testKey_1, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionStringsByPrompt")]
        public void GetOptionStringsByPrompt_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetOptionStringsByPrompt(testKey_2, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionStringsByPrompt")]
        public void GetOptionStringsByPrompt_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetOptionStringsByPrompt(null, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionStringsByPrompt")]
        public void GetOptionStringsByPrompt_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNull(v.GetOptionStringsByPrompt(testKey_1, testPrompt_Valid_2.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionStringsByPrompt")]
        public void GetOptionStringsByPrompt_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }







        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionGuidsByPrompt")]
        public void GetOptionGuidsByPrompt_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetOptionGuidsByPrompt(testKey_1, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionGuidsByPrompt")]
        public void GetOptionGuidsByPrompt_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetOptionGuidsByPrompt(testKey_2, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionGuidsByPrompt")]
        public void GetOptionGuidsByPrompt_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetOptionGuidsByPrompt(null, testPrompt_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionGuidsByPrompt")]
        public void GetOptionGuidsByPrompt_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNull(v.GetOptionGuidsByPrompt(testKey_1, testPrompt_Valid_2.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetOptionGuidsByPrompt")]
        public void GetOptionGuidsByPrompt_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }










        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotesByOption")]
        public void GetVotesByOption_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.IsNotNull(v.GetVotesByOption(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotesByOption")]
        public void GetVotesByOption_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.GetVotesByOption(testKey_2, testPrompt_Valid_1.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotesByOption")]
        public void GetVotesByOption_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.GetVotesByOption(null, testPrompt_Valid_1.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotesByOption")]
        public void GetVotesByOption_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentException>(() => v.GetVotesByOption(testKey_1, testPrompt_NullString.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotesByOption")]
        public void GetVotesByOption_InvalidOptionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentException>(() => v.GetVotesByOption(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_5.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotesByOption")]
        public void GetVotesByOptionT_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            // TODO
        }













        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        [TestCategory("ToString")]
        public void ToStringTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }
    }
}
