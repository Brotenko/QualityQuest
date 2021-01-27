using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        private bool serverIsActive = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public void StartServer(int port)
        {
            if (serverIsActive == false)
            {
                serverIsActive = true;
                pABackend = new PABackend(port);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is already running and can't be started right now!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void StopServer()
        {
            if (serverIsActive)
            {
                serverIsActive = false;
                pABackend.StopServer();
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsServerActive()
        {
            return serverIsActive;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public void StartNewSession(string sessionkey)
        {
            if (Regex.IsMatch(sessionkey, @"\[A-Z0-9]{6}"))
            {
                pABackend.StartNewSession(sessionkey);
            }
            else
            {
                throw new InvalidOperationException(message: "The requested session is either already active, or the transferred sessionkey is invalid.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public void StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            if (IsSessionActive(sessionkey))
            {
                pABackend.SendPushMessage(sessionkey, prompt, options);
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        private bool IsSessionActive(string sessionkey)
        {
            return pABackend.GetSessionKeys().Contains(sessionkey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> EndSession(string sessionkey)
        {
            if (IsSessionActive(sessionkey))
            {
                return pABackend.EndSession(sessionkey);
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResults(string sessionkey, string prompt)
        {
            if (IsSessionActive(sessionkey))
            {
                return pABackend.GetVotingResult(sessionkey, prompt);
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        public PlayerAudienceClientAPI()
        {
            /* FALL THROUGH */
        }
    }
}
