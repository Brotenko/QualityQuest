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
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public bool StartNewSession(string sessionkey)
        {
            if (serverIsActive)
            {
                if (Regex.IsMatch(sessionkey, @"[A-Z0-9]{6}"))
                {
                    try
                    {
                        pABackend.StartNewSession(sessionkey);
                        return true;
                    }
                    catch (ArgumentNullException e)
                    {
                        return false;
                    }
                    catch (ArgumentException e)
                    {
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
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async void StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            try
            {
                await pABackend.SendPushMessage(sessionkey, prompt, options);
            }
            catch (ArgumentNullException e)
            {
                
            }
            catch (ArgumentException e)
            {
                
            }
            catch (SessionNotFoundException e)
            {

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
        /// <param name="prompt"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResults(string sessionkey, KeyValuePair<Guid, string> prompt)
        {
            if (serverIsActive)
            {
                try
                {
                    return pABackend.GetVotingResult(sessionkey, prompt);
                }
                catch (ArgumentNullException e)
                {
                    return null;
                }
                catch (ArgumentException e)
                {
                    return null;
                }
                catch (SessionNotFoundException e)
                {
                    return null;
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
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> EndSession(string sessionkey)
        {
            if (serverIsActive)
            {
                try
                {
                    return pABackend.EndSession(sessionkey);
                }
                catch (ArgumentNullException e)
                {
                    return null;
                }
                catch (ArgumentException e)
                {
                    return null;
                }
                catch (SessionNotFoundException e)
                {
                    return null;
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
        public PlayerAudienceClientAPI()
        {
            /* FALL THROUGH */
        }
    }
}
