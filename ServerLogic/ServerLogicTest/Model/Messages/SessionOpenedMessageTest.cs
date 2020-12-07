using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;
using ServerLogic.Model.Messages;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class SessionOpenedMessageTest
    {
        private static readonly Random random = new Random();

        /// <summary>
        /// 
        /// </summary>
        private static string RandomString(int length)
        {
            const string availableCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(availableCharacters, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private static readonly Guid testGuid = Guid.NewGuid();
        private static readonly string testKey = RandomString(6);
        private static readonly Uri testURL = new Uri("https://www.google.com/");
        //private static readonly Bitmap testQrCode = new Bitmap("");
        private readonly string expectedStringPattern = @"SessionOpenedMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: SessionOpened, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], SessionKey: " + 
            testKey + @", DirectURL: " + testURL + @", QrCode: \]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void KeyTest()
        {
            SessionOpenedMessage s = new SessionOpenedMessage(testGuid, testKey, testURL, null);

            Assert.IsNotNull(s.SessionKey);
            Assert.AreEqual(s.SessionKey, testKey);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void URLTest()
        {
            SessionOpenedMessage s = new SessionOpenedMessage(testGuid, testKey, testURL, null);

            Assert.IsNotNull(s.DirectURL);
            Assert.AreEqual(s.DirectURL, testURL);
        }

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            SessionOpenedMessage s = new SessionOpenedMessage(testGuid, testKey, testURL, null);

            Assert.IsNotNull(s.ToString());
            Assert.IsTrue(Regex.IsMatch(s.ToString(), expectedStringPattern));
        }
    }
}
