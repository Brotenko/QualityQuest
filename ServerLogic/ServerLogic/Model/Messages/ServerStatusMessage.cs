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
    public class ServerStatusMessage : MessageContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        public ServerStatusMessage(Guid moderatorId) : this(moderatorId, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="debugMessage"></param>
        public ServerStatusMessage(Guid moderatorId, string debugMessage) : base(moderatorId, MessageType.ServerStatus, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ServerStatusMessage [<container>: " + base.ToString() + "]";
        }
    }
}
