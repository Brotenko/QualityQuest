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

namespace PAClient
{
    public class PABackend
    {
        // Current prompt depending on the session
        //                 sessionkey             prompt
        public static Dictionary<string, Dictionary<Guid, string>> CurrentPrompt
        {
            get; set;
        }

        private int Port
        {
            set; get;
        }

        private IHost host;
        private static IHubContext<ServerHub> _hubContext;

        private Thread _serverThread;


        public static List<string> ValidSessionKeys
        {
            get; set;
        }

        // A list of all voting results, sorted by the GUID of the "voting prompt/questions" 
        /*
         * List:
         *  - Prompt 1:
         *     - Option 1 => Number of votes
         *     - Option 2 => Number of votes
         *  - Prompt 2:
         *     - ...
         */
        public static VotingResults PAVotingResults
        {
            get; private set;
        }

        // A list of all groups, each having their own list of connected clients
        public static Dictionary<string, List<string>> ConnectionList
        {
            get; private set;
        }

        
        public static void CountNewVote(string choice)
        {
            Dictionary<string, int> tempDict = PAVotingResults.GetValueOrDefault(CurrentPrompt);

            Console.WriteLine(tempDict.GetValueOrDefault(choice));
            tempDict[choice] = 1 + tempDict.GetValueOrDefault(choice);
            Console.WriteLine(tempDict.GetValueOrDefault(choice));
        }
        


        public static void UpdateSessions(string session)
        {
            try
            {
                foreach (KeyValuePair<string, List<string>> entry in ConnectionList)
                {
                    if (entry.Key == session)
                    {
                        ConnectionList.Remove(entry.Key);
                    }
                }
            }
            catch (NullReferenceException)
            {
                ConnectionList.Add(session, new List<string>());
            }
            finally
            {
                ConnectionList.Add(session, new List<string>());
            }
        }

        public static void AddConnection(string session, string connectionId)
        {
            if (!ConnectionList.GetValueOrDefault(session).Contains(connectionId))
            {
                ConnectionList.GetValueOrDefault(session).Add(connectionId);
            }
        }

        public static void RemoveConnections(string connectionId)
        {
            foreach (KeyValuePair<string, List<string>> entry in ConnectionList)
            {
                if (entry.Value.Contains(connectionId))
                {
                    entry.Value.Remove(connectionId);
                }
            }
        }


        public Dictionary<Dictionary<Guid, string>, int> getVotingResult(string sessionkey, string prompt)
        {
            SendPushClear(sessionkey);
            return PAVotingResults.GetOptionsVotesPairsByPrompt(sessionkey, prompt);
        }


        public async void SendPushMessage(string sessionkey, Dictionary<Guid,string> prompt, Dictionary<Guid, string>[] options)
        {
            string pageContent = CreatePageContent(prompt, options);
            CurrentPrompt[sessionkey] = prompt;
            PAVotingResults.AddNewPoll(sessionkey, prompt, options);

            await _hubContext.Clients.Group(sessionkey).SendAsync("NewPrompt", pageContent);
        }

        public async void SendPushClear(string group)
        {
            await _hubContext.Clients.Group(group).SendAsync("ClearPrompt");
        }

        private string CreatePageContent(Dictionary<Guid, string> prompt, Dictionary<Guid, string>[] options)
        {
            string ret = "";

            string promptString = prompt.First().Value;
            string[] optionsStrings = options.SelectMany(keyValuePair => keyValuePair.Values.Select(key => key)).Distinct().ToArray();

            switch (options.Length)
            {
            case 2:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        prompt +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" class=\"input-button voting-container-2items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" class=\"input-button voting-container-2items-2\" value=\"" + optionsStrings[1] + "\" />" +
                      "</div>";
                break;
            case 3:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        prompt +
                      "</div>" +
                      "<div id=\"voting-container\" class=\"voting-container\">" +
                        "<input type=\"button\" id=\"choice-1\" class=\"input-button voting-container-3items-1\" value=\"" + optionsStrings[0] + "\" />" +
                        "<input type=\"button\" id=\"choice-2\" class=\"input-button voting-container-3items-2\" value=\"" + optionsStrings[1] + "\" />" +
                        "<input type=\"button\" id=\"choice-3\" class=\"input-button voting-container-3items-3\" value=\"" + optionsStrings[2] + "\" />" +
                      "</div>";
                break;
            case 4:
                ret = "<div id=\"voting-prompt\" name=\"future-guid\" class=\"voting-prompt text-center\">" +
                        prompt +
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

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                CreateHostBuilder(7777).Build().Run();
            }

            CreateHostBuilder(Convert.ToInt32(args[0], CultureInfo.CurrentCulture)).Build().Run();
        }

        public PABackend(int port)
        {
            Port = port;
            ValidSessionKeys = new List<string>();
            PAVotingResults = new VotingResults();
            ConnectionList = new Dictionary<string, List<string>>();
            ValidSessionKeys = new List<string> { "asdasd", "qweqwe" };
            UpdateSessions("asdasd");
            UpdateSessions("qweqwe");

            _serverThread = new Thread(this.ServerStart);
            _serverThread.Start();
        }

        private void ServerStart()
        {
            host = CreateHostBuilder(Port).Build();
            _hubContext = (IHubContext<ServerHub>)host.Services.GetService(typeof(IHubContext<ServerHub>));
            host.Run();
        }

        public void StopServer()
        {
            host.StopAsync();
            _serverThread.Join();
        }

        public static IHostBuilder CreateHostBuilder(int port) =>
            Host.CreateDefaultBuilder()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://localhost:" + port + "/");
                });
    }
}