using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLogic.Model
{
    /// <summary>
    /// Basic identification of a network-error.
    /// </summary>
    public enum ErrorType
    {
        /// <summary>
        /// Is triggered when a <see cref="Messages.RequestOpenSessionMessage"/> contains the wrong password.
        /// </summary>
        WrongPassword,
        /// <summary>
        /// 
        /// </summary>
        UnknownGuid,
        /// <summary>
        /// 
        /// </summary>
        IllegalPauseAction,
        /// <summary>
        /// 
        /// </summary>
        SessionDoesNotExist,
        /// <summary>
        /// 
        /// </summary>
        NewModerator,
        /// <summary>
        /// 
        /// </summary>
        IllegalMessage
    }
}
