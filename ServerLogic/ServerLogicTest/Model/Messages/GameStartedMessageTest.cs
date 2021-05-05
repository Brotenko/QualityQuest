using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>GameStartedMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class GameStartedMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"GameStartedMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: GameStarted, Date: \d{4}\.\d{2}\.\d{2}\s{1}\d{2}\:\d{2}\:\d{2}\]";

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void ToStringCorrectness()
        {
            GameStartedMessage g = new GameStartedMessage(testGuid);

            Assert.IsNotNull(g.ToString());
            Assert.IsTrue(Regex.IsMatch(g.ToString(), expectedStringPattern));
        }
    }
}
