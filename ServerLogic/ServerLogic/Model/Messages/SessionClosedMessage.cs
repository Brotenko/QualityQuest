﻿using System;
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
        public Dictionary<string, int> Statistics { get; private set; }

        /// <summary>
        /// Constructs a new SessionClosedMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="statistics">Contains the id of the option as the key and the respective 
        /// amount of received votes as the value.</param>
        public SessionClosedMessage(Guid moderatorId, Dictionary<string, int> statistics) : this(moderatorId, statistics, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new SessionClosedMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="statistics">Contains the id of the option as the key and the respective 
        /// amount of received votes as the value.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public SessionClosedMessage(Guid moderatorId, Dictionary<string, int> statistics, string debugMessage) : base(moderatorId, MessageType.SessionClosed, debugMessage)
        {
            Statistics = statistics;
        }

        public override string ToString()
        {
            string dictToString = "{" + string.Join(",", Statistics.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            return "SessionClosedMessage [<container>: " + base.ToString() + ", Statistics: " + dictToString + "]";
        }
    }
}
