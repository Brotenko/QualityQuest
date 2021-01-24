using System;
using System.Collections.Generic;
using System.Text;

namespace PAClient
{
    class SessionNotFoundException : Exception
    {
        public SessionNotFoundException()
        {
            /* FALL THROUGH */
        }

        public SessionNotFoundException(string message) : base(message)
        {
            /* FALL THROUGH */
        }

        public SessionNotFoundException(string message, Exception inner) : base(message, inner)
        {
            /* FALL THROUGH */
        }
    }
}
