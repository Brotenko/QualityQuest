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
    public class RequestServerStatusMessage : MessageContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        public RequestServerStatusMessage(Guid moderatorId) : this(moderatorId,  "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="debugMessage"></param>
        public RequestServerStatusMessage(Guid moderatorId, string debugMessage) : base(moderatorId, MessageType.RequestServerStatus, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RequestServerStatusMessage [<container>: " + base.ToString() + "]";
        }
    }
}
