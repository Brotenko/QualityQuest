using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class RequestStartVotingMessage : MessageContainer
    {
        public int VotingTime { get; private set; }
        public Dictionary<Guid, string> VotingOptions { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="votingTime"></param>
        /// <param name="votingOptions"></param>
        public RequestStartVotingMessage(Guid moderatorId, int votingTime, Dictionary<Guid, string> votingOptions) : this(moderatorId, votingTime, votingOptions, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="votingTime"></param>
        /// <param name="votingOptions"></param>
        /// <param name="debugMessage"></param>
        public RequestStartVotingMessage(Guid moderatorId, int votingTime, Dictionary<Guid, string> votingOptions, string debugMessage) : base(moderatorId, MessageType.RequestStartVoting, debugMessage)
        {
            VotingTime = votingTime;
            VotingOptions = votingOptions;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string dictToString = "{" + string.Join(",", VotingOptions.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            return "RequestStartVotingMessage [<container>: " + base.ToString() + ", VotingTime: " + VotingTime + ", VotingOptions: " + dictToString + "]";
        }
    }
}
