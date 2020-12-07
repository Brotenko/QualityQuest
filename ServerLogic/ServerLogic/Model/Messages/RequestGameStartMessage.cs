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
    public class RequestGameStartMessage : MessageContainer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        public RequestGameStartMessage(Guid moderatorId) : this(moderatorId, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="debugMessage"></param>
        public RequestGameStartMessage(Guid moderatorId, string debugMessage) : base(moderatorId, MessageType.RequestGameStart, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RequestGameStartMessage [<container>: " + base.ToString() + "]";
        }
    }
}
