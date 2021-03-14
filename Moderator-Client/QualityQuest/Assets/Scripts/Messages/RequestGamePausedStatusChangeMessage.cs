using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the Moderator-Client to the ServerLogic to switch the game 
    /// between running and being paused.
    /// </summary>
    public class RequestGamePausedStatusChangeMessage : MessageContainer
    {
        public bool GamePaused { get; }

        /// <summary>
        /// Constructs a new RequestPauseGameStatusChangeMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="gamePaused">Specifies whether the game is being paused or whether 
        /// the already paused game is being continued. With true indicating that the game has 
        /// been paused, and false indicating that the game is continuing.</param>
        public RequestGamePausedStatusChangeMessage(Guid moderatorId, bool gamePaused) : this(moderatorId, gamePaused, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new RequestPauseGameStatusChangeMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="gamePaused">Specifies whether the game is being paused or whether 
        /// the already paused game is being continued. With true indicating that the game has 
        /// been paused, and false indicating that the game is continuing.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public RequestGamePausedStatusChangeMessage(Guid moderatorId, bool gamePaused, string debugMessage) : base(moderatorId, MessageType.RequestGamePausedStatusChange, debugMessage)
        {
            GamePaused = gamePaused;
        }

        public override string ToString()
        {
            return "RequestGamePausedStatusChangeMessage [<container>: " + base.ToString() + ", GamePaused: " + GamePaused + "]";
        }
    }
}
