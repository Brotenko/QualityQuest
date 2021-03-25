namespace PAClient
{
    /// <summary>
    /// All possible causes for a PABackendError, which can occur when the SignalR
    /// Hub communicates with the backend, since throwing actual errors, could be
    /// disastrous to the persistent communication.
    /// </summary>
    public enum PABackendErrorType
    {
        /// <summary>
        /// Is triggered when no error occured.
        /// </summary>
        NoError = 0,
        /// <summary>
        /// Is triggered when the given sessionkey is null.
        /// </summary>
        NullSessionkeyError = -1,
        /// <summary>
        /// Is triggered when a non-sessionkey argument is of invalid type.
        /// </summary>
        InvalidArgumentError = -2,
        /// <summary>
        /// Is triggered when the given sessionkey is of invalid type, but not null.
        /// </summary>
        InvalidSessionkeyError = -3,
        /// <summary>
        /// Is triggered when the given ConnectionId is invalid, but not null.
        /// </summary>
        InvalidConnectionIdError = -4,
        /// <summary>
        /// Is triggered when the given ConnectionId is null.
        /// </summary>
        NullConnectionIdError = -5
    }
}
