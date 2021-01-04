using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the 
    /// <c>RequestGamePausedStatusChangeMessage</c>, to ensure they are able to 
    /// parse valid messages.
    /// </summary>
    [TestClass]
    public class RequestGamePausedStatusChangeMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedPauseStatusTrueTestStringPattern = @"RequestGamePausedStatusChangeMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestGamePausedStatusChange, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePaused: True\]";
        private readonly string expectedPauseStatusFalseTestStringPattern = @"RequestGamePausedStatusChangeMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestGamePausedStatusChange, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePaused: False\]";

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void PauseStatusTrueTest()
        {
            RequestGamePausedStatusChangeMessage r = new RequestGamePausedStatusChangeMessage(testGuid, true);

            Assert.IsTrue(r.GamePaused);
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedPauseStatusTrueTestStringPattern));
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
            RequestGamePausedStatusChangeMessage r = new RequestGamePausedStatusChangeMessage(testGuid, false);

            Assert.IsFalse(r.GamePaused);
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedPauseStatusFalseTestStringPattern));
        }
    }
}
