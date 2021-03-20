
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the Moderator-Client to the ServerLogic to reestablish a lost 
    /// connection. For this purpose, the Moderator-Client's GUID is required for comparison with 
    /// the previously saved Moderator-Client GUID. This message shall only be sent when the 
    /// Moderator-Client is still in-game, otherwise a new Online-Session has to be opened through 
    /// a <see cref="Messages.RequestOpenSessionMessage"/>.
    /// </summary>
    public class ReconnectMessage : MessageContainer
    {

        /// <summary>
        /// Constructs a new ReconnectMessage.
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
        public ReconnectMessage(Guid moderatorId) : base(moderatorId, MessageType.Reconnect)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "ReconnectMessage [<container>: " + base.ToString() + "]";
        }
    }
}
