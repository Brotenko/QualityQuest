
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
    public class ReconnectMessage : MessageContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        public ReconnectMessage(Guid moderatorId) : this(moderatorId, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="debugMessage"></param>
        public ReconnectMessage(Guid moderatorId, string debugMessage) : base(moderatorId, MessageType.Reconnect, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ReconnectMessage [<container>: " + base.ToString() + "]";
        }
    }
}
