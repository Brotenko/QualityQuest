using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the Moderator-Client to the ServerLogic to request the start 
    /// of the game with the current Online-Session.
    /// </summary>
    public class RequestGameStartMessage : MessageContainer
    {
        /// <summary>
        /// Constructs a new RequestGameStartMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        public RequestGameStartMessage(Guid moderatorId) : base(moderatorId, MessageType.RequestGameStart)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "RequestGameStartMessage [<container>: " + base.ToString() + "]";
        }
    }
}
