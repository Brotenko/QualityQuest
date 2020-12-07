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
    public class SessionClosedMessage : MessageContainer
    {
        public Dictionary<string, int> Statistics { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="statistics"></param>
        public SessionClosedMessage(Guid moderatorId, Dictionary<string, int> statistics) : this(moderatorId, statistics, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="statistics"></param>
        /// <param name="debugMessage"></param>
        public SessionClosedMessage(Guid moderatorId, Dictionary<string, int> statistics, string debugMessage) : base(moderatorId, MessageType.SessionClosed, debugMessage)
        {
            Statistics = statistics;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string dictToString = "{" + string.Join(",", Statistics.Select(kv => kv.Key + "=" + kv.Value).ToArray()) + "}";
            return "SessionClosedMessage [<container>: " + base.ToString() + ", Statistics: " + dictToString + "]";
        }
    }
}
