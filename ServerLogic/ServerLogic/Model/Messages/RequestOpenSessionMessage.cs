using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the Moderator-Client to the ServerLogic when the moderator 
    /// wants to connect to the ServerLogic. The password confirms that the moderator is allowed 
    /// to use the ServerLogic and the GUID of the moderator will be saved in the logs henceforth, 
    /// for further communication. In addition, the creation of an Online-Session is also requested 
    /// from the ServerLogic at the same time.
    /// </summary>
    public class RequestOpenSessionMessage : MessageContainer
    {
        public string Password { get; }

        /// <summary>
        /// Constructs a new RequestOpenSessionMessage with an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="password">The password, required by the ServerLogic, to establish a 
        /// connection with the ServerLogic.</param>
        public RequestOpenSessionMessage(Guid moderatorId, string password) : this(moderatorId, password, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new RequestOpenSessionMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="password">The password, required by the ServerLogic, to establish a 
        /// connection with the ServerLogic.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public RequestOpenSessionMessage(Guid moderatorId, string password, string debugMessage) : base(moderatorId, MessageType.RequestOpenSession, debugMessage)
        {
            Password = password;
        }

        public override string ToString()
        {
            return "RequestOpenSessionMessage [<container>: " + base.ToString() + ", Password: " + Password + "]";
        }
    }
}
