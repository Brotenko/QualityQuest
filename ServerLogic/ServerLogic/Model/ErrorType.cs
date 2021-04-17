namespace ServerLogic.Model
{
    /// <summary>
    /// All possible causes for an <see cref="Messages.ErrorMessage"/>, which can occur in the context of 
    /// communication between ServerLogic and Moderator-Client. These apply both when establishing the 
    /// connection and during the general course of the game.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Is triggered when a <see cref="Messages.RequestOpenSessionMessage"/> contains the wrong
        /// password.
        /// </summary>
        WrongPassword,
        /// <summary>
        /// Is triggered when a message with an unknown moderatorId is sent to the ServerLogic.
        /// </summary>
        UnknownGuid,
        /// <summary>
        /// Is triggered if one of the following cases applies:
        /// <list type="bullet">
        /// <item>A request to pause the game reaches the ServerLogic even though the game is already 
        /// paused.</item>
        /// <item>A request to continue the game reaches the ServerLogic even though the game has not been 
        /// paused previously.</item>
        /// </list>
        /// </summary>
        IllegalPauseAction,
        /// <summary>
        /// Is triggered when an attempt is made to interact with an Online-Session that does not exist.
        /// </summary>
        SessionDoesNotExist,
        /// <summary>
        /// Is triggered when an unknown message type is received, or when a message arrives at the 
        /// ServerLogic out of order. More precise details are to be specified in the errorMessageText.
        /// </summary>
        IllegalMessage,
        /// <summary>
        /// Is triggered if an already registered ModeratorGuid attempts to open a new session from a new connection.
        /// Addresses the unlikely event that two different ModeratorClients happen to generate the same Guid.
        /// </summary>
        GuidAlreadyExists
    }
}
