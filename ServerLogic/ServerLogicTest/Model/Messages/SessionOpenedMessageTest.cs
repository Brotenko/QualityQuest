﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Drawing;
using System.Text.RegularExpressions;
using ServerLogic.Model.Messages;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>SessionOpenedMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class SessionOpenedMessageTest
    {
        private static readonly Random random = new Random();

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
        private static readonly string testKey = RandomString(6);
        private static readonly Uri testURL = new Uri("https://www.google.com/");
        private readonly string expectedStringPattern = @"SessionOpenedMessage \[<container>: MessageContainer \[ModeratorId: " +
                                                        testGuid + @", Type: SessionOpened, Date: \d{4}\.\d{2}\.\d{2}\s{1}\d{2}\:\d{2}\:\d{2}\], SessionKey: " + 
                                                        testKey + @", DirectURL: " + testURL + @"\]";

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void KeyTest()
        {
            SessionOpenedMessage s = new SessionOpenedMessage(testGuid, testKey, testURL/*, null*/);

            Assert.IsNotNull(s.SessionKey);
            Assert.AreEqual(s.SessionKey, testKey);
        }

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void URLTest()
        {
            SessionOpenedMessage s = new SessionOpenedMessage(testGuid, testKey, testURL/*, null*/);

            Assert.IsNotNull(s.DirectURL);
            Assert.AreEqual(s.DirectURL, testURL);
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
            SessionOpenedMessage s = new SessionOpenedMessage(testGuid, testKey, testURL/*, null*/);

            Assert.IsNotNull(s.ToString());
            Assert.IsTrue(Regex.IsMatch(s.ToString(), expectedStringPattern));
        }
    }
}
