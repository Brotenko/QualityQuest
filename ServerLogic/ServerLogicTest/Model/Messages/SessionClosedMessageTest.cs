using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// 
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
            testGuid + @", Type: SessionClosed, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], Statistics: " + dictToString + @"\]";

        /// <summary>
        /// 
        /// </summary>
        [TestMethod]
        public void StatisticsTest()
        {
            SessionClosedMessage s = new SessionClosedMessage(testGuid, testStatistics);

            Assert.IsNotNull(s.Statistics);
            Assert.AreEqual(s.Statistics, testStatistics);
        }

        /// <summary>
        /// 
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
