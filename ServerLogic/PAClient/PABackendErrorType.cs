using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PAClient
{
    /// <summary>
    /// 
    /// </summary>
    public enum PABackendErrorType
    {
        /// <summary>
        /// 
        /// </summary>
        NoError = 0,
        /// <summary>
        /// 
        /// </summary>
        NullSessionkeyError = -1,
        /// <summary>
        /// 
        /// </summary>
        InvalidArgumentError = -2,
        /// <summary>
        /// 
        /// </summary>
        InvalidSessionkeyError = -3,
        /// <summary>
        /// 
        /// </summary>
        InvalidConnectionIdError = -4,
        /// <summary>
        /// 
        /// </summary>
        NullConnectionIdError = -5
    }
}
