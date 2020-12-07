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
    public class RequestCloseSessionMessageTest
    {
        private static Random random = new Random();

        /// <summary>
        /// 
        /// </summary>
        private static string RandomString(int length)
        {
            const string availableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(availableCharacters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static readonly Guid testGuid = Guid.NewGuid();
        private static readonly string sessionKey = RandomString(6);
        private readonly string expectedStringPattern = @"RequestCloseSessionMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestCloseSession, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], SessionKey: " + sessionKey + @"\]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void SessionKeyTest()
        {
            RequestCloseSessionMessage r = new RequestCloseSessionMessage(testGuid, sessionKey);

            Assert.AreEqual(r.SessionKey, sessionKey);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            RequestCloseSessionMessage r = new RequestCloseSessionMessage(testGuid, sessionKey);

            Assert.IsNotNull(r.ToString());
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedStringPattern));
        }
    }
}
