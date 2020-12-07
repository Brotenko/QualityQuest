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
    public class RequestOpenSessionMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string password = "asdfasdfTestTEST 123 456";
        private readonly string expectedStringPattern = @"RequestOpenSessionMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestOpenSession, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void PasswordTest()
        {
            RequestOpenSessionMessage r = new RequestOpenSessionMessage(testGuid, password);

            Assert.IsNotNull(r.ToString());
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedStringPattern));
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            RequestOpenSessionMessage r = new RequestOpenSessionMessage(testGuid, password);

            Assert.IsNotNull(r.ToString());
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedStringPattern));
        }
    }
}
