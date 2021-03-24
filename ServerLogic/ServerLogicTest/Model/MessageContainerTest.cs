﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>MessageContainer</c>, to 
    /// ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class MessageContainerTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private const MessageType testType = MessageType.AudienceStatus;
        private static readonly DateTime testDate = DateTime.Now;
        private static readonly string testMessage = "This is a debug test 123456 =";
        private readonly string expectedStringPattern = @"MessageContainer \[ModeratorId: " +
            testGuid + @", Type: " + testType + @", Date: " + testDate.ToString("yyyy.MM.dd hh:mm:ss") + @", Debug: " + testMessage + @"\]";

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void ModeratorIdTest()
        {
            MessageContainer m = new MessageContainer(testGuid, testType);

            Assert.AreEqual(m.ModeratorID, testGuid);
        }

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void TypeTest()
        {
            MessageContainer m = new MessageContainer(testGuid, testType);

            Assert.AreEqual(m.Type, testType);
        }

        /// <summary>
        /// Validates that the assigned test-variables are the same before and after
        /// construction of the messages.
        /// </summary>
        [TestMethod]
        public void CreationDateTest()
        {
            MessageContainer m_1 = new MessageContainer(testGuid, testType);
            MessageContainer m_2 = new MessageContainer(testGuid, testType, testDate, "");

            Assert.IsNotNull(m_1.CreationDate);
            Assert.IsInstanceOfType(m_1.CreationDate, typeof(DateTime));

            Assert.IsNotNull(m_2.CreationDate);
            Assert.AreEqual(m_2.CreationDate, testDate);
        }

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void DebugMessageTest()
        {
            MessageContainer m = new MessageContainer(testGuid, testType, testDate, testMessage);

            Assert.IsNotNull(m.DebugMessage);
            Assert.AreEqual(m.DebugMessage, testMessage);
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
            MessageContainer m = new MessageContainer(testGuid, testType, testDate, testMessage);

            Assert.IsNotNull(m.ToString());
            Assert.IsTrue(Regex.IsMatch(m.ToString(), expectedStringPattern));
        }
    }
}
