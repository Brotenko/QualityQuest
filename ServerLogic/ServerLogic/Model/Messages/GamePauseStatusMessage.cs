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
    public class GamePauseStatusMessage : MessageContainer
    {
        public bool GamePausedStatus { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="gamePausedStatus"></param>
        public GamePauseStatusMessage(Guid moderatorId, bool gamePausedStatus) : this(moderatorId, gamePausedStatus, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="gamePausedStatus"></param>
        /// <param name="debugMessage"></param>
        public GamePauseStatusMessage(Guid moderatorId, bool gamePausedStatus, string debugMessage) : base(moderatorId, MessageType.GamePauseStatus, debugMessage)
        {
            GamePausedStatus = gamePausedStatus;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "GamePauseStatusMessage [<container>: " + base.ToString() + ", GamePausedStatus: " + GamePausedStatus + "]";
        }
    }
}
