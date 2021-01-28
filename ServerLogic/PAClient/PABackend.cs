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
using System.Threading;
using System.Threading.Tasks;

/*
 * 1) Add more security levels to methods with Exceptions and return values and shit
 * 2) Add tests and comments
 * 3) Error Pop-Up when connecting to non-existent session 
 * 4) Update documentation
 */


namespace PAClient
{
    /// <summary>
    /// 
    /// </summary>
    public class PABackend
    {
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

        private IHost host;
        private static IHubContext<ServerHub> _hubContext;

        private Thread _serverThread;

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
        /// <returns></returns>
        public string[] GetSessionKeys()
        {
            return PAVotingResults.GetSessionKeys();
        }

        private static bool IsSessionActive(string sessionkey)
        {
            return PAVotingResults.GetSessionKeys().Contains(sessionkey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="option"></param>
        public static void CountNewVote(string sessionkey, string option)
        {
            try
            {
                Guid clientPrompt = CurrentPrompt.GetValueOrDefault(sessionkey).Key;

                PAVotingResults.AddVote(sessionkey, clientPrompt, option);
            }
            catch (InvalidOperationException e)
            {
                /* LOG ERROR HERE */
            }
            catch (SessionNotFoundException e)
            {
                /* LOG ERROR HERE */
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="option"></param>
        public static void CountNewVote(string sessionkey, Guid option)
        {
            try
            {
                Guid clientPrompt = CurrentPrompt.GetValueOrDefault(sessionkey).Key;

                PAVotingResults.AddVote(sessionkey, clientPrompt, option);
            }
            catch (InvalidOperationException e)
            {
                /* LOG ERROR HERE */
            }
            catch (SessionNotFoundException e)
            {
                /* LOG ERROR HERE */
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        public bool StartNewSession(string sessionkey)
        {
            if (!IsSessionActive(sessionkey))
            {
                AddNewSession(sessionkey);
                return true;
            }
            else
            {
                /* LOG ERROR HERE */
                return false;
            }
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
        /// <param name="sessionkey"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> EndSession(string sessionkey)
        {
            if (IsSessionActive(sessionkey))
            {
                Dictionary<KeyValuePair<Guid, string>, Dictionary<KeyValuePair<Guid, string>, int>> temp = PAVotingResults.GetStatistics(sessionkey);
                RemoveSession(sessionkey);
                return temp;
            }
            else
            {
                return null;
            }
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
        /// <param name="sessionkey"></param>
        /// <param name="connectionId"></param>
        public static void AddConnection(string sessionkey, string connectionId)
        {
            if (IsSessionActive(sessionkey))
            {
                ConnectionList.GetValueOrDefault(sessionkey).Add(connectionId);
            }
            else
            {
                /* LOG ERROR HERE */
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        public static void RemoveConnection(string connectionId)
        {
            foreach (KeyValuePair<string, List<string>> entry in ConnectionList)
            {
                if (entry.Value.Contains(connectionId))
                {
                    entry.Value.Remove(connectionId);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public Dictionary<KeyValuePair<Guid, string>, int> GetVotingResult(string sessionkey, KeyValuePair<Guid, string> prompt)
        {
            try
            {
                SendPushClear(sessionkey);
                return PAVotingResults.GetOptionsVotesPairsByPrompt(sessionkey, prompt.Key);
            }
            catch (SessionNotFoundException e)
            {
                /* LOG ERROR HERE */
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sessionkey"></param>
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        public async void SendPushMessage(string sessionkey, KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            string pageContent = CreatePageContent(prompt, options);
            CurrentPrompt[sessionkey] = prompt;
            PAVotingResults.AddNewPoll(sessionkey, prompt, options);

            await _hubContext.Clients.Group(sessionkey).SendAsync("NewPrompt", pageContent);
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
        /// <param name="prompt"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        private string CreatePageContent(KeyValuePair<Guid, string> prompt, KeyValuePair<Guid, string>[] options)
        {
            string ret = "";

            string promptString = prompt.Value;
            //string[] optionsStrings = options.SelectMany(keyValuePair => keyValuePair.Values.Select(key => key)).Distinct().ToArray();
            string[] optionsStrings = options.Select(kvp => kvp.Value).ToArray();

            switch (options.Length)
            {
            case 2:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        promptString +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" class=\"input-button voting-container-2items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" class=\"input-button voting-container-2items-2\" value=\"" + optionsStrings[1] + "\" />" +
                      "</div>";
                break;
            case 3:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        promptString +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" class=\"input-button voting-container-3items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" class=\"input-button voting-container-3items-2\" value=\"" + optionsStrings[1] + "\" />" +
                        "<input type=\"button\" id=\"choice-3\" class=\"input-button voting-container-3items-3\" value=\"" + optionsStrings[2] + "\" />" +
                      "</div>";
                break;
            case 4:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        promptString +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" class=\"input-button voting-container-4items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" class=\"input-button voting-container-4items-2\" value=\"" + optionsStrings[1] + "\" />" +
                        "<input type=\"button\" id=\"choice-3\" class=\"input-button voting-container-4items-3\" value=\"" + optionsStrings[2] + "\" />" +
                        "<input type=\"button\" id=\"choice-4\" class=\"input-button voting-container-4items-4\" value=\"" + optionsStrings[3] + "\" />" +
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
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                CreateHostBuilder(7777).Build().Run();
            }

            CreateHostBuilder(Convert.ToInt32(args[0], CultureInfo.CurrentCulture)).Build().Run();
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
            AddNewSession("ASDASD");
            AddNewSession("QWEQWE");

            _serverThread = new Thread(this.ServerStart);
            _serverThread.Start();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ServerStart()
        {
            host = CreateHostBuilder(Port).Build();
            _hubContext = (IHubContext<ServerHub>)host.Services.GetService(typeof(IHubContext<ServerHub>));
            host.Run();
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
        /// <param name="port"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(int port) =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://localhost:" + port + "/");
                });
    }
}