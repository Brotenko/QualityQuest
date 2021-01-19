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
        private int Port { set; get; }
        private IHost host;
        private static IHubContext<ServerHub> _hubContext;

        private Thread _serverThread;


        public static List<string> ValidSessionKeys
        {
            get; set;
        } 

        // A list of all voting results, sorted by the GUID of the "voting prompt/questions" 
        public static Dictionary<string, List<int>> VotingResults
        {
            get; private set;
        }

        // A list of all groups, each having their own list of connected clients
        public static Dictionary<string, List<string>> ConnectionList
        {
            get; private set;
        } 

        public static void AddVotingResult(string prompt, List<int> votes)
        {
            VotingResults.Add(prompt, votes);
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
            VotingResults = new Dictionary<string, List<int>>();
            ConnectionList = new Dictionary<string, List<string>>();
            ValidSessionKeys = new List<string> { "asdasd" };
            UpdateSessions("asdasd");

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