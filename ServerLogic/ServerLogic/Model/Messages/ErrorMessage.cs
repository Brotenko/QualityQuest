using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// This message is sent from the ServerLogic to the Moderator-Client in case of a 
    /// disconnection initiated by the ServerLogic and explains the reason for the 
    /// disconnection.
    /// </summary>
    public class ErrorMessage : MessageContainer
    {
        public ErrorType ErrorMessageType { get; }
        public string ErrorMessageText { get; }

        /// <summary>
        /// Constructs a new ErrorMessage.
        /// </summary>
        /// 
        /// <param name="moderatorId">The individual identifier assigned to the Moderator-Client. 
        /// Only the Moderator-Client sends this id to the ServerLogic to identify itself. The 
        /// ServerLogic leaves this field empty.</param>
        /// 
        /// <param name="errorMessageType">Specifies the reason for the occurred error.</param>
        /// 
        /// <param name="errorMessageText">Optional, more detailed description of the occurred 
        /// error.</param>
        public ErrorMessage(Guid moderatorId, ErrorType errorMessageType, string errorMessageText) : base(moderatorId, MessageType.Error)
        {
            ErrorMessageType = errorMessageType;
            ErrorMessageText = errorMessageText;
        }

        public override string ToString()
        {
            return "ErrorMessage [<container>: " + base.ToString() + ", ErrorMessageType: " + ErrorMessageType + ", ErrorMessageText: " + ErrorMessageText + "]";
        }
    }
}
