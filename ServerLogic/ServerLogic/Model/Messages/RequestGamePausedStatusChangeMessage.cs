using System;

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
        public RequestGamePausedStatusChangeMessage(Guid moderatorId, bool gamePaused) : base(moderatorId, MessageType.RequestGamePausedStatusChange)
        {
            GamePaused = gamePaused;
        }

        public override string ToString()
        {
            return "RequestGamePausedStatusChangeMessage [<container>: " + base.ToString() + ", GamePaused: " + GamePaused + "]";
        }
    }
}
