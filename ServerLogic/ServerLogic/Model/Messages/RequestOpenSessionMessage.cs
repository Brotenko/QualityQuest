using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestOpenSessionMessage : MessageContainer
    {
        public string Password { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="password"></param>
        public RequestOpenSessionMessage(Guid moderatorId, string password) : this(moderatorId, password, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="password"></param>
        /// <param name="debugMessage"></param>
        public RequestOpenSessionMessage(Guid moderatorId, string password, string debugMessage) : base(moderatorId, MessageType.RequestOpenSession, debugMessage)
        {
            Password = password;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RequestOpenSessionMessage [<container>: " + base.ToString() + ", Password: " + Password + "]";
        }
    }
}
