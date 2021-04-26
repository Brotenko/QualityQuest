using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the the ServerLogic to the Moderator-Client to confirm that a 
    /// lost connection has been reestablished.
    /// </summary>
    public class ReconnectSuccessfulMessage : MessageContainer
    {
        
        /// <summary>
        /// Constructs a new ReconnectSuccessfulMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        public ReconnectSuccessfulMessage(Guid moderatorId) : base(moderatorId, MessageType.ReconnectSuccessful)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "ReconnectSuccessfulMessage [<container>: " + base.ToString() + "]";
        }
    }
}
