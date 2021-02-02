using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PAClient.Hubs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PAClient
{
    /// <summary>
    /// 
    /// </summary>
    public class PABackend
    {
        private IHost host;
        private static IHubContext<ServerHub> _hubContext;
        private Thread _serverThread;
        private static bool isDebug;

        // A list of all voting results, sorted by the GUID of the "voting prompt/questions" 
        public static VotingResults PAVotingResults
        {
            get; private set;
        }

        // A list of all groups, each having their own list of connected clients
        public static Dictionary<string, List<string>> ConnectionList
        {
            get; private set;
        }

        // Current prompt depending on the session
        //                 sessionkey             prompt
        private static Dictionary<string, KeyValuePair<Guid, string>> CurrentPrompt
        {
            get; set;
        }

        private int Port
        {
            get; set;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public PABackend(int port)
        {
            Port = port;
            PAVotingResults = new VotingResults(new Dictionary<string, Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>>>());
            ConnectionList = new Dictionary<string, List<string>>();
            CurrentPrompt = new Dictionary<string, KeyValuePair<Guid, string>>();

            _serverThread = new Thread(this.StartServer);
            _serverThread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        public static PABackend DebugPABackend(int port)
        {
            isDebug = true;
            return new PABackend(port);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string[] GetSessionKeys()
        {
            return PAVotingResults.GetSessionKeys();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
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
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public string DebugCreatePageContent(KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            return CreatePageContent(prompt, options);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        public async Task SendPushMessage(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
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
                    // can not (at least to my knowledge) start/host the server to test these.
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
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="option"></param>
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
            catch (ArgumentNullException e)
            {
                /* LOG ERROR HERE */
                return (int) PABackendErrorType.NullSessionkeyError;
            }
            catch (ArgumentException e)
            {
                /* LOG ERROR HERE */
                return (int) PABackendErrorType.InvalidArgumentError;
            }
            catch (SessionNotFoundException e)
            {
                /* LOG ERROR HERE */
                return (int) PABackendErrorType.InvalidSessionkeyError;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <returns></returns>
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
                throw new ArgumentException("The transmitted sessionkey does not belong to an active session.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="connectionId"></param>
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
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
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
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
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
        /// 
        /// </summary>
        public void StopServer()
        {
            host.StopAsync();
            _serverThread.Join();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                CreateHostBuilder(7777).Build().Run();
            }

            CreateHostBuilder(Convert.ToInt32(args[0], CultureInfo.CurrentCulture)).Build().Run();
        }

        private static bool IsSessionActive(string sessionkey)
        {
            return PAVotingResults.GetSessionKeys().Contains(sessionkey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        private static void AddNewSession(string sessionkey)
        {
            PAVotingResults.AddSessionKey(sessionkey);
            ConnectionList.Add(sessionkey, new List<string>());
            CurrentPrompt.Add(sessionkey, new KeyValuePair<Guid, string>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
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
            //string[] optionsStrings = options.SelectMany(keyValuePair => keyValuePair.Values.Select(key => key)).Distinct().ToArray();
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
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        private static void RemoveSession(string sessionkey)
        {
            PAVotingResults.RemoveSession(sessionkey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="group"></param>
        private async void SendPushClear(string sessionkey)
        {
            await _hubContext.Clients.Group(sessionkey).SendAsync("ClearPrompt");
        }

        /// <summary>
        /// 
        /// </summary>
        private void StartServer()
        {
            host = CreateHostBuilder(Port).Build();
            _hubContext = (IHubContext<ServerHub>)host.Services.GetService(typeof(IHubContext<ServerHub>));
            host.Run();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="port"></param>
        /// <returns></returns>
        private static IHostBuilder CreateHostBuilder(int port) =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://localhost:" + port + "/");
                });
    }
}