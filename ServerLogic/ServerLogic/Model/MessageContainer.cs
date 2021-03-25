using System;
using System.Globalization;

namespace ServerLogic.Model
{
    /// <summary>
    /// Defines the container format for a message. All following fields can be found in every network 
    /// message.
    /// </summary>
    public class MessageContainer
    {
        public Guid ModeratorID { get; }
        public MessageType Type { get; }
        public DateTime CreationDate { get; }

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
        public MessageContainer(Guid moderatorId, MessageType type)
        {
            ModeratorID = moderatorId;
            Type = type;
            CreationDate = DateTime.Now;
        }

        public override string ToString()
        {
            return "MessageContainer [ModeratorId: " + Convert.ToString(ModeratorID, CultureInfo.CurrentCulture) + 
                ", Type: " + Convert.ToString(Type, CultureInfo.CurrentCulture) + 
                ", Date: " + CreationDate.ToString("yyyy.MM.dd HH:mm:ss") + 
                "]";
        }
    }
}
