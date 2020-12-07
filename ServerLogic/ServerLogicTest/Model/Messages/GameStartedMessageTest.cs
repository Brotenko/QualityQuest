using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class GameStartedMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"GameStartedMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: GameStarted, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            GameStartedMessage g = new GameStartedMessage(testGuid);

            Assert.IsNotNull(g.ToString());
            Assert.IsTrue(Regex.IsMatch(g.ToString(), expectedStringPattern));
        }
    }
}
