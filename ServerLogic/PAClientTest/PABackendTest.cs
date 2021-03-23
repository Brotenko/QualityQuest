using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using PAClient;
using System.Threading.Tasks;

namespace PAClientTest
{
    /// <summary>
    /// PABackend Tests
    /// </summary>
    [TestClass]
    public sealed class PABackendTest
    {
        private const int testPort = 7777;
        private const string testKey_1 = "TU7ROU";
        private const string testKey_2 = "G9EL40";
        private const string testKey_Invalid_1 = "7g/vﬂ`";
        private const string testId_Valid_1 = "sQxPVXaPUuoSV_2epIFMkw";
        private const string testId_Valid_2 = "7fJem-hO8gPE8v_4rZUg5a";
        private const string testId_Invalid_1 = "u?oS‰V_2epI7.=4gZUs%";
        private KeyValuePair<Guid, string> testPrompt_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "This is a test prompt!");
        private KeyValuePair<Guid, string> testPrompt_NullString = new KeyValuePair<Guid, string>(Guid.NewGuid(), null);
        private KeyValuePair<Guid, string> testPrompt_Valid_2 = new KeyValuePair<Guid, string>(Guid.NewGuid(), "Another very valid prompt!");
        private static KeyValuePair<Guid, string> testPair_Valid_1 = KeyValuePair.Create(Guid.NewGuid(), "Nothing");
        private static KeyValuePair<Guid, string> testPair_Valid_2 = KeyValuePair.Create(Guid.NewGuid(), "Quite a lot");
        private static KeyValuePair<Guid, string> testPair_Valid_3 = KeyValuePair.Create(Guid.NewGuid(), "Perhaps a little something");
        private static KeyValuePair<Guid, string> testPair_Valid_4 = KeyValuePair.Create(Guid.NewGuid(), "Everything");
        private static KeyValuePair<Guid, string> testPair_Valid_5 = KeyValuePair.Create(Guid.NewGuid(), "This one is not supposed to work! 451%51s0-.‹POK$%");
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

        /************************ GetSessionKeys Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.GetSessionKeys"/> method
        /// returns an empty array when no sessionkeys have been set yet.
        /// </summary>
        [TestMethod]
        [TestCategory("GetSessionKeys")]
        public void GetSessionKeys_EmptyArrayTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.AreEqual(0, p.GetSessionKeys().Length);
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.GetSessionKeys"/> method
        /// returns an array of all currently active sessionkeys.
        /// </summary>
        [TestMethod]
        [TestCategory("GetSessionKeys")]
        public void GetSessionKeys_FilledArrayTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            PABackend.PAVotingResults.AddSessionKey(testKey_1);
            Assert.AreEqual(1, p.GetSessionKeys().Length);
            Assert.AreEqual(testKey_1, p.GetSessionKeys()[0]);
        }

        /************************ StartNewSession Test ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewSession"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            p.StartNewSession(testKey_1);

            Assert.AreEqual(testKey_1, PABackend.PAVotingResults.GetSessionKeys()[0]);
            Assert.IsNotNull(PABackend.ConnectionList.GetValueOrDefault(testKey_1));
            Assert.IsInstanceOfType(PABackend.ConnectionList.GetValueOrDefault(testKey_1), typeof(List<string>));
            Assert.AreEqual(0, PABackend.ConnectionList.GetValueOrDefault(testKey_1).Count);
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewSession"/> method
        /// throws an <see cref="ArgumentException"/> exception when an invalid sessionkey
        /// is given.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_InvalidSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentException>(() => p.StartNewSession(testKey_Invalid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewSession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_NullSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.StartNewSession(null));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewSession"/> method
        /// throws an <see cref="ArgumentException"/> exception when the same
        /// valid sessionkey is given twice.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewSession")]
        public void StartNewSession_SameSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            p.StartNewSession(testKey_1);
            Assert.ThrowsException<ArgumentException>(() => p.StartNewSession(testKey_1));
        }

        /************************ CreatePageContent Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.CreatePageContent"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            string pageContent = p.DebugCreatePageContent(testPrompt_Valid_1, testOptions_Valid_1);
            string toCompare = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                                 testPrompt_Valid_1.Value +
                               "</div>" +
                               "<div id=\"voting-container\" class=\"voting-container\">" +
                               "<input type=\"button\" id=\"choice-1\" name=\"" + testOptions_Valid_1[0].Key + "\" class=\"input-button voting-container-4items-1\" value=\"" + testOptions_Valid_1[0].Value + "\" />" +
                               "<input type=\"button\" id=\"choice-2\" name=\"" + testOptions_Valid_1[1].Key + "\" class=\"input-button voting-container-4items-2\" value=\"" + testOptions_Valid_1[1].Value + "\" />" +
                               "<input type=\"button\" id=\"choice-3\" name=\"" + testOptions_Valid_1[2].Key + "\" class=\"input-button voting-container-4items-3\" value=\"" + testOptions_Valid_1[2].Value + "\" />" +
                               "<input type=\"button\" id=\"choice-4\" name=\"" + testOptions_Valid_1[3].Key + "\" class=\"input-button voting-container-4items-4\" value=\"" + testOptions_Valid_1[3].Value + "\" />" +
                               "</div>";

            Assert.AreEqual(toCompare, pageContent);
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.CreatePageContent"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_NullPromptDescriptionTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.DebugCreatePageContent(testPrompt_NullString, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.CreatePageContent"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_NullOptionsTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.DebugCreatePageContent(testPrompt_Valid_1, null));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.CreatePageContent"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("CreatePageContent")]
        public void CreatePageContent_NullOptionDescriptionTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.DebugCreatePageContent(testPrompt_Valid_1, testOptions_Invalid));
        }

        /************************ StartNewVote Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_InvalidSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);
            
            await Assert.ThrowsExceptionAsync<SessionNotFoundException>(() => p.StartNewVote(testKey_2, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_NullSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(null, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// throws an <see cref="ArgumentException"/> exception when the same
        /// valid prompt is given twice.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_SameValidPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// throws an <see cref="ArgumentException"/> exception when the same
        /// valid prompt is assigned a poll twice.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_DoubleAssignPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_2));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_NullStringPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(testKey_1, testPrompt_NullString, testOptions_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// works correctly when given the same valid input twice.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_SameValidOptionsTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);
            await p.StartNewVote(testKey_1, testPrompt_Valid_2, testOptions_Valid_1);
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_NullOptionsTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, null));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.StartNewVote"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("StartNewVote")]
        public async Task SendPushMessage_NullStringOptionTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Invalid));
        }

        /************************ CountNewVote Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.CountNewVote"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public async Task CountNewVote_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);
            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Assert.AreEqual((int) PABackendErrorType.NoError, PABackend.CountNewVote(testKey_1, testPair_Valid_1.Key));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.CountNewVote"/> method
        /// returns <see cref="PABackendErrorType.InvalidSessionkeyError"/>
        /// when an invalid sessionkey is given.
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_InvalidSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidSessionkeyError, PABackend.CountNewVote(testKey_2, testPair_Valid_1.Key));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.CountNewVote"/> method
        /// returns <see cref="PABackendErrorType.NullSessionkeyError"/>
        /// when a null-value is given.
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_NullSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.NullSessionkeyError, PABackend.CountNewVote(null, testPair_Valid_1.Key));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.CountNewVote"/> method
        /// returns <see cref="PABackendErrorType.InvalidSessionkeyError"/>
        /// when the given sessionkey does not belong to an active session.
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_NoSessionStartedTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.AreEqual((int) PABackendErrorType.InvalidSessionkeyError, PABackend.CountNewVote(testKey_1, testPair_Valid_1.Key));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.CountNewVote"/> method
        /// returns <see cref="PABackendErrorType.InvalidArgumentError"/>
        /// when a given prompt does not belong to a valid sessionkey.
        /// </summary>
        [TestMethod]
        [TestCategory("CountNewVote")]
        public void CountNewVote_MissingPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidArgumentError, PABackend.CountNewVote(testKey_1, testPair_Valid_1.Key));
        }

        /************************ EndSession Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.EndSession"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.IsTrue(Array.Exists(p.GetSessionKeys(), element => element == testKey_1));
            Assert.IsNotNull(p.EndSession(testKey_1));
            Assert.IsFalse(Array.Exists(p.GetSessionKeys(), element => element == testKey_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.EndSession"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_InvalidSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<SessionNotFoundException>(() => p.EndSession(testKey_2));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.EndSession"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("EndSession")]
        public void EndSession_NullSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.EndSession(null));
        }

        /************************ AddConnection Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.AddConnection"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.IsFalse(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
            Assert.AreEqual((int) PABackendErrorType.NoError, PABackend.AddConnection(testKey_1, testId_Valid_1));
            Assert.IsTrue(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.AddConnection"/> method
        /// returns <see cref="PABackendErrorType.InvalidSessionkeyError"/>
        /// when an invalid sessionkey is given.
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_InvalidSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidSessionkeyError, PABackend.AddConnection(testKey_2, testId_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.AddConnection"/> method
        /// returns <see cref="PABackendErrorType.NullSessionkeyError"/>
        /// when a null-value is given.
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_NullSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.NullSessionkeyError, PABackend.AddConnection(null, testId_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.AddConnection"/> method
        /// returns <see cref="PABackendErrorType.InvalidConnectionIdError"/>
        /// when the given connectionId does not belong to an active connection.
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_InvalidConnectionIdTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.InvalidConnectionIdError, PABackend.AddConnection(testKey_1, testId_Invalid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.AddConnection"/> method
        /// returns <see cref="PABackendErrorType.NullConnectionIdError"/>
        /// when a null-value is given.
        /// </summary>
        [TestMethod]
        [TestCategory("AddConnection")]
        public void AddConnection_NullConnectionIdTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.AreEqual((int) PABackendErrorType.NullConnectionIdError, PABackend.AddConnection(testKey_1, null));
        }

        /************************ RemoveConnection Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.RemoveConnection"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveConnection")]
        public void RemoveConnection_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);
            PABackend.AddConnection(testKey_1, testId_Valid_1);

            Assert.IsTrue(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
            Assert.AreEqual((int) PABackendErrorType.NoError, PABackend.RemoveConnection(testId_Valid_1));
            Assert.IsFalse(PABackend.ConnectionList.GetValueOrDefault(testKey_1).Contains(testId_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.RemoveConnection"/> method
        /// returns <see cref="PABackendErrorType.InvalidConnectionIdError"/>
        /// when the given connectionId does not belong to an active connection.
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveConnection")]
        public void RemoveConnection_InvalidConnectionIdTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.AreEqual((int) PABackendErrorType.InvalidConnectionIdError, PABackend.RemoveConnection(testId_Valid_2));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.RemoveConnection"/> method
        /// returns <see cref="PABackendErrorType.NullConnectionIdError"/>
        /// when a null-value is given.
        /// </summary>
        [TestMethod]
        [TestCategory("RemoveConnection")]
        public void RemoveConnection_NullConnectionIdTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.AreEqual((int) PABackendErrorType.NullConnectionIdError, PABackend.RemoveConnection(null));
        }

        /************************ GetVotingResult Tests ****************************/

        /// <summary>
        /// Validates that the <see cref="PABackend.GetVotingResult"/> method
        /// works correctly when given a valid input.
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public async Task GetVotingResult_ValidInputTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);
            p.StartNewSession(testKey_2);
            await p.StartNewVote(testKey_1, testPrompt_Valid_1, testOptions_Valid_1);

            Dictionary<KeyValuePair<Guid, string>, int> retrievedValues = p.GetVotingResult(testKey_1, testPrompt_Valid_1);

            Assert.IsNotNull(retrievedValues);
            Assert.AreEqual(testOptions_Valid_1.ElementAt(0), retrievedValues.Keys.ElementAt(0));
            Assert.AreEqual(testOptions_Valid_1.ElementAt(1), retrievedValues.Keys.ElementAt(1));
            Assert.AreEqual(testOptions_Valid_1.ElementAt(2), retrievedValues.Keys.ElementAt(2));
            Assert.AreEqual(testOptions_Valid_1.ElementAt(3), retrievedValues.Keys.ElementAt(3));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.GetVotingResult"/> method
        /// throws a <see cref="SessionNotFoundException"/> exception when an invalid sessionkey
        /// is given.
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_InvalidSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<SessionNotFoundException>(() => p.GetVotingResult(testKey_1, testPrompt_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.GetVotingResult"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_NullSessionkeyTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.GetVotingResult(null, testPrompt_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.GetVotingResult"/> method
        /// throws an <see cref="ArgumentException"/> exception when the given
        /// prompt is invalid.
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_InvalidPromptTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);
            p.StartNewSession(testKey_1);

            Assert.ThrowsException<ArgumentException>(() => p.GetVotingResult(testKey_1, testPrompt_Valid_1));
        }

        /// <summary>
        /// Validates that the <see cref="PABackend.GetVotingResult"/> method
        /// throws an <see cref="ArgumentNullException"/> exception when a null-value is
        /// given.
        /// </summary>
        [TestMethod]
        [TestCategory("GetVotingResult")]
        public void GetVotingResult_NullPromptDescriptionTest()
        {
            PABackend p = PABackend.DebugPABackend(testPort);

            Assert.ThrowsException<ArgumentNullException>(() => p.GetVotingResult(testKey_1, testPrompt_NullString));
        }
    }
}
