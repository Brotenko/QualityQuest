using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model
{
    /// <summary>
    /// 
    /// </summary>
    public enum MessageType
    {
        // Initialization
        /// <summary>
        /// 
        /// </summary>
        RequestOpenSession,
        /// <summary>
        /// 
        /// </summary>
        SessionOpened,
        /// <summary>
        /// 
        /// </summary>
        AudienceStatus,
        /// <summary>
        /// 
        /// </summary>
        RequestServerStatus,
        /// <summary>
        /// 
        /// </summary>
        ServerStatus,
        /// <summary>
        /// 
        /// </summary>
        Reconnect,
        /// <summary>
        /// 
        /// </summary>
        ReconnectSuccessful,
        /// <summary>
        /// 
        /// </summary>
        RequestGameStart,
        /// <summary>
        /// 
        /// </summary>
        GameStarted,
        // Voting
        /// <summary>
        /// 
        /// </summary>
        RequestStartVoting,
        /// <summary>
        /// 
        /// </summary>
        VotingStarted,
        /// <summary>
        /// 
        /// </summary>
        VotingEnded,
        // Control messages
        /// <summary>
        /// 
        /// </summary>
        Error,
        /// <summary>
        /// 
        /// </summary>
        RequestPauseGameStatusChange,
        /// <summary>
        /// 
        /// </summary>
        GamePauseStatus,
        // Postgame
        /// <summary>
        /// 
        /// </summary>
        RequestCloseSession,
        /// <summary>
        /// 
        /// </summary>
        SessionClosed
    }
}
