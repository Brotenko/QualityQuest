using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
    /// <see cref="Messages.RequestServerStatusMessage"/> to confirm that the ServerLogic is 
    /// available for a connection.
    /// </summary>
    public class ServerStatusMessage : MessageContainer
    {
       /// <summary>
        /// Constructs a new ServerStatusMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
       public ServerStatusMessage(Guid moderatorId) : base(moderatorId, MessageType.ServerStatus)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "ServerStatusMessage [<container>: " + base.ToString() + "]";
        }
    }
}
