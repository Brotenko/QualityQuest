﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>ReconnectSuccessfulMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class ReconnectSuccessfulMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"ReconnectSuccessfulMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: ReconnectSuccessful, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            ReconnectSuccessfulMessage r = new ReconnectSuccessfulMessage(testGuid);

            Assert.IsNotNull(r.ToString());
            Assert.IsTrue(Regex.IsMatch(r.ToString(), expectedStringPattern));
        }
    }
}