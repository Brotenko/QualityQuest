using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using PAClient;
using System.Threading.Tasks;

namespace PAClientTest
{
    /// <summary>
    /// SessionNotFoundException Tests
    /// </summary>
    [TestClass]
    public sealed class SessionNotFoundExceptionTest
    {
        /// <summary>
        /// Validates that the <see cref="SessionNotFoundException"/> is
        /// thrown correctly.
        /// </summary>
        [TestMethod]
        public void SessionNotFoundException_Test1()
        {
            Assert.ThrowsException<SessionNotFoundException>(() => throw new SessionNotFoundException());
        }

        /// <summary>
        /// Validates that the <see cref="SessionNotFoundException"/> is
        /// thrown correctly.
        /// </summary>
        [TestMethod]
        public void SessionNotFoundException_Test2()
        {
            Assert.ThrowsException<SessionNotFoundException>(() => throw new SessionNotFoundException("Test message!"));
        }

        /// <summary>
        /// Validates that the <see cref="SessionNotFoundException"/> is
        /// thrown correctly.
        /// </summary>
        [TestMethod]
        public void SessionNotFoundException_Test3()
        {
            Assert.ThrowsException<SessionNotFoundException>(() => throw new SessionNotFoundException("Test message!", new Exception()));
        }
    }
}
