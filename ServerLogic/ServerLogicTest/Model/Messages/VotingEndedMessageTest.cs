using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// 
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
        /// 
        /// </summary>
        [TestMethod]
        public void WinningOptionTest()
        {
            VotingEndedMessage v = new VotingEndedMessage(testGuid, testWinningOption, testVotingResults);

            Assert.IsNotNull(v.WinningOption);
            Assert.AreEqual(v.WinningOption, testWinningOption);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void VotingResultsTest()
        {
            VotingEndedMessage v = new VotingEndedMessage(testGuid, testWinningOption, testVotingResults);

            Assert.IsNotNull(v.VotingResults);
            Assert.AreEqual(v.VotingResults, testVotingResults);
        }

        /// <summary>
        /// 
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
