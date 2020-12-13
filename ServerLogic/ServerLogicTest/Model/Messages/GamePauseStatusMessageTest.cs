using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>GamePauseStatusMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class GamePauseStatusMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedPauseStatusTrueTestStringPattern = @"GamePauseStatusMessage \[<container>: MessageContainer \[ModeratorId: " + 
            testGuid + @", Type: GamePauseStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePaused: True\]";
        private readonly string expectedPauseStatusFalseTestStringPattern = @"GamePauseStatusMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: GamePauseStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePaused: False\]";

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void PauseStatusTrueTest()
        {
            GamePauseStatusMessage g = new GamePauseStatusMessage(testGuid, true);

            Assert.IsTrue(g.GamePaused);
            Assert.IsTrue(Regex.IsMatch(g.ToString(), expectedPauseStatusTrueTestStringPattern));
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void PauseStatusFalseTest()
        {
            GamePauseStatusMessage g = new GamePauseStatusMessage(testGuid, false);

            Assert.IsFalse(g.GamePaused);
            Assert.IsTrue(Regex.IsMatch(g.ToString(), expectedPauseStatusFalseTestStringPattern));
        }
    }
}
