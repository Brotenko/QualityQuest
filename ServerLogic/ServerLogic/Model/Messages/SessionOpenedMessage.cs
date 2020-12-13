﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in response to a 
    /// <see cref="Messages.RequestOpenSessionMessage"/> to provide the Moderator-Client with all 
    /// necessary data to allow the audience to join the Online-Session.
    /// </summary>
    public class SessionOpenedMessage : MessageContainer
    {
        public string SessionKey { get; private set; }
        public Uri DirectURL { get; private set; }
        public Bitmap QrCode { get; private set; }

        /// <summary>
        /// Constructs a new SessionOpenedMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="sessionKey">A randomly generated Online-Session key, of size 6, required 
        /// by the audience to join the Online-Session, after connecting to the ServerLogic.</param>
        /// 
        /// <param name="directURL">A direct URL that the audience can use to connect to the 
        /// ServerLogic via the PlayerAudience-Client should the QR code not be usable.</param>
        /// 
        /// <param name="qrCode">A QR-code automatically generated by the ServerLogic which can be 
        /// scanned by the audience to connect to the ServerLogic.</param>
        public SessionOpenedMessage(Guid moderatorId, string sessionKey, Uri directURL, Bitmap qrCode) : this(moderatorId, sessionKey, directURL, qrCode, "")
        {
            /* FALL THROUGH */
        }

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
        /// <param name="directURL">A direct URL that the audience can use to connect to the 
        /// ServerLogic via the PlayerAudience-Client should the QR code not be usable.</param>
        /// 
        /// <param name="qrCode">A QR-code automatically generated by the ServerLogic which can be 
        /// scanned by the audience to connect to the ServerLogic.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public SessionOpenedMessage(Guid moderatorId, string sessionKey, Uri directURL, Bitmap qrCode, string debugMessage) : base(moderatorId, MessageType.SessionOpened, debugMessage)
        {
            SessionKey = sessionKey;
            DirectURL = directURL;
            QrCode = qrCode;
        }

        public override string ToString()
        {
            return "SessionOpenedMessage [<container>: " + base.ToString() + ", SessionKey: " + SessionKey + ", DirectURL: " + DirectURL + ", QrCode: " + QrCode + "]";
        }
    }
}