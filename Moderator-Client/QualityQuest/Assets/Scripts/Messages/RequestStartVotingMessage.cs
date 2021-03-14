using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the Moderator-Client to the ServerLogic to request the start of a 
    /// voting phase. For this purpose the Moderator-Client provides the ServerLogic with different 
    /// options for the audience to choose from. It also provides the ServerLogic with a time-limit 
    /// on how long the PlayerAudience-Clients may vote on the topic.
    /// </summary>
    public class RequestStartVotingMessage : MessageContainer
    {
        public int VotingTime { get; }
        public Dictionary<Guid, string> VotingOptions { get; }

        /// <summary>
        /// Constructs a new RequestStartVotingMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="votingTime">The time in seconds that PlayerAudience-Clients have to cast 
        /// their vote.</param>
        /// 
        /// <param name="votingOptions">Contains the GUIDs of the respective voting option as the 
        /// key and textual description of the voting option as the value.</param>
        public RequestStartVotingMessage(Guid moderatorId, int votingTime, Dictionary<Guid, string> votingOptions) : this(moderatorId, votingTime, votingOptions, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new RequestStartVotingMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="votingTime">The time in seconds that PlayerAudience-Clients have to cast 
        /// their vote.</param>
        /// 
        /// <param name="votingOptions">Contains the GUIDs of the respective voting option as the 
        /// key and textual description of the voting option as the value.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public RequestStartVotingMessage(Guid moderatorId, int votingTime, Dictionary<Guid, string> votingOptions, string debugMessage) : base(moderatorId, MessageType.RequestStartVoting, debugMessage)
        {
            VotingTime = votingTime;
            VotingOptions = votingOptions;
        }

        public override string ToString()
        {
            string dictToString = "{" + string.Join(",", VotingOptions.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            return "RequestStartVotingMessage [<container>: " + base.ToString() + ", VotingTime: " + VotingTime + ", VotingOptions: " + dictToString + "]";
        }
    }
}
