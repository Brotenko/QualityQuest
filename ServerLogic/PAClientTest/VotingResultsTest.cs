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
    public class VotingResultsTest
    {
        private const int testPort = 7777;
        private const string testKey_1 = "TU7ROU";
        private const string testKey_2 = "G9EL40";
        private const string testKey_3 = "GHOU80";
        private KeyValuePair<Guid, string> testPrompt_1 = KeyValuePair.Create(Guid.NewGuid(), "This is a test prompt!");
        private KeyValuePair<Guid, string> testPrompt_2 = new KeyValuePair<Guid, string>(Guid.NewGuid(), null);
        private KeyValuePair<Guid, string> testPrompt_3 = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Another very valid prompt!");
        private static KeyValuePair<Guid, string> testPair_1 = KeyValuePair.Create(Guid.NewGuid(), "Nothing");
        private static KeyValuePair<Guid, string> testPair_2 = KeyValuePair.Create(Guid.NewGuid(), "Quite a lot");
        private static KeyValuePair<Guid, string> testPair_3 = KeyValuePair.Create(Guid.NewGuid(), "Perhaps a little something");
        private static KeyValuePair<Guid, string> testPair_4 = KeyValuePair.Create(Guid.NewGuid(), "Everything");
        private static KeyValuePair<Guid, string> testPair_5 = KeyValuePair.Create(Guid.NewGuid(), "This one is not supposed to work! 451%51s0-.ÜPOK$%");
        private static KeyValuePair<Guid, string> testPair_6 = new KeyValuePair<Guid, string>(Guid.NewGuid(), null);
        private KeyValuePair<Guid, string>[] testOptions_1 = new KeyValuePair<Guid, string>[]
            {
                testPair_1,
                testPair_2,
                testPair_3,
                testPair_4
            };
        private KeyValuePair<Guid, string>[] testOptions_2 = new KeyValuePair<Guid, string>[]
            {
                testPair_5
            };
        private KeyValuePair<Guid, string>[] testOptions_3 = null;
        private KeyValuePair<Guid, string>[] testOptions_4 = new KeyValuePair<Guid, string>[]
            {
                testPair_6
            };


        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddSessionKey_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            v.AddSessionKey(testKey_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
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
        public void AddSessionKey_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            Assert.ThrowsException<ArgumentNullException>(() => v.AddSessionKey(null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddSessionKey_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            // TODO
        }











        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.AddNewPoll(testKey_2, testPrompt_1, testOptions_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(null, testPrompt_1, testOptions_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_SameValidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);
            Assert.ThrowsException<ArgumentException>(() => v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_DoubleAssignPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);
            Assert.ThrowsException<ArgumentException>(() => v.AddNewPoll(testKey_1, testPrompt_1, testOptions_2));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_NullStringPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(testKey_1, testPrompt_2, testOptions_1));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_SameValidOptionsTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);
            v.AddNewPoll(testKey_1, testPrompt_3, testOptions_1);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_NullOptionsTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(testKey_1, testPrompt_1, null));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddNewPoll_NullStringOptionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddNewPoll(testKey_1, testPrompt_1, testOptions_4));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
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
        public void AddVote_ValdInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);

            v.AddVote(testKey_1, testPrompt_1.Key, testPair_1.Key);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddVote_InvalidSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);

            Assert.ThrowsException<SessionNotFoundException>(() => v.AddVote(testKey_2, testPrompt_1.Key, testPair_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddVote_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);

            Assert.ThrowsException<ArgumentNullException>(() => v.AddVote(null, testPrompt_1.Key, testPair_1.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddVote_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);

            Assert.ThrowsException<ArgumentException>(() => v.AddVote(testKey_1, testPrompt_3.Key, testPair_5.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddVote_InvalidOptionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);

            Assert.ThrowsException<ArgumentException>(() => v.AddVote(testKey_1, testPrompt_1.Key, testPair_5.Key));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AddVote_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_1, testOptions_1);

            // TODO
        }







        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
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
        public void GetSessionKeysTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetPromptsBySessionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetStatisticsTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetPromptGuidsBySessionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetPromptStringsBySessionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetOptionsVotesPairsByPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetOptionsByPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetOptionStringsByPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetOptionGuidsByPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void GetVotesByOptionTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());


        }
    }
}
