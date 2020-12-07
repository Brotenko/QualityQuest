using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model.Messages
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorMessage : MessageContainer
    {
        public ErrorType ErrorMessageType { get; private set; }
        public string ErrorMessageText { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="errorMessageType"></param>
        /// <param name="errorMessageText"></param>
        public ErrorMessage(Guid moderatorId, ErrorType errorMessageType, string errorMessageText) : this(moderatorId, errorMessageType, errorMessageText, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="errorMessageType"></param>
        /// <param name="errorMessageText"></param>
        /// <param name="debugMessage"></param>
        public ErrorMessage(Guid moderatorId, ErrorType errorMessageType, string errorMessageText, string debugMessage) : base(moderatorId, MessageType.Error, debugMessage)
        {
            ErrorMessageType = errorMessageType;
            ErrorMessageText = errorMessageText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "ErrorMessage [<container>: " + base.ToString() + ", ErrorMessageType: " + ErrorMessageType + ", ErrorMessageText: " + ErrorMessageText + "]";
        }
    }
}
