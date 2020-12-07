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
    public class ReconnectMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"ReconnectMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: Reconnect, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            ReconnectMessage r = new ReconnectMessage(testGuid);

            Assert.IsNotNull(r.ToString());
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedStringPattern));
        }
    }
}
