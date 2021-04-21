using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using PAClient.Hubs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace PAClient
{
    /// <summary>
    /// Backend for the PlayerAudience-Client
    /// </summary>
    public class PABackend
    {
        private IHost host;
        private static IHubContext<ServerHub> _hubContext;
        private Thread _serverThread;
        private static bool isDebug;

        /// <summary>
        /// A complex datatype acting as a database for the PABackend, holding sessions,
        /// prompts, voting options and respective votes.
        /// </summary>
        public static VotingResults PAVotingResults
        {
            get; private set;
        }

        /// <summary>
        /// A dictionary of lists of all SignalR Hub-Groups, ordered by sessionkey.
        /// </summary>
        public static Dictionary<string, List<string>> ConnectionList
        {
            get; private set;
        }

        /// <summary>
        /// The currently active prompt of every active session.
        /// </summary>
        private static Dictionary<string, KeyValuePair<Guid, string>> CurrentPrompt
        {
            get; set;
        }

        /// <summary>
        /// The port of the PlayerAudience-Client host.
        /// </summary>
        private int Port
        {
            get; set;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PABackend"/> class.
        /// </summary>
        /// 
        /// <param name="port">The port of the PlayerAudience-Client host.</param>
        public PABackend(int port)
        {
            Port = port;
            PAVotingResults = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            ConnectionList = new Dictionary<string, List<string>>();
            CurrentPrompt = new Dictionary<string, KeyValuePair<Guid, string>>();

            if (!isDebug)
            {
                _serverThread = new Thread(this.StartServer);
                _serverThread.Start();
            }
        }

        /// <summary>
        /// Starts a debug version of the PABackend without actually starting the server and SignalR hub
        /// for the PlayerAudience-Clients.
        /// </summary>
        /// 
        /// <param name="port">The port of the PlayerAudience-Client host.</param>
        /// 
        /// <returns>A new instance of the <see cref="PABackend"/> class.</returns>
        public static PABackend DebugPABackend(int port)
        {
            isDebug = true;
            return new PABackend(port);
        }

        /// <summary>
        /// Retrieves all sessionkeys that correspond to currently active sessions.
        /// </summary>
        /// 
        /// <returns>all sessionkeys that correspond to currently active sessions.</returns>
        public string[] GetSessionKeys()
        {
            return PAVotingResults.GetSessionKeys();
        }

        /// <summary>
        /// Starts a new session for the PlayerAudience to connect to.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// 
        /// <param name="sessionkey">The sessionkey of the to be started session.</param>
        public void StartNewSession(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (Regex.IsMatch(sessionkey, @"[A-Z0-9]{6}"))
            {
                if (!IsSessionActive(sessionkey))
                {
                    AddNewSession(sessionkey);
                }
                else
                {
                    throw new ArgumentException("A session with that key is already registered.");
                }
            }
            else
            {
                throw new ArgumentException("The sessionkey has to be six alphanumerical uppercase characters.");
            }
        }

        /// <summary>
        /// A debug method for testing purposes. Creates an HTML string that holds information
        /// regarding the prompt and voting options of the to be created poll. Also adapts to
        /// the amount of provided options, so that the output can be directly injected into
        /// the website.
        /// </summary>
        /// 
        /// <param name="prompt">The prompt of the debug vote.</param>
        /// 
        /// <param name="options">The voting options of the debug prompt.</param>
        /// 
        /// <returns>An HTML string that holds information regarding the prompt and voting 
        /// options of the to be created poll.</returns>
        public string DebugCreatePageContent(KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            return CreatePageContent(prompt, options);
        }

        /// <summary>
        /// Starts a new vote for a specific session, with the given prompt and voting options.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The session which begins a new vote.</param>
        /// 
        /// <param name="prompt">>The prompt of the vote.</param>
        /// 
        /// <param name="options">The voting options of the prompt.</param>
        public async Task StartNewVote(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }
            if (prompt.Value == null)
            {
                throw new ArgumentNullException("The prompt's description can not be null.");
            }
            if (options == null)
            {
                throw new ArgumentNullException("The options can not be null.");
            }
            foreach (KeyValuePair<Guid, string> pair in options)
            {
                if (pair.Value == null)
                {
                    throw new ArgumentNullException("The option's description can not be null.");
                }
            }

            if (IsSessionActive(sessionkey))
            {
                if (!PAVotingResults.GetPromptsBySession(sessionkey).Contains(prompt))
                {
                    string pageContent = CreatePageContent(prompt, options);
                    PAVotingResults.AddNewPoll(sessionkey, prompt, options);
                    CurrentPrompt[sessionkey] = prompt;

                    // Can't test for hubContext/Host related stuff, since the test framework
                    // can not start/host the server to test these (Important: This might be false).
                    // It would go beyond any reason to test these, even if it is somehow possible.
                    if (!isDebug)
                    {
                        await _hubContext.Clients.Group(sessionkey).SendAsync("NewPrompt", pageContent);
                    }
                }
                else
                {
                    throw new ArgumentException("The transmitted prompt has already been sent to this session.");
                }
            }
            else
            {
                throw new SessionNotFoundException(message: "The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// Allows PlayerAudience members to vote on the current prompt through a SignalR-Hub.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The session which the PlayerAudience member, that currently votes,
        /// belongs to.</param>
        /// 
        /// <param name="option">The voting option the PlayerAudience member voted for.</param>
        /// 
        /// <returns>If the vote was successful or if some kind of error occurred.</returns>
        public static int CountNewVote(string sessionkey, Guid option)
        {
            try
            {
                if (sessionkey == null)
                {
                    throw new ArgumentNullException("The sessionkey can not be null.");
                }

                if (IsSessionActive(sessionkey))
                {
                    Guid clientPrompt = CurrentPrompt.GetValueOrDefault(sessionkey).Key;

                    PAVotingResults.AddVote(sessionkey, clientPrompt, option);
                    return (int) PABackendErrorType.NoError;
                }
                else
                {
                    throw new SessionNotFoundException("The requested session is either inactive or invalid!");
                }
            }
            catch (ArgumentNullException)
            {
                return (int) PABackendErrorType.NullSessionkeyError;
            }
            catch (ArgumentException)
            {
                return (int) PABackendErrorType.InvalidArgumentError;
            }
            catch (SessionNotFoundException)
            {
                return (int) PABackendErrorType.InvalidSessionkeyError;
            }
        }

        /// <summary>
        /// Terminates an active session, returns the statistics of the session, and removes every trace of it 
        /// from the internal data.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The to be terminated session.</param>
        /// 
        /// <returns>The statistics of the terminated session.</returns>
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> EndSession(string sessionkey)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> temp = PAVotingResults.GetStatistics(sessionkey);
                RemoveSession(sessionkey);
                return temp;
            }
            else
            {
                throw new SessionNotFoundException("The transmitted sessionkey does not belong to an active session.");
            }
        }

        /// <summary>
        /// Adds a new connection to the list of currently connected PlayerAudience-Clients. The 
        /// connectionId is provided by the SignalR-Hub calling this method.
        /// </summary>
        /// 
        /// <param name="sessionkey">The session which the PlayerAudience-Client belongs to.</param>
        /// 
        /// <param name="connectionId">The unique connectionId of the PlayerAudience-Client.</param>
        /// 
        /// <returns>If the connection was established successfully or if some kind of error 
        /// occurred.</returns>
        public static int AddConnection(string sessionkey, string connectionId)
        {
            if (sessionkey == null)
            {
                /* LOG ERROR HERE */
                return (int) PABackendErrorType.NullSessionkeyError;
            }
            if (connectionId == null)
            {
                /* LOG ERROR HERE */
                return (int) PABackendErrorType.NullConnectionIdError;
            }

            if (IsSessionActive(sessionkey))
            {
                if (Regex.IsMatch(connectionId, @"[a-zA-Z0-9\-_]{22}"))
                {
                    ConnectionList.GetValueOrDefault(sessionkey).Add(connectionId);
                    return (int) PABackendErrorType.NoError;
                }
                else
                {
                    /* LOG ERROR HERE */
                    return (int) PABackendErrorType.InvalidConnectionIdError;
                }
            }
            else
            {
                /* LOG ERROR HERE */
                return (int) PABackendErrorType.InvalidSessionkeyError;
            }
        }

        /// <summary>
        /// Removes a connectionId from every list of currently connected PlayerAudience-Clients. The 
        /// connectionId is provided by the SignalR-Hub calling this method.
        /// </summary>
        /// 
        /// <param name="connectionId">The unique connectionId of the PlayerAudience-Client.</param>
        /// 
        /// <returns>If the connection was terminated successfully or if some kind of error 
        /// occurred.</returns>
        public static int RemoveConnection(string connectionId)
        {
            if (connectionId == null)
            {
                /* LOG ERROR HERE */
                return (int) PABackendErrorType.NullConnectionIdError;
            }

            foreach (KeyValuePair<string, List<string>> entry in ConnectionList)
            {
                if (entry.Value.Contains(connectionId))
                {
                    entry.Value.Remove(connectionId);
                    return (int) PABackendErrorType.NoError;
                }
            }
            return (int) PABackendErrorType.InvalidConnectionIdError;
        }


        /// <summary>
        /// Retrieves the voting results for a specific session and prompt.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// <exception cref="ArgumentException">One or more of the arguments provided is not valid.</exception>
        /// <exception cref="SessionNotFoundException">The given sessionkey is invalid or missformed.</exception>
        /// 
        /// <param name="sessionkey">The session from which the result is requested.</param>
        /// 
        /// <param name="prompt">The prompt from which the results is requested.</param>
        /// 
        /// <returns>The voting result of the given session and prompt.</returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResult(string sessionkey, KeyValuePair<Guid, string> prompt)
        {
            if (sessionkey == null)
            {
                throw new ArgumentNullException("The sessionkey can not be null.");
            }
            if (prompt.Value == null)
            {
                throw new ArgumentNullException("The prompt's description can not be null.");
            }

            if (IsSessionActive(sessionkey))
            {
                if (PAVotingResults.GetPromptsBySession(sessionkey).Contains(prompt)) {
                    SendPushClear(sessionkey);
                    return PAVotingResults.GetOptionsVotesPairsByPrompt(sessionkey, prompt.Key);
                }
                else
                {
                    throw new ArgumentException("The transmitted prompt is not part of this session.");
                }
            }
            else
            {
                throw new SessionNotFoundException("The requested session is either inactive or invalid!");
            }
        }

        /// <summary>
        /// Stops the server that hosts the PlayerAudience-Client.
        /// </summary>
        public void StopServer()
        {
            if (!isDebug)
            {
                host.StopAsync();
                _serverThread.Join();
            }
        }

        /// <summary>
        /// The Main method of the PABackend.
        /// </summary>
        /// 
        /// <param name="args">Command-line parameters.</param>
        /// 
        /// <exception cref="ArgumentException"></exception>
        public static void Main(string[] args)
        {
            if (!isDebug)
            {
                if (args.Length == 0)
                {
                    CreateHostBuilder(7777).Build().Run();
                }

                CreateHostBuilder(Convert.ToInt32(args[0], CultureInfo.CurrentCulture)).Build().Run();
            }
        }

        /// <summary>
        /// Checks if a given session is active.
        /// </summary>
        /// 
        /// <param name="sessionkey">The sessionkey of the to be checked session.</param>
        /// 
        /// <returns>If the session is currently active.</returns>
        private static bool IsSessionActive(string sessionkey)
        {
            return PAVotingResults.GetSessionKeys().Contains(sessionkey);
        }

        /// <summary>
        /// Adds a new session to the PABackend and sets up the fields of the class to
        /// be used for communication purposes between SignalR-Hub, PABackend and the
        /// MainServerLogic.
        /// </summary>
        /// 
        /// <param name="sessionkey">The sessionkey of the to be added session.</param>
        private static void AddNewSession(string sessionkey)
        {
            PAVotingResults.AddSessionKey(sessionkey);
            ConnectionList.Add(sessionkey, new List<string>());
            CurrentPrompt.Add(sessionkey, new KeyValuePair<Guid, string>());
        }

        /// <summary>
        /// Creates an HTML string that holds information
        /// regarding the prompt and voting options of the to be created poll. Also adapts to
        /// the amount of provided options, so that the output can be directly injected into
        /// the website.
        /// </summary>
        /// 
        /// <exception cref="ArgumentNullException">Any of the given parameters contains a null-value.</exception>
        /// 
        /// <param name="prompt">The prompt of the debug vote.</param>
        /// 
        /// <param name="options">The voting options of the debug prompt.</param>
        /// 
        /// <returns>An HTML string that holds information regarding the prompt and voting 
        /// options of the to be created poll.</returns>
        private string CreatePageContent(KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            if (prompt.Value == null)
            {
                throw new ArgumentNullException("The prompt's description can not be null.");
            }
            if (options == null)
            {
                throw new ArgumentNullException("The options can not be null.");
            }
            foreach (KeyValuePair<Guid, string> pair in options)
            {
                if (pair.Value == null)
                {
                    throw new ArgumentNullException("The option's description can not be null.");
                }
            }

            string ret = "";

            string promptString = prompt.Value;
            string[] optionsStrings = options.Select(kvp => kvp.Value).ToArray();
            Guid[] optionsGuids = options.Select(kvp => kvp.Key).ToArray();

            switch (options.Length)
            {
            case 2:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        promptString +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" name=\"" + optionsGuids[0] + "\" class=\"input-button voting-container-2items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" name=\"" + optionsGuids[1] + "\" class=\"input-button voting-container-2items-2\" value=\"" + optionsStrings[1] + "\" />" +
                      "</div>";
                break;
            case 3:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        promptString +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" name=\"" + optionsGuids[0] + "\" class=\"input-button voting-container-3items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" name=\"" + optionsGuids[1] + "\" class=\"input-button voting-container-3items-2\" value=\"" + optionsStrings[1] + "\" />" +
                        "<input type=\"button\" id=\"choice-3\" name=\"" + optionsGuids[2] + "\" class=\"input-button voting-container-3items-3\" value=\"" + optionsStrings[2] + "\" />" +
                      "</div>";
                break;
            case 4:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        promptString +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" name=\"" + optionsGuids[0] + "\" class=\"input-button voting-container-4items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" name=\"" + optionsGuids[1] + "\" class=\"input-button voting-container-4items-2\" value=\"" + optionsStrings[1] + "\" />" +
                        "<input type=\"button\" id=\"choice-3\" name=\"" + optionsGuids[2] + "\" class=\"input-button voting-container-4items-3\" value=\"" + optionsStrings[2] + "\" />" +
                        "<input type=\"button\" id=\"choice-4\" name=\"" + optionsGuids[3] + "\" class=\"input-button voting-container-4items-4\" value=\"" + optionsStrings[3] + "\" />" +
                      "</div>";
                break;
            default:
                break;
            }

            return ret;
        }

        /// <summary>
        /// Removes a session from the PABackend.
        /// </summary>
        /// 
        /// <param name="sessionkey">The sessionkey of the to be removed session.</param>
        private static void RemoveSession(string sessionkey)
        {
            PAVotingResults.RemoveSession(sessionkey);
            ConnectionList.Remove(sessionkey);
            CurrentPrompt.Remove(sessionkey);
        }

        /// <summary>
        /// Sends a prompt to the SignalR-Hub to clear the voting-page of a specific
        /// Hub-Group.
        /// </summary>
        /// 
        /// <param name="sessionkey">The sessionkey of the Hub-Group.</param>
        private async void SendPushClear(string sessionkey)
        {
            if (!isDebug)
            {
                await _hubContext.Clients.Group(sessionkey).SendAsync("ClearPrompt");
            }
        }

        /// <summary>
        /// Starts the server and SignalR-Hub for the PlayerAudience-Clients.
        /// </summary>
        private void StartServer()
        {
            if (!isDebug)
            {
                host = CreateHostBuilder(Port).Build();
                _hubContext = (IHubContext<ServerHub>)host.Services.GetService(typeof(IHubContext<ServerHub>));
                host.Run();
            }
        }

        /// <summary>
        /// An autogenerated method responsible for starting up the Server-Host that
        /// hosts the PlayerAudience-Client website and sets the URL.
        /// </summary>
        /// 
        /// <param name="port">The port of the PlayerAudience-Client host.</param>
        /// 
        /// <returns>The program initialization.</returns>
        private static IHostBuilder CreateHostBuilder(int port) =>
            Host.CreateDefaultBuilder()
                .ConfigureLogging(logging =>
                //sets the built in Logger to Warning-Level, to reduce Log-Spam in Server-Shell 
                    logging.AddFilter("System", LogLevel.Warning)
                        .AddFilter("Microsoft", LogLevel.Warning))
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseUrls("http://0.0.0.0:" + port + "/");
                    webBuilder.UseUrls("http://127.0.0.1:" + port + "/");
                });
    }
}