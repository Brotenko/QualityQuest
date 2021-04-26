using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
    /// <see cref="Messages.RequestCloseSessionMessage"/>, to confirm that the Online-Session has 
    /// been successfully closed and that the logs have been cleared completely. In addition to 
    /// that, the statistics of the Online-Session are returned to the Moderator-Client, which 
    /// can be used to display every conducted vote and which option got how many votes.
    /// </summary>
    public class SessionClosedMessage : MessageContainer
    {
        /// <summary>
        /// Constructs a new SessionClosedMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        public SessionClosedMessage(Guid moderatorId) : base(moderatorId, MessageType.SessionClosed)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "SessionClosedMessage [<container>: " + base.ToString() + "]";
        }
    }
}
