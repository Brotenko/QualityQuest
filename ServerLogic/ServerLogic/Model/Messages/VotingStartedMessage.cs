using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
    /// <see cref="Messages.RequestStartVotingMessage"/> to confirm the start of a voting phase 
    /// with the provided voting options.
    /// </summary>
    public class VotingStartedMessage : MessageContainer
    {
       /// <summary>
        /// Constructs a new VotingStartedMessage.
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
        public VotingStartedMessage(Guid moderatorId) : base(moderatorId, MessageType.VotingStarted)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "VotingStartedMessage [<container>: " + base.ToString() + "]";
        }
    }
}
