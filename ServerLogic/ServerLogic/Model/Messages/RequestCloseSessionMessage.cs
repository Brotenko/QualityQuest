using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the Moderator-Client to the ServerLogic to tell the ServerLogic 
    /// to close the Online-Session and with that the connection to the PlayerAudience-Clients. 
    /// It also commands the ServerLogic to clear the logs.
    /// </summary>
    public class RequestCloseSessionMessage : MessageContainer
    {
        public string SessionKey { get; }
        
        /// <summary>
        /// Constructs a new RequestCloseSessionMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="sessionKey">The key of the to be closed Online-Session.</param>
        public RequestCloseSessionMessage(Guid moderatorId, string sessionKey) : base(moderatorId, MessageType.RequestCloseSession)
        {
            SessionKey = sessionKey;
        }

        public override string ToString()
        {
            return "RequestCloseSessionMessage [<container>: " + base.ToString() + ", SessionKey: " + SessionKey + "]";
        }
    }
}
