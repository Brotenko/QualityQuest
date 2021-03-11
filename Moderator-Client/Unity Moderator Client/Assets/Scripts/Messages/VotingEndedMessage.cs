using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
    /// <see cref="Messages.RequestStartVotingMessage"/>, after the voting time has expired. The 
    /// winning option and the statistical results of the vote are sent back to the 
    /// Moderator-Client.
    /// </summary>
    public class VotingEndedMessage : MessageContainer
    {
        public Guid WinningOption { get; }
        public Dictionary<Guid, int> VotingResults { get; }

        /// <summary>
        /// Constructs a new VotingEndedMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="winningOption">The GUID of the option that got the most votes from the 
        /// PlayerAudience.</param>
        /// 
        /// <param name="votingResults">Contains the GUIDs of the option as the key and the 
        /// respective amount of received votes as the value.</param>
        public VotingEndedMessage(Guid moderatorId, Guid winningOption, Dictionary<Guid, int> votingResults) : this(moderatorId, winningOption, votingResults, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new VotingEndedMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="winningOption">The GUID of the option that got the most votes from the 
        /// PlayerAudience.</param>
        /// 
        /// <param name="votingResults">Contains the GUIDs of the option as the key and the 
        /// respective amount of received votes as the value.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public VotingEndedMessage(Guid moderatorId, Guid winningOption, Dictionary<Guid, int> votingResults, string debugMessage) : base(moderatorId, MessageType.VotingEnded, debugMessage)
        {
            WinningOption = winningOption;
            VotingResults = votingResults;
        }

        public override string ToString()
        {
            string dictToString = "{" + string.Join(",", VotingResults.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            return "VotingEndedMessage [<container>: " + base.ToString() + ", WinningOption: " + WinningOption + ", VotingResults:" + dictToString + "]";
        }
    }
}
