using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model
{
    /// <summary>
    /// Lists all message types. The structuring by comments is only for overview and has no semantic 
    /// meaning whatsoever. All messages are identified by the <see cref="MessageContainer"/>.
    /// </summary>
    public enum MessageType
    {
        // Initialization
        /// <summary>
        /// This message is sent from the Moderator-Client to the ServerLogic when the moderator wants 
        /// to connect to the ServerLogic. The password confirms that the moderator is allowed to use 
        /// the ServerLogic and the GUID of the moderator will be saved in the logs henceforth, for 
        /// further communication. In addition, the creation of an Online-Session is also requested from 
        /// the ServerLogic at the same time.
        /// </summary>
        RequestOpenSession,
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
        /// <see cref="Messages.RequestOpenSessionMessage"/> to provide the Moderator-Client with all 
        /// necessary data to allow the audience to join the Online-Session.
        /// </summary>
        SessionOpened,
        /// <summary>
        /// This message is sent from the ServerLogic to Moderator-Client every 3 seconds to inform the 
        /// Moderator-Client about the amount of PlayerAudience members that already connected to the server. 
        /// This message is only sent in the time-frame after the <see cref="Messages.SessionOpenedMessage"/>, 
        /// and before the <see cref="Messages.GameStartedMessage"/>, was received by the Moderator-Client.
        /// </summary>
        AudienceStatus,
        /// <summary>
        /// This message is sent from the Moderator-Client to the ServerLogic if there is currently no connection 
        /// to a ServerLogic. This message is sent to the ServerLogic at regular intervals until the ServerLogic 
        /// returns a response in form of a <see cref="Messages.ServerStatusMessage"/>. If a 
        /// <see cref="Messages.ServerStatusMessage"/> is received by the Moderator-Client at any given time, 
        /// the moderator is notified that a connection to the ServerLogic is possible, and at the same time, 
        /// <see cref="Messages.RequestServerStatusMessage"/> messages are stopped being sent to the ServerLogic.
        /// </summary>
        RequestServerStatus,
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
        /// <see cref="Messages.RequestServerStatusMessage"/> to confirm that the ServerLogic is available for 
        /// a connection.
        /// </summary>
        ServerStatus,
        /// <summary>
        /// This message is sent from the Moderator-Client to the ServerLogic to reestablish a lost connection. 
        /// For this purpose, the Moderator-Client's GUID is required for comparison with the previously saved 
        /// Moderator-Client GUID. This message shall only be sent when the Moderator-Client is still in-game, 
        /// otherwise a new Online-Session has to be opened through a <see cref="Messages.RequestOpenSessionMessage"/>.
        /// </summary>
        Reconnect,
        /// <summary>
        /// This message is sent from the the ServerLogic to the Moderator-Client to confirm that a lost 
        /// connection has been reestablished.
        /// </summary>
        ReconnectSuccessful,
        /// <summary>
        /// This message is sent from the Moderator-Client to the ServerLogic to request the start of the 
        /// game with the current Online-Session.
        /// </summary>
        RequestGameStart,
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
        /// <see cref="Messages.RequestGameStartMessage"/> to inform the Moderator-Client that the game has 
        /// started. This results in the Moderator-Client starting the game locally and the ServerLogic 
        /// awaiting further communication.
        /// </summary>
        GameStarted,
        // Voting
        /// <summary>
        /// This message is sent from the Moderator-Client to the ServerLogic to request the start of a voting 
        /// phase. For this purpose the Moderator-Client provides the ServerLogic with different options for the 
        /// audience to choose from. It also provides the ServerLogic with a time-limit on how long the 
        /// PlayerAudience-Clients may vote on the topic.
        /// </summary>
        RequestStartVoting,
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
        /// <see cref="Messages.RequestStartVotingMessage"/> to confirm the start of a voting phase with the 
        /// provided voting options.
        /// </summary>
        VotingStarted,
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
        /// <see cref="Messages.RequestStartVotingMessage"/>, after the voting time has expired. The winning 
        /// option and the statistical results of the vote are sent back to the Moderator-Client.
        /// </summary>
        VotingEnded,
        // Control messages
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in case of a disconnection initiated
        /// by the ServerLogic and explains the reason for the disconnection.
        /// </summary>
        Error,
        /// <summary>
        /// This message is sent from the Moderator-Client to the ServerLogic to switch the game between running 
        /// and being paused.
        /// </summary>
        RequestGamePausedStatusChange,
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
        /// <see cref="Messages.RequestGamePausedStatusChangeMessage"/>, to confirm that the game is now either 
        /// continuing or being paused.
        /// </summary>
        GamePausedStatus,
        // Postgame
        /// <summary>
        /// This message is sent from the Moderator-Client to the ServerLogic to tell the ServerLogic to close the 
        /// Online-Session and with that the connection to the PlayerAudience-Clients. It also commands the 
        /// ServerLogic to clear the logs.
        /// </summary>
        RequestCloseSession,
        /// <summary>
        /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
        /// <see cref="Messages.RequestCloseSessionMessage"/>, to confirm that the Online-Session has been 
        /// successfully closed and that the logs have been cleared completely. In addition to that, the statistics 
        /// of the Online-Session are returned to the Moderator-Client, which can be used to display every conducted 
        /// vote and which option got how many votes.
        /// </summary>
        SessionClosed
    }
}
