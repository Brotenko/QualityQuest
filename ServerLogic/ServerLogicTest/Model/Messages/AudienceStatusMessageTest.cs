using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    [TestClass]
    public class AudienceStatusMessageTest
    {
        private const int TestAudienceCount = 20;
        private static readonly Guid testGuid = Guid.NewGuid();
        private readonly string expectedStringPattern = @"AudienceStatusMessage \[<container>: MessageContainer \[ModeratorId: " + testGuid + 
            @", Type: AudienceStatus, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], AudienceCount: " + TestAudienceCount + @"\]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void AudienceCountTest()
        {
            AudienceStatusMessage a = new AudienceStatusMessage(testGuid, TestAudienceCount);

            Assert.AreEqual(a.AudienceCount, TestAudienceCount);
        }

        /// <summary>
        /// 
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
