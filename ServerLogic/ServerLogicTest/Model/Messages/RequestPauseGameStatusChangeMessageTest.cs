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
    public class RequestPauseGameStatusChangeMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedPauseStatusTrueTestStringPattern = @"RequestPauseGameStatusChangeMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestPauseGameStatusChange, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePausedStatus: True\]";
        private readonly string expectedPauseStatusFalseTestStringPattern = @"RequestPauseGameStatusChangeMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestPauseGameStatusChange, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], GamePausedStatus: False\]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void PauseStatusTrueTest()
        {
            RequestPauseGameStatusChangeMessage r = new RequestPauseGameStatusChangeMessage(testGuid, true);

            Assert.IsTrue(r.GamePausedStatus);
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedPauseStatusTrueTestStringPattern));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void PauseStatusFalseTest()
        {
            RequestPauseGameStatusChangeMessage r = new RequestPauseGameStatusChangeMessage(testGuid, false);

            Assert.IsFalse(r.GamePausedStatus);
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedPauseStatusFalseTestStringPattern));
        }
    }
}
