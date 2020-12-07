using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class SessionOpenedMessage : MessageContainer
    {
        public string SessionKey { get; private set; }
        public Uri DirectURL { get; private set; }
        public Bitmap QrCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="sessionKey"></param>
        /// <param name="directURL"></param>
        /// <param name="qrCode"></param>
        public SessionOpenedMessage(Guid moderatorId, string sessionKey, Uri directURL, Bitmap qrCode) : this(moderatorId, sessionKey, directURL, qrCode, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="sessionKey"></param>
        /// <param name="directURL"></param>
        /// <param name="qrCode"></param>
        /// <param name="debugMessage"></param>
        public SessionOpenedMessage(Guid moderatorId, string sessionKey, Uri directURL, Bitmap qrCode, string debugMessage) : base(moderatorId, MessageType.SessionOpened, debugMessage)
        {
            SessionKey = sessionKey;
            DirectURL = directURL;
            QrCode = qrCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SessionOpenedMessage [<container>: " + base.ToString() + ", SessionKey: " + SessionKey + ", DirectURL: " + DirectURL + ", QrCode: " + QrCode + "]";
        }
    }
}
