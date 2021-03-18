using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>AudienceStatusMessage</c>, 
    /// to ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class AudienceStatusMessageTest
    {
        private const int TestAudienceCount = 20;
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"AudienceStatusMessage \[<container>: MessageContainer \[ModeratorId: " + testGuid + 
            @", Type: AudienceStatus, Date: \d{4}\.\d{2}\.\d{2}\s{1}\d{2}\:\d{2}\:\d{2}], AudienceCount: " + TestAudienceCount + @"\]";

        /// <summary>
        /// Validates that the assigned test-variable is the same before and after
        /// construction of the message.
        /// </summary>
        [TestMethod]
        public void AudienceCountTest()
        {
            AudienceStatusMessage a = new AudienceStatusMessage(testGuid, TestAudienceCount);

            Assert.AreEqual(a.AudienceCount, TestAudienceCount);
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
            AudienceStatusMessage a = new AudienceStatusMessage(testGuid, TestAudienceCount);

            Assert.IsNotNull(a.ToString());
            Assert.IsTrue(Regex.IsMatch(a.ToString(), expectedStringPattern));
        }
    }
}
