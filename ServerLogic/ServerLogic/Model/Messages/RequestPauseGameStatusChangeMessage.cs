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
    public class RequestPauseGameStatusChangeMessage : MessageContainer
    {
        public bool GamePausedStatus { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="gamePausedStatus"></param>
        public RequestPauseGameStatusChangeMessage(Guid moderatorId, bool gamePausedStatus) : this(moderatorId, gamePausedStatus, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="gamePausedStatus"></param>
        /// <param name="debugMessage"></param>
        public RequestPauseGameStatusChangeMessage(Guid moderatorId, bool gamePausedStatus, string debugMessage) : base(moderatorId, MessageType.RequestPauseGameStatusChange, debugMessage)
        {
            GamePausedStatus = gamePausedStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "RequestPauseGameStatusChangeMessage [<container>: " + base.ToString() + ", GamePausedStatus: " + GamePausedStatus + "]";
        }
    }
}
