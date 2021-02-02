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

        public bool serverIsActive { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public PlayerAudienceClientAPI()
        {
            /* FALL THROUGH */
        }

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
        /// <param name="port"></param>
        public void DebugStartServer(int port)
        {
            if (serverIsActive == false)
            {
                serverIsActive = true;
                pABackend = PABackend.DebugPABackend(port);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is already running and can't be started right now!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public void StartNewSession(string sessionkey)
        {
            if (serverIsActive)
            {
                if (Regex.IsMatch(sessionkey, @"[A-Z0-9]{6}"))
                {
                    pABackend.StartNewSession(sessionkey);
                }
                else
                {
                    throw new ArgumentException(message: "Sessionkey needs to be 6 uppercase, alphanumerical characters.");
                }
            }
            else
            {
                throw new InvalidOperationException(message: "The server is already running and can't be started right now!");
            }
        }

        /// <summary>
        /// 
        /// 
        /// </summary>
        /// 
        /// <param name="sessionkey"></param>
        /// 
        /// <param name="prompt"></param>
        /// 
        /// <param name="options"></param>
        /// 
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SessionNotFoundException"></exception>
        /// 
        /// <returns></returns>
        public async Task StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            if (serverIsActive)
            {
                await pABackend.SendPushMessage(sessionkey, prompt, options);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// 
        /// <param name="sessionkey"></param>
        /// 
        /// <param name="prompt"></param>
        ///
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SessionNotFoundException"></exception>
        /// 
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResult(string sessionkey, KeyValuePair<Guid, string> prompt)
        {
            if (serverIsActive)
            {
                return pABackend.GetVotingResult(sessionkey, prompt);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        ///
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="SessionNotFoundException"></exception>
        /// 
        /// <param name="sessionkey"></param>
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> EndSession(string sessionkey)
        {
            if (serverIsActive)
            {
                return pABackend.EndSession(sessionkey);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
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
    }
}
