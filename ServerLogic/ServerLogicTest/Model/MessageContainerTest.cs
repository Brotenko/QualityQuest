using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model;
using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.CodeCoverage;
using Pose;
using Shim = Pose.Shim;

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
        private readonly string expectedStringPattern = @"MessageContainer \[ModeratorId: " +
                                                        testGuid + @", Type: " + testType + @", Date: " + testDate.ToString("yyyy.MM.dd HH:mm:ss") + @"\]";

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
            //As Date ist set inside the MessageContainer-Class, we mock with Pose the DateTime.Now-Method and set it to always return the Value in 'testDateTime'
            DateTime testDateTime = new DateTime(2012, 12, 21, 11, 50, 0);
            Shim dateTimeShim = Shim.Replace(() => DateTime.Now).With(() => testDateTime);

            // Objects initialized inside PoseContext.Isolate won't be visible outside of it, so we override already initialized Objects,
            // as Assert sadly doesn't seem to work inside PoseContext.Isolate.
            MessageContainer m_1 = new MessageContainer(testGuid, testType);
            MessageContainer m_2 = new MessageContainer(testGuid, testType);

            PoseContext.Isolate(() =>
            {
                m_1 = new MessageContainer(testGuid, testType);
                m_2 = new MessageContainer(testGuid, testType);
            }, dateTimeShim);

            Assert.IsNotNull(m_1.CreationDate);
            Assert.IsInstanceOfType(m_1.CreationDate, typeof(DateTime));

            Assert.IsNotNull(m_2.CreationDate);
            Assert.AreEqual(m_2.CreationDate, testDateTime);
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
            MessageContainer m = new MessageContainer(testGuid, testType);

            Assert.IsNotNull(m.ToString());
            Assert.IsTrue(Regex.IsMatch(m.ToString(), expectedStringPattern));
        }
    }
}
