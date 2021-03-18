﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response 
    /// to a <see cref="Messages.RequestGamePausedStatusChangeMessage"/>, to confirm that the 
    /// game is now either continuing or being paused.
    /// </summary>
    public class GamePausedStatusMessage : MessageContainer
    {
        public bool GamePaused { get; }

        /// <summary>
        /// Constructs a new GamePauseStatusMessage.
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
        public GamePausedStatusMessage(Guid moderatorId, bool gamePaused) : base(moderatorId, MessageType.GamePausedStatus)
        {
            GamePaused = gamePaused;
        }

        public override string ToString()
        {
            return "GamePausedStatusMessage [<container>: " + base.ToString() + ", GamePaused: " + GamePaused + "]";
        }
    }
}
