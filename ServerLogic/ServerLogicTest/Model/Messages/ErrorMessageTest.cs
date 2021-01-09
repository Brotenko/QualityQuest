﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerLogic.Model;
using ServerLogic.Model.Messages;
using System;
using System.Text.RegularExpressions;

namespace ServerLogicTests.Model.Messages
{
    /// <summary>
    /// Tests the basic parsing and construction of the <c>ErrorMessage</c>, to 
    /// ensure they are able to parse valid messages.
    /// </summary>
    [TestClass]
    public class ErrorMessageTest
    {
        private static readonly Guid testGuid = Guid.NewGuid();

        private const ErrorType WrongPasswordError = ErrorType.WrongPassword;
        private const ErrorType UnknownGuidError = ErrorType.UnknownGuid;
        private const ErrorType IllegalPauseActionError = ErrorType.IllegalPauseAction;
        private const ErrorType SessionDoesNotExistError = ErrorType.SessionDoesNotExist;
        private const ErrorType NewModeratorError = ErrorType.NewModerator;
        private const ErrorType IllegalMessageError = ErrorType.IllegalMessage;
        private const string ErrorMessageText = "TestTestTest 123456";

        private readonly string expectedWrongPasswordErrorStringPattern = @"ErrorMessage \[<container>: MessageContainer \[ModeratorId: " + 
            testGuid + @", Type: Error, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], ErrorMessageType: " +
            WrongPasswordError + @", ErrorMessageText: " + ErrorMessageText + @"\]";
        private readonly string expectedUnknownGuidErrorStringPattern = @"ErrorMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: Error, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], ErrorMessageType: " +
            UnknownGuidError + @", ErrorMessageText: " + ErrorMessageText + @"\]";
        private readonly string expectedIllegalPauseActionErrorStringPattern = @"ErrorMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: Error, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], ErrorMessageType: " +
            IllegalPauseActionError + @", ErrorMessageText: " + ErrorMessageText + @"\]";
        private readonly string expectedSessionDoesNotExistErrorStringPattern = @"ErrorMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: Error, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], ErrorMessageType: " +
            SessionDoesNotExistError + @", ErrorMessageText: " + ErrorMessageText + @"\]";
        private readonly string expectedNewModeratorErrorStringPattern = @"ErrorMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: Error, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], ErrorMessageType: " +
            NewModeratorError + @", ErrorMessageText: " + ErrorMessageText + @"\]";
        private readonly string expectedIllegalMessageErrorStringPattern = @"ErrorMessage \[<container>: MessageContainer \[ModeratorId: " +
            testGuid + @", Type: Error, Date: \d{2}\.\d{2}\.\d{4}\s{1}\d{2}\:\d{2}\:\d{2}, Debug: \], ErrorMessageType: " +
            IllegalMessageError + @", ErrorMessageText: " + ErrorMessageText + @"\]";

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void WrongPasswordErrorTest()
        {
            ErrorMessage e = new ErrorMessage(testGuid, WrongPasswordError, ErrorMessageText);

            Assert.AreEqual(e.ErrorMessageType, WrongPasswordError);
            Assert.IsTrue(Regex.IsMatch(e.ToString(), expectedWrongPasswordErrorStringPattern));
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void UnknownGuidErrorTest()
        {
            ErrorMessage e = new ErrorMessage(testGuid, UnknownGuidError, ErrorMessageText);

            Assert.AreEqual(e.ErrorMessageType, UnknownGuidError);
            Assert.IsTrue(Regex.IsMatch(e.ToString(), expectedUnknownGuidErrorStringPattern));
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void IllegalPauseActionErrorTest()
        {
            ErrorMessage e = new ErrorMessage(testGuid, IllegalPauseActionError, ErrorMessageText);

            Assert.AreEqual(e.ErrorMessageType, IllegalPauseActionError);
            Assert.IsTrue(Regex.IsMatch(e.ToString(), expectedIllegalPauseActionErrorStringPattern));
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void SessionDoesNotExistErrorTest()
        {
            ErrorMessage e = new ErrorMessage(testGuid, SessionDoesNotExistError, ErrorMessageText);

            Assert.AreEqual(e.ErrorMessageType, SessionDoesNotExistError);
            Assert.IsTrue(Regex.IsMatch(e.ToString(), expectedSessionDoesNotExistErrorStringPattern));
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void NewModeratorErrorTest()
        {
            ErrorMessage e = new ErrorMessage(testGuid, NewModeratorError, ErrorMessageText);

            Assert.AreEqual(e.ErrorMessageType, NewModeratorError);
            Assert.IsTrue(Regex.IsMatch(e.ToString(), expectedNewModeratorErrorStringPattern));
        }

        /// <summary>
        /// Validates that the constructed message contains all the provided
        /// test-variables, at the correct position and with the correct value,
        /// and also validates that the <c>ToString()</c> method of the message
        /// returns a well-formed string, according to the expectations.
        /// </summary>
        [TestMethod]
        public void IllegalMessageErrorTest()
        {
            ErrorMessage e = new ErrorMessage(testGuid, IllegalMessageError, ErrorMessageText);

            Assert.AreEqual(e.ErrorMessageType, IllegalMessageError);
            Assert.IsTrue(Regex.IsMatch(e.ToString(), expectedIllegalMessageErrorStringPattern));
        }
    }
}