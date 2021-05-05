using System;
using System.Collections.Generic;
using System.Linq;

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
        public KeyValuePair<Guid, string> VotingPrompt;
        public KeyValuePair<Guid, string>[] VotingOptions { get; }

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
        /// <param name="votingPrompt">The Guid and string of the prompt to be voted on.</param>
        /// <param name="votingOptions">Contains the GUIDs of the respective voting option as the 
        /// key and textual description of the voting option as the value.</param>
        public RequestStartVotingMessage(Guid moderatorId, int votingTime, KeyValuePair<Guid, string> votingPrompt, KeyValuePair<Guid, string>[] votingOptions) : base(moderatorId, MessageType.RequestStartVoting)
        {
            VotingTime = votingTime;
            VotingOptions = votingOptions;
            VotingPrompt = votingPrompt;
        }

        public override string ToString()
        {
            string dictToString = "{" + string.Join(",", VotingOptions.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            return "RequestStartVotingMessage [<container>: " + base.ToString() + ", VotingTime: " + VotingTime + ", VotingOptions: " + dictToString + "]";
        }
    }
}
