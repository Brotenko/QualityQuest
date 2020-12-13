using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>VotingEndedMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class VotingEndedMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private static readonly Guid testWinningOption = Guid.NewGuid();
        private static readonly Dictionary<Guid, int> testVotingResults =
            new Dictionary<Guid, int>() { { Guid.NewGuid(), 1 }, { Guid.NewGuid(), 2 } };

        private static readonly string dictToString =
        "{" + string.Join(",", testVotingResults.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";

        private readonly string expectedStringPattern = @"VotingEndedMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: VotingEnded, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], WinningOption: " + 
            testWinningOption + @", VotingResults:" + dictToString + @"\]";

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void WinningOptionTest()
        {
            VotingEndedMessage v = new VotingEndedMessage(testGuid, testWinningOption, testVotingResults);

            Assert.IsNotNull(v.WinningOption);
            Assert.AreEqual(v.WinningOption, testWinningOption);
        }

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void VotingResultsTest()
        {
            VotingEndedMessage v = new VotingEndedMessage(testGuid, testWinningOption, testVotingResults);

            Assert.IsNotNull(v.VotingResults);
            Assert.AreEqual(v.VotingResults, testVotingResults);
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
            VotingEndedMessage v = new VotingEndedMessage(testGuid, testWinningOption, testVotingResults);

            Assert.IsNotNull(v.ToString());
            Assert.IsTrue(Regex.IsMatch(v.ToString(), expectedStringPattern));
        }
    }
}
