using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>RequestStartVotingMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class RequestStartVotingMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();

        private const int votingTime_1 = 0;
        private const int votingTime_2 = 19265423;
        private const int votingTime_3 = Int32.MaxValue;
        private const int votingTime_4 = -0;
        private const int votingTime_5 = -19265423;
        private const int votingTime_6 = Int32.MinValue;

        private static readonly Dictionary<Guid, string> votingOptions_1 = 
            new Dictionary<Guid, string>() { { Guid.NewGuid(), "string1" }, { Guid.NewGuid(), "string2" } };
        private static readonly Dictionary<Guid, string> votingOptions_2 = 
            new Dictionary<Guid, string>();

        private static readonly string dictToString = 
            "{" + string.Join(",", votingOptions_1.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";

        private readonly string expectedStringPattern = @"RequestStartVotingMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestStartVoting, Date: \d{4}\.\d{2}\.\d{2}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], VotingTime: " +
            votingTime_1 + @", VotingOptions: " + dictToString + @"\]";

        /// <summary>
        /// Validates that the assigned test-variables are the same before and after
        /// construction of the messages.
        /// </summary>
        [TestMethod]
        public void VotingTimeTest()
        {
            RequestStartVotingMessage r_1 = new RequestStartVotingMessage(testGuid, votingTime_1, votingOptions_1);
            RequestStartVotingMessage r_2 = new RequestStartVotingMessage(testGuid, votingTime_2, votingOptions_1);
            RequestStartVotingMessage r_3 = new RequestStartVotingMessage(testGuid, votingTime_3, votingOptions_1);
            RequestStartVotingMessage r_4 = new RequestStartVotingMessage(testGuid, votingTime_4, votingOptions_1);
            RequestStartVotingMessage r_5 = new RequestStartVotingMessage(testGuid, votingTime_5, votingOptions_1);
            RequestStartVotingMessage r_6 = new RequestStartVotingMessage(testGuid, votingTime_6, votingOptions_1);

            Assert.AreEqual(r_1.VotingTime, votingTime_1);
            Assert.AreEqual(r_2.VotingTime, votingTime_2);
            Assert.AreEqual(r_3.VotingTime, votingTime_3);
            Assert.AreEqual(r_4.VotingTime, votingTime_4);
            Assert.AreEqual(r_5.VotingTime, votingTime_5);
            Assert.AreEqual(r_6.VotingTime, votingTime_6);
        }

        /// <summary>
        /// Validates that the assigned test-variables are the same before and after
        /// construction of the messages.
        /// </summary>
        [TestMethod]
        public void VotingOptionsTest()
        {
            RequestStartVotingMessage r_1 = new RequestStartVotingMessage(testGuid, votingTime_1, votingOptions_1);
            RequestStartVotingMessage r_2 = new RequestStartVotingMessage(testGuid, votingTime_1, votingOptions_2);

            Assert.AreEqual(r_1.VotingOptions, votingOptions_1);
            Assert.AreEqual(r_2.VotingOptions, votingOptions_2);
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            RequestStartVotingMessage r = new RequestStartVotingMessage(testGuid, votingTime_1, votingOptions_1);

            Assert.IsNotNull(r.ToString());
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedStringPattern));   // DictionaryToString für die normale Klasse und die Test-Klasse erstellen
        }
    }
}
