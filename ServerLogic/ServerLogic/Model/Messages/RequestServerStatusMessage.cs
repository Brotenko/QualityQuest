using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the Moderator-Client to the ServerLogic if there is currently 
    /// no connection to a ServerLogic. This message is sent to the ServerLogic at regular intervals 
    /// until the ServerLogic returns a response in form of a <see cref="Messages.ServerStatusMessage"/>. 
    /// If a <see cref="Messages.ServerStatusMessage"/> is received by the Moderator-Client at any given 
    /// time, the moderator is notified that a connection to the ServerLogic is possible, and at the same 
    /// time, <see cref="Messages.RequestServerStatus"/> messages are stopped being sent to the 
    /// ServerLogic.
    /// </summary>
    public class RequestServerStatusMessage : MessageContainer
    {
        /// <summary>
        /// Constructs a new RequestServerStatusMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        public RequestServerStatusMessage(Guid moderatorId) : this(moderatorId,  "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new RequestServerStatusMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public RequestServerStatusMessage(Guid moderatorId, string debugMessage) : base(moderatorId, MessageType.RequestServerStatus, debugMessage)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "RequestServerStatusMessage [<container>: " + base.ToString() + "]";
        }
    }
}
