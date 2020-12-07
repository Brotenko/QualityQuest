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
    public class RequestCloseSessionMessage : MessageContainer
    {
        public string SessionKey { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="sessionKey"></param>
        public RequestCloseSessionMessage(Guid moderatorId, string sessionKey) : this(moderatorId, sessionKey, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="sessionKey"></param>
        /// <param name="debugMessage"></param>
        public RequestCloseSessionMessage(Guid moderatorId, string sessionKey, string debugMessage) : base(moderatorId, MessageType.RequestCloseSession, debugMessage)
        {
            SessionKey = sessionKey;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RequestCloseSessionMessage [<container>: " + base.ToString() + ", SessionKey: " + SessionKey + "]";
        }
    }
}
