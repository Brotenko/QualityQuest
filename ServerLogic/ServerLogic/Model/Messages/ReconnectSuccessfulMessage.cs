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
    public class ReconnectSuccessfulMessage : MessageContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        public ReconnectSuccessfulMessage(Guid moderatorId) : this(moderatorId, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="debugMessage"></param>
        public ReconnectSuccessfulMessage(Guid moderatorId, string debugMessage) : base(moderatorId, MessageType.ReconnectSuccessful, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ReconnectSuccessfulMessage [<container>: " + base.ToString() + "]";
        }
    }
}
