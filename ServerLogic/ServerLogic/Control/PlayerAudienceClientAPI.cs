using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PAClient;
using ServerLogic.Properties;

namespace ServerLogic.Control
{
    /// <summary>
    /// Responsible for providing a communication interface between the
    /// PlayerAudience-Client, the MainServerLogic and the ServerShell.
    /// </summary>
    public class PlayerAudienceClientAPI
    {
        private PABackend _pABackend;

        /// <summary>
        /// A flag that determines if the server is currently running or not.
        /// </summary>
        public bool ServerIsActive { 
            get; private set; 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAudienceClientAPI"/> class.
        /// </summary>
        public PlayerAudienceClientAPI()
        {
            /* FALL THROUGH */
        }

        /// <summary>
        /// Starts the server and SignalR-Hub for the PlayerAudience-Clients.
        /// </summary>
        /// 
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="InvalidOperationException">The server is already in a running state.</exception>
        /// 
        /// <param name="port">The port of the PlayerAudience-Client host.</param>
        public void StartServer(int port)
        {
            if (port >= 1024 && port <= 65535)
            {
                if (ServerIsActive == false)
                {
                    ServerIsActive = true;
                    _pABackend = new PABackend(port, Settings.Default.DockerUrl);
                }
                else
                {
                    throw new InvalidOperationException(message: "The server is already running and can't be started right now!");
                }
            }
            else
            {
                throw new ArgumentException(message: "The given port is not in the valid range (1024-65535).");
            }
        }

        /// <summary>
        /// Starts a debug version of the PABackend without actually starting the server and SignalR hub
        /// for the PlayerAudience-Clients.
        /// </summary>
        /// 
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="InvalidOperationException">The server is already in a running state.</exception>
        /// 
        /// <param name="port">The port of the PlayerAudience-Client host.</param>
        public void DebugStartServer(int port)
        {
            if (port >= 1024 && port <= 65535)
            {
                if (ServerIsActive == false)
                {
                    ServerIsActive = true;
                    _pABackend = PABackend.DebugPABackend(port);
                }
                else
                {
                    throw new InvalidOperationException(message: "The server is already running and can't be started right now!");
                }
            }
            else
            {
                throw new ArgumentException(message: "The given port is not in the valid range (1024-65535).");
            }
        }

        /// <summary>
        /// Starts a new session for the PlayerAudience to connect to.
        /// </summary>
        /// 
        /// <param name="sessionkey">The SessionKey of the to be started session.</param>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="InvalidOperationException">The server is currently not in a running state.</exception>
        public void StartNewSession(string sessionkey)
        {
            if (ServerIsActive)
            {
                if (Regex.IsMatch(sessionkey, @"[A-Z0-9]{6}"))
                {
                    _pABackend.StartNewSession(sessionkey);
                }
                else
                {
                    throw new ArgumentException(message: "SessionKey needs to be 6 uppercase, alphanumerical characters.");
                }
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and thus can't start a new session!");
            }
        }

        /// <summary>
        /// Starts a new vote for a specific session, with the given prompt and voting options.
        /// </summary>
        /// 
        /// <param name="sessionkey">The session which begins a new vote.</param>
        /// 
        /// <param name="prompt">The prompt of the vote.</param>
        /// 
        /// <param name="options">The voting options of the prompt.</param>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given SessionKey is invalid or missformed.</exception>
        /// <exception cref="InvalidOperationException">The server is currently not in a running state.</exception>
        public async Task StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            if (ServerIsActive)
            {
                await _pABackend.StartNewVote(sessionkey, prompt, options);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and thus can't start a new vote!");
            }
        }

        /// <summary>
        /// Retrieves the voting results for a specfic session and prompt.
        /// </summary>
        /// 
        /// <param name="sessionkey">The session from which the result is requested.</param>
        /// 
        /// <param name="prompt">The prompt from which the results is requested.</param>
        ///
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given SessionKey is invalid or missformed.</exception>
        /// <exception cref="InvalidOperationException">The server is currently not in a running state.</exception>
        /// 
        /// <returns>The voting result of the given session and prompt.</returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResult(string sessionkey, KeyValuePair<Guid, string> prompt)
        {
            if (ServerIsActive)
            {
                return _pABackend.GetVotingResult(sessionkey, prompt);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and thus can't retrieve any results!");
            }
        }

        /// <summary>
        /// Terminates an active session, returns the statistics of the session, and removes every trace of it 
        /// from the internal data.
        /// </summary>
        /// 
        /// <param name="sessionkey">The to be terminated session.</param>
        ///
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given SessionKey is invalid or missformed.</exception>
        /// <exception cref="InvalidOperationException">The server is currently not in a running state.</exception>
        /// 
        /// <returns>The statistics of the terminated session.</returns>
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> EndSession(string sessionkey)
        {
            if (ServerIsActive)
            {
                return _pABackend.EndSession(sessionkey);
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and thus no session can be terminated!");
            }
        }

        /// <summary>
        /// Stops the server that hosts the PlayerAudience-Client.
        /// </summary>
        /// 
        /// <exception cref="InvalidOperationException">The server is currently not in a running state.</exception>
        public void StopServer()
        {
            if (ServerIsActive)
            {
                ServerIsActive = false;
                _pABackend.StopServer();
            }
            else
            {
                throw new InvalidOperationException(message: "The server is currently not running and can't be stopped right now!");
            }
        }
    }
}
