using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to Moderator-Client every 3 seconds to 
    /// inform the Moderator-Client about the amount of PlayerAudience members that already 
    /// connected to the server. This message is only sent in the time-frame after the 
    /// <see cref="Messages.SessionOpenedMessage"/>, and before the 
    /// <see cref="Messages.GameStartedMessage"/>, was received by the Moderator-Client.
    /// </summary>
    public class AudienceStatusMessage : MessageContainer
    {
        public int AudienceCount { get; private set; }

        /// <summary>
        /// Constructs a new AudienceStatusMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="audienceCount">The amount of PlayerAudience members that connected to the 
        /// current session.></param>
        public AudienceStatusMessage(Guid moderatorId, int audienceCount) : this(moderatorId, audienceCount, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new AudienceStatusMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="audienceCount">The amount of PlayerAudience members that connected to the 
        /// current session.></param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public AudienceStatusMessage(Guid moderatorId, int audienceCount, string debugMessage) : base(moderatorId, MessageType.AudienceStatus, debugMessage)
        {
            AudienceCount = audienceCount;
        }

        public override string ToString()
        {
            return "AudienceStatusMessage [<container>: " + base.ToString() + ", AudienceCount: " + AudienceCount + "]";
        }
    }
}
