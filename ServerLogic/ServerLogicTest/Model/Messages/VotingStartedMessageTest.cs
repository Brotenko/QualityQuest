using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class VotingStartedMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"VotingStartedMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: VotingStarted, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            VotingStartedMessage v = new VotingStartedMessage(testGuid);

            Assert.IsNotNull(v.ToString());
            Assert.IsTrue(Regex.IsMatch(v.ToString(), expectedStringPattern));
        }
    }
}
