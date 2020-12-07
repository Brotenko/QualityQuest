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
    public class ServerStatusMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"ServerStatusMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: ServerStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            ServerStatusMessage s = new ServerStatusMessage(testGuid);

            Assert.IsNotNull(s.ToString());
            Assert.IsTrue(Regex.IsMatch(s.ToString(), expectedStringPattern));
        }
    }
}
