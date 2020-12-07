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
    public class GameStartedMessage : MessageContainer
    {
        /// <summary>
        ///  
        /// </summary>
        /// <param name="moderatorId"></param>
        public GameStartedMessage(Guid moderatorId) : this(moderatorId, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="debugMessage"></param>
        public GameStartedMessage(Guid moderatorId, string debugMessage) : base(moderatorId, MessageType.GameStarted, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "GameStartedMessage [<container>: " + base.ToString() + "]";
        }
    }
}
