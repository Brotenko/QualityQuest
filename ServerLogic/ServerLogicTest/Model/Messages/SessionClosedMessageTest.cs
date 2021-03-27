using Microsoft.VisualStudio.TestTools.UnitTesting;
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
        // private static readonly Dictionary<string, int> testStatistics =
        //    new Dictionary<string, int>() { { "string1", 1 }, { "string2", 2 } };
        /* private static readonly Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> testStatistics =
             new Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>
             {
                 //{
                     (//new Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>(
                         new KeyValuePair<Guid, string>(new Guid(), "string1"),
                         new Dictionary<KeyValuePair<Guid, string>,int >((new KeyValuePair<Guid, string>(new Guid(), "string1")), 1))
                 //}//,
                 //{"string2", 2}
             };*/
        private static readonly Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> testStatistics = new();
            
        private static readonly string dictToString =
            "{" + string.Join(",", testStatistics.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";

        private readonly string expectedStringPattern = @"SessionClosedMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: SessionClosed, Date: \d{4}\.\d{2}\.\d{2}\s{1}\d{2}\:\d{2}\:\d{2}\], Statistics: " + dictToString + @"\]";


        /// <summary>
        /// Initializes the testStatistics object in multiple, better "readable" steps; 
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            KeyValuePair<Guid, string> promptOne = new KeyValuePair<Guid, string>(new Guid(), "string1");
            KeyValuePair<Guid, string> promptOneOptionOne = new KeyValuePair<Guid, string>(new Guid(), "string1_option_1");
            KeyValuePair<Guid, string> promptOneOptionTwo = new KeyValuePair<Guid, string>(new Guid(), "string1_option_2");
            
            KeyValuePair<Guid, string> promptTwo = new KeyValuePair<Guid, string>(new Guid(), "string2");
            KeyValuePair<Guid, string> promptTwoOptionOne = new KeyValuePair<Guid, string>(new Guid(), "string2_option_1");
            KeyValuePair<Guid, string> promptTwoOptionTwo = new KeyValuePair<Guid, string>(new Guid(), "string2_option_2");

            Dictionary<KeyValuePair<Guid, string>, int> votesOnPromptOneOptions = new();
            votesOnPromptOneOptions.Add(promptOneOptionOne, 4);
            votesOnPromptOneOptions.Add(promptOneOptionTwo,2);

            Dictionary<KeyValuePair<Guid, string>, int> votesOnPromptTwoOptions = new();
            votesOnPromptOneOptions.Add(promptTwoOptionOne, 4);
            votesOnPromptOneOptions.Add(promptTwoOptionTwo, 2);

            testStatistics.Add(promptOne, votesOnPromptOneOptions);
            testStatistics.Add(promptTwo, votesOnPromptTwoOptions);

        }

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
