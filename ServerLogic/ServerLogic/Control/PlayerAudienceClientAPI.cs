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
        private bool serverIsActive;

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
        /// <returns>If a new session has been started successfully.</returns>
        public bool StartNewSession(string sessionkey)
        {
            if (serverIsActive)
            {
                if (Regex.IsMatch(sessionkey, @"[A-Z0-9]{6}"))
                {
                    return pABackend.StartNewSession(sessionkey);
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public bool StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            if (serverIsActive)
            {
                if (IsSessionActive(sessionkey))
                {
                    try
                    {
                        pABackend.SendPushMessage(sessionkey, prompt, options);
                        return true;
                    }
                    catch (InvalidOperationException e)
                    {
                        /* LOG ERROR HERE */
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
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
            if (serverIsActive)
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
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResults(string sessionkey, KeyValuePair<Guid, string> prompt)
        {
            if (serverIsActive)
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
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stoppped right now!");
            }
        }

        public PlayerAudienceClientAPI()
        {
            /* FALL THROUGH */
        }
    }
}
