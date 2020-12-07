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
    public class AudienceStatusMessage : MessageContainer
    {
        public int AudienceCount { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="audienceCount"></param>
        public AudienceStatusMessage(Guid moderatorId, int audienceCount) : this(moderatorId, audienceCount, "")
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="moderatorId"></param>
        /// <param name="audienceCount"></param>
        /// <param name="debugMessage"></param>
        public AudienceStatusMessage(Guid moderatorId, int audienceCount, string debugMessage) : base(moderatorId, MessageType.AudienceStatus, debugMessage)
        {
            AudienceCount = audienceCount;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "AudienceStatusMessage [<container>: " + base.ToString() + ", AudienceCount: " + AudienceCount + "]";
        }
    }
}
