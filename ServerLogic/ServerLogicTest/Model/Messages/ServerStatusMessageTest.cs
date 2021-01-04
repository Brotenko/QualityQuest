using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>ServerStatusMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class ServerStatusMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"ServerStatusMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: ServerStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \]";

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
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
