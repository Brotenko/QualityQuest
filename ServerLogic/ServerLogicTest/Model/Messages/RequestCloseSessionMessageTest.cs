using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>RequestCloseSessionMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class RequestCloseSessionMessageTest
    {
        private static Random random = new Random();

        /// <summary>
        /// Creates a random string, equal to the provided length, consisting of all
        /// uppercase characters (in the english alphabet) and the numbers 0-9.
        /// </summary>
        private static string RandomString(int length)
        {
            const string availableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(availableCharacters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static readonly Guid testGuid = Guid.NewGuid();
        private static readonly string sessionKey = RandomString(6);
        private readonly string expectedStringPattern = @"RequestCloseSessionMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: RequestCloseSession, Date: \d{4}\.\d{2}\.\d{2}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], SessionKey: " + sessionKey + @"\]";

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void SessionKeyTest()
        {
            RequestCloseSessionMessage r = new RequestCloseSessionMessage(testGuid, sessionKey);

            Assert.AreEqual(r.SessionKey, sessionKey);
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
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
