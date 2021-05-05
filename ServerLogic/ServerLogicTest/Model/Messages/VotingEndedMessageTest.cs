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
        //  private static readonly Guid testWinningOption = Guid.NewGuid();
        private static readonly string testWinningOption = "string1";
        
        private static readonly Dictionary<Guid, int> testVotingResults =
            new() { { Guid.NewGuid(), 1 }, { Guid.NewGuid(), 2 } };

      
        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void WinningOptionTest()
        {
            VotingEndedMessage v = new VotingEndedMessage(testGuid, testWinningOption, testVotingResults, 42);

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
            VotingEndedMessage v = new VotingEndedMessage(testGuid, testWinningOption, testVotingResults, 42);

            Assert.IsNotNull(v.VotingResults);
            Assert.AreEqual(v.VotingResults, testVotingResults);
        }
    }
}
