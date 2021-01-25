using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PAClient;

namespace ServerLogic.Control
{
    /// <summary>
    /// 
    /// </summary>
    public class PlayerAudienceClientAPI
    {
        private PABackend pABackend;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public void StartServer(int port)
        {
            pABackend = new PABackend(port);
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopServer()
        {
            pABackend.StopServer();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsServerActive()
        {
            return pABackend is not null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        public void StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            pABackend.SendPushMessage(sessionkey, prompt, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        public void StartNewSession(string sessionkey)
        {
            pABackend.StartNewSession(sessionkey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> EndSession(string sessionkey)
        {
            return pABackend.EndSession(sessionkey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResults(string sessionkey, string prompt)
        {
            return pABackend.GetVotingResult(sessionkey, prompt);
        }

        public PlayerAudienceClientAPI()
        {
            /* FALL THROUGH */
        }
    }
}
