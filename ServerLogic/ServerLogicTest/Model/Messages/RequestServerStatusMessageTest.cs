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
    public class RequestServerStatusMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"RequestServerStatusMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestServerStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            RequestServerStatusMessage r = new RequestServerStatusMessage(testGuid);

            Assert.IsNotNull(r.ToString());
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedStringPattern));
        }
    }
}
