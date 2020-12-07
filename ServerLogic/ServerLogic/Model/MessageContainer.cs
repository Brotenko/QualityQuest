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
    /// 
    /// </summary>
    public class MessageContainer
    {
        public Guid ModeratorID { get; private set; }
        public MessageType Type { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string DebugMessage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="type"></param>
        /// <param name="creationDate"></param>
        /// <param name="debugMessage"></param>
        public MessageContainer(Guid moderatorId, MessageType type, 
            DateTime creationDate, string debugMessage)
        {
            ModeratorID = moderatorId;
            Type = type;
            CreationDate = creationDate;
            DebugMessage = debugMessage;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="type"></param>
        /// <param name="debugMessage"></param>
        public MessageContainer(Guid moderatorId, MessageType type,
            string debugMessage) : this(moderatorId, type, DateTime.Now, debugMessage)
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="type"></param>
        public MessageContainer(Guid moderatorId, MessageType type) : this(moderatorId,
            type, DateTime.Now, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "MessageContainer [ModeratorId: " + Convert.ToString(ModeratorID, CultureInfo.CurrentCulture) + 
                ", Type: " + Convert.ToString(Type, CultureInfo.CurrentCulture) + 
                ", Date: " + Convert.ToString(CreationDate, CultureInfo.CurrentCulture) + 
                ", Debug: " + DebugMessage + "]";
        }
    }
}