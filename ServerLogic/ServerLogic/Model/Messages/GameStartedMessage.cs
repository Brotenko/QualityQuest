using System;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
    /// <see cref="Messages.RequestGameStartMessage"/> to inform the Moderator-Client that the 
    /// game has started. This results in the Moderator-Client starting the game locally and 
    /// the ServerLogic awaiting further communication.
    /// </summary>
    public class GameStartedMessage : MessageContainer
    {

        /// <summary>
        /// Constructs a new GameStartedMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        public GameStartedMessage(Guid moderatorId) : base(moderatorId, MessageType.GameStarted)
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "GameStartedMessage [<container>: " + base.ToString() + "]";
        }
    }
}
