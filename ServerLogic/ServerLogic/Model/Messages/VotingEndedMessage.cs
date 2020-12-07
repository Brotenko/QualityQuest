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
    public class VotingEndedMessage : MessageContainer
    {
        public Guid WinningOption { get; private set; }
        public Dictionary<Guid, int> VotingResults { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="winningOption"></param>
        /// <param name="votingResults"></param>
        public VotingEndedMessage(Guid moderatorId, Guid winningOption, Dictionary<Guid, int> votingResults) : this(moderatorId, winningOption, votingResults, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="winningOption"></param>
        /// <param name="votingResults"></param>
        /// <param name="debugMessage"></param>
        public VotingEndedMessage(Guid moderatorId, Guid winningOption, Dictionary<Guid, int> votingResults, string debugMessage) : base(moderatorId, MessageType.VotingEnded, debugMessage)
        {
            WinningOption = winningOption;
            VotingResults = votingResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string dictToString = "{" + string.Join(",", VotingResults.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            return "VotingEndedMessage [<container>: " + base.ToString() + ", WinningOption: " + WinningOption + ", VotingResults:" + dictToString + "]";
        }
    }
}
