using Microsoft.VisualStudio.TestTools.UnitTesting;
using PAClient;
using System;
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



        /*********************** GetSessionKeys Tests *****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetSessionKeys"/> method
        /// returns all sessionkeys in the correct format.
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

        /*********************** AddSessionKey Tests *****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.AddSessionKey"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("AddSessionKey")]
        public void AddSessionKey_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            v.AddSessionKey(testKey_1);
        }

        /// <summary>
        /// Validates that the <see cref="VotingResults.AddSessionKey"/> method
        /// throws an <see cref="ArgumentException"/> exception when the same sessionkey
        /// is given twice.
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
        /// Validates that the <see cref="VotingResults.AddSessionKey"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("AddSessionKey")]
        public void AddSessionKey_NullSessionkeyTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            Assert.ThrowsException<ArgumentNullException>(() => v.AddSessionKey(null));
        }

        /// <summary>
        /// Validates that the <see cref="VotingResults.AddSessionKey"/> method
        /// functions correctly and assigns everything in the right order and at
        /// the right position.
        /// </summary>
        [TestMethod]
        [TestCategory("AddSessionKey")]
        public void AddSessionKey_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());

            string toCompare =
                "VotingResults:\n" +
                " - " + testKey_1 + ":\n" +
                " - " + testKey_2 + ":\n" +
                " - " + testKey_3 + ":\n";

            v.AddSessionKey(testKey_1);
            v.AddSessionKey(testKey_2);
            v.AddSessionKey(testKey_3);

            Assert.AreEqual(toCompare, v.ToString());
        }

        /************************** AddNewPoll Tests *****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// throws an <see cref="ArgumentException"/> exception when the same
        /// valid prompt is given twice.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// throws an <see cref="ArgumentException"/> exception when the same valid prompt
        /// is assigned a poll twice.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// works correctly when given the same valid input twice.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.AddNewPoll"/> method
        /// functions correctly and assigns everything in the right order and at
        /// the right position.
        /// </summary>
        [TestMethod]
        [TestCategory("AddNewPoll")]
        public void AddNewPoll_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            string toCompare =
                "VotingResults:\n" +
                " - " + testKey_1 + ":\n" +
                "   - " + testPrompt_Valid_1.Value + " (" + testPrompt_Valid_1.Key + "):\n" +
                "     - " + testPair_Valid_1.Value + " (" + testPair_Valid_1.Key + "): 0\n" +
                "     - " + testPair_Valid_2.Value + " (" + testPair_Valid_2.Key + "): 0\n" +
                "     - " + testPair_Valid_3.Value + " (" + testPair_Valid_3.Key + "): 0\n" +
                "     - " + testPair_Valid_4.Value + " (" + testPair_Valid_4.Key + "): 0\n";

            Assert.AreEqual(toCompare, v.ToString());
        }

        /*************************** AddVote Tests *****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.AddVote"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_ValidInputTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            v.AddVote(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_1.Key);
        }

        /// <summary>
        /// Validates that the <see cref="VotingResults.AddVote"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.AddVote"/> method
        /// throws an <see cref="ArgumentException"/> exception when a vote
        /// is added to a given prompt thast is not part of the given session.
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
        /// Validates that the <see cref="VotingResults.AddVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.AddVote"/> method
        /// throws an <see cref="ArgumentException"/> exception when an invalid prompt is
        /// given.
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
        /// Validates that the <see cref="VotingResults.AddVote"/> method
        /// throws an <see cref="ArgumentException"/> exception when an invalid option is
        /// given.
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
        /// Validates that the <see cref="VotingResults.AddVote"/> method
        /// functions correctly and assigns everything in the right order and at
        /// the right position.
        /// </summary>
        [TestMethod]
        [TestCategory("AddVote")]
        public void AddVote_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            v.AddVote(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_1.Key);

            string toCompare =
                "VotingResults:\n" +
                " - " + testKey_1 + ":\n" +
                "   - " + testPrompt_Valid_1.Value + " (" + testPrompt_Valid_1.Key + "):\n" +
                "     - " + testPair_Valid_1.Value + " (" + testPair_Valid_1.Key + "): 1\n" +
                "     - " + testPair_Valid_2.Value + " (" + testPair_Valid_2.Key + "): 0\n" +
                "     - " + testPair_Valid_3.Value + " (" + testPair_Valid_3.Key + "): 0\n" +
                "     - " + testPair_Valid_4.Value + " (" + testPair_Valid_4.Key + "): 0\n";

            Assert.AreEqual(toCompare, v.ToString());
        }

        /************************ RemoveSession Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.RemoveSession"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.RemoveSession"/> method
        /// throws an <see cref="SessionNotFoundException"/> exception when the same
        /// valid sessionkey is given twice.
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
        /// Validates that the <see cref="VotingResults.RemoveSession"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.RemoveSession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.RemoveSession"/> method
        /// functions correctly and assigns everything in the right order and at
        /// the right position.
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveSession")]
        public void RemoveSession_ToStringCorrectness()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            v.AddVote(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_1.Key);
            v.RemoveSession(testKey_1);

            string toCompare =
                "VotingResults:\n";

            Assert.AreEqual(toCompare, v.ToString());
        }

        /************************ GetPromptsBySession Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetPromptsBySession"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetPromptsBySession"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetPromptsBySession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetPromptsBySession"/> method
        /// returns an empty array when no prompts have been added to the session
        /// beforehand.
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptsBySession")]
        public void GetPromptsBySession_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetPromptsBySession(testKey_1).Length);
        }

        /************************ GetStatistics Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetStatistics"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetStatistics"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetStatistics"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetStatistics"/> method
        /// returns an empty Dictionary when nothing has been assigned to the
        /// session beforehand.
        /// </summary>
        [TestMethod]
        [TestCategory("GetStatistics")]
        public void GetStatistics_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetStatistics(testKey_1).Count);
        }

        /************************ GetPromptGuidsBySession Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetPromptGuidsBySession"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetPromptGuidsBySession"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetPromptGuidsBySession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetPromptGuidsBySession"/> method
        /// returns an empty array when no prompts have been added to the session
        /// beforehand.
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptGuidsBySession")]
        public void GetPromptGuidsBySession_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetPromptGuidsBySession(testKey_1).Length);
        }

        /************************ GetPromptStringsBySession Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetPromptStringsBySession"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetPromptStringsBySession"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetPromptStringsBySession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetPromptStringsBySession"/> method
        /// returns an empty array when no prompts have been added to the session
        /// beforehand.
        /// </summary>
        [TestMethod]
        [TestCategory("GetPromptStringsBySession")]
        public void GetPromptStringsBySession_MissingPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);

            Assert.AreEqual(0, v.GetPromptStringsBySession(testKey_1).Length);
        }

        /************************ GetOptionsVotesPairsByPrompt Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetOptionsVotesPairsByPrompt"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetOptionsVotesPairsByPrompt"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetOptionsVotesPairsByPrompt"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetOptionsVotesPairsByPrompt"/> method
        /// returns a null-value when a prompt is given that is not part of the given session.
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

        /************************ GetOptionsByPrompt Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetOptionsByPrompt"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetOptionsByPrompt"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetOptionsByPrompt"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetOptionsByPrompt"/> method
        /// returns an empty array when no prompts have been added to the session
        /// beforehand.
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

        /************************ GetOptionStringsByPrompt Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetOptionStringsByPrompt"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetOptionStringsByPrompt"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetOptionStringsByPrompt"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetOptionStringsByPrompt"/> method
        /// returns a null-value when a prompt is given that is not part of the given session.
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

        /************************ GetOptionGuidsByPrompt Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetOptionGuidsByPrompt"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetOptionGuidsByPrompt"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetOptionGuidsByPrompt"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetOptionGuidsByPrompt"/> method
        /// returns a null-value when a prompt is given that is not part of the given session.
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

        /************************ GetVotesByOption Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetVotesByOption"/> method
        /// works correctly when given a valid input.
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
        /// Validates that the <see cref="VotingResults.GetVotesByOption"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
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
        /// Validates that the <see cref="VotingResults.GetVotesByOption"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
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
        /// Validates that the <see cref="VotingResults.GetVotesByOption"/> method
        /// throws an <see cref="ArgumentException"/> exception when the given prompt
        /// is not part of the given session.
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotesByOption")]
        public void GetVotesByOption_InvalidPromptTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.ThrowsException<ArgumentException>(() => v.GetVotesByOption(testKey_1, testPrompt_Valid_2.Key, testPair_Valid_1.Key));
        }

        /// <summary>
        /// Validates that the <see cref="VotingResults.GetVotesByOption"/> method
        /// throws an <see cref="ArgumentException"/> exception when the given option
        /// is not part of the given session.
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

        /************************ ToString Test ****************************/

        /// <summary>
        /// Validates that the <see cref="VotingResults.ToString"/> method
        /// functions correctly and returns everything in the right order and at
        /// the right position.
        /// </summary>
        [TestMethod]
        [TestCategory("ToString")]
        public void ToStringTest()
        {
            VotingResults v = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            v.AddSessionKey(testKey_1);
            v.AddSessionKey(testKey_2);
            v.AddSessionKey(testKey_3);
            v.AddNewPoll(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            v.AddVote(testKey_1, testPrompt_Valid_1.Key, testPair_Valid_1.Key);

            string toCompare =
                "VotingResults:\n" +
                " - " + testKey_1 + ":\n" +
                "   - " + testPrompt_Valid_1.Value + " (" + testPrompt_Valid_1.Key + "):\n" +
                "     - " + testPair_Valid_1.Value + " (" + testPair_Valid_1.Key + "): 1\n" +
                "     - " + testPair_Valid_2.Value + " (" + testPair_Valid_2.Key + "): 0\n" +
                "     - " + testPair_Valid_3.Value + " (" + testPair_Valid_3.Key + "): 0\n" +
                "     - " + testPair_Valid_4.Value + " (" + testPair_Valid_4.Key + "): 0\n" +
                " - " + testKey_2 + ":\n" +
                " - " + testKey_3 + ":\n";

            Assert.AreEqual(toCompare, v.ToString());
        }
    }
}
