using System;


namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
    /// <see cref="Messages.RequestOpenSessionMessage"/> to provide the Moderator-Client with all 
    /// necessary data to allow the audience to join the Online-Session.
    /// </summary>
    public class SessionOpenedMessage : MessageContainer
    {
        public string SessionKey { get; }
        public Uri DirectURL { get; }

        /// <summary>
        /// Constructs a new SessionOpenedMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="sessionKey">A randomly generated Online-Session key, of size 6, required 
        /// by the audience to join the Online-Session, after connecting to the ServerLogic.</param>
        /// 
        /// <param name="directUrl">A direct URL that the audience can use to connect to the 
        /// ServerLogic via the PlayerAudience-Client should the QR code not be usable.</param>
        public SessionOpenedMessage(Guid moderatorId, string sessionKey, Uri directUrl) : base(moderatorId, MessageType.SessionOpened)
        {
            SessionKey = sessionKey;
            DirectURL = directUrl;
        }

        public override string ToString()
        {
            return "SessionOpenedMessage [<container>: " + base.ToString() + ", SessionKey: " + SessionKey + ", DirectURL: " + DirectURL + /*", QrCode: " + QrCode + */"]";
        }
    }
}
