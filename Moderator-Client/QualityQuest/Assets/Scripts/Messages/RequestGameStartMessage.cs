using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageContainer.Messages
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
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
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
