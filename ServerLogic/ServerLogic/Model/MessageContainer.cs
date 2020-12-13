using ServerLogic.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model
{
    /// <summary>
    /// Defines the container format for a message. All following fields can be found in every network 
    /// message, whereas a debugMessage is purely optional.
    /// </summary>
    public class MessageContainer
    {
        public Guid ModeratorID { get; private set; }
        public MessageType Type { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string DebugMessage { get; private set; }

        /// <summary>
        /// Constructs a new MessageContainer.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="type">Specifies the type of the message to be able to parse it accordingly.
        /// </param>
        /// 
        /// <param name="creationDate">The timestamp of the message.</param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public MessageContainer(Guid moderatorId, MessageType type, 
            DateTime creationDate, string debugMessage)
        {
            ModeratorID = moderatorId;
            Type = type;
            CreationDate = creationDate;
            DebugMessage = debugMessage;
        }

        /// <summary>
        /// Constructs a new MessageContainer with the current DateTime.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="type">Specifies the type of the message to be able to parse it accordingly.
        /// </param>
        /// 
        /// <param name="debugMessage">Can be used during development to transport additional data 
        /// between ServerLogic and Moderator-Client. This way, in case of a non parsable message, 
        /// or an error occurring, information can be carried to the Moderator-Client directly for 
        /// quick access, without the need to search through the logs.</param>
        public MessageContainer(Guid moderatorId, MessageType type,
            string debugMessage) : this(moderatorId, type, DateTime.Now, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Constructs a new MessageContainer with the current DateTime and an empty debugMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="type">Specifies the type of the message to be able to parse it accordingly.
        /// </param>
        public MessageContainer(Guid moderatorId, MessageType type) : this(moderatorId,
            type, DateTime.Now, "")
        {
            /* FALL THROUGH */
        }

        public override string ToString()
        {
            return "MessageContainer [ModeratorId: " + Convert.ToString(ModeratorID, CultureInfo.CurrentCulture) + 
                ", Type: " + Convert.ToString(Type, CultureInfo.CurrentCulture) + 
                ", Date: " + Convert.ToString(CreationDate, CultureInfo.CurrentCulture) + 
                ", Debug: " + DebugMessage + "]";
        }
    }
}