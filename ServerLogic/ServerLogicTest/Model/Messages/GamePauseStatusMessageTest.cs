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
    public class GamePauseStatusMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedPauseStatusTrueTestStringPattern = @"GamePauseStatusMessage \[<container>: MessageContainer \[ModeratorId: " + 
            testGuid + @", Type: GamePauseStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePausedStatus: True\]";
        private readonly string expectedPauseStatusFalseTestStringPattern = @"GamePauseStatusMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: GamePauseStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePausedStatus: False\]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void PauseStatusTrueTest()
        {
            GamePauseStatusMessage g = new GamePauseStatusMessage(testGuid, true);

            Assert.IsTrue(g.GamePausedStatus);
            Assert.IsTrue(Regex.IsMatch(g.ToString(), expectedPauseStatusTrueTestStringPattern));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void PauseStatusFalseTest()
        {
            GamePauseStatusMessage g = new GamePauseStatusMessage(testGuid, false);

            Assert.IsFalse(g.GamePausedStatus);
            Assert.IsTrue(Regex.IsMatch(g.ToString(), expectedPauseStatusFalseTestStringPattern));
        }
    }
}
