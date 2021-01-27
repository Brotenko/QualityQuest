using System;
using System.Collections.Generic;
using System.Text;

namespace PAClient
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionNotFoundException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public SessionNotFoundException()
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SessionNotFoundException(string message) : base(message)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public SessionNotFoundException(string message, Exception inner) : base(message, inner)
        {
            /* FALL THROUGH */
        }
    }
}
