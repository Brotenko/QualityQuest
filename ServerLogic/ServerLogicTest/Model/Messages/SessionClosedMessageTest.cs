﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>SessionClosedMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class SessionClosedMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private static readonly Dictionary<string, int> testStatistics =
            new Dictionary<string, int>() { { "string1", 1 }, { "string2", 2 } };

        private static readonly string dictToString =
            "{" + string.Join(",", testStatistics.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";

        private readonly string expectedStringPattern = @"SessionClosedMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: SessionClosed, Date: \d{4}\.\d{2}\.\d{2}\s{1}\d{2}\:\d{2}\:\d{2}\], Statistics: " + dictToString + @"\]";

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void StatisticsTest()
        {
            SessionClosedMessage s = new SessionClosedMessage(testGuid, testStatistics);

            Assert.IsNotNull(s.Statistics);
            Assert.AreEqual(s.Statistics, testStatistics);
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
            SessionClosedMessage s = new SessionClosedMessage(testGuid, testStatistics);

            Assert.IsNotNull(s.ToString());
            Assert.IsTrue(Regex.IsMatch(s.ToString(), expectedStringPattern));
        }
    }
}
