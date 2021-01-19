using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PAClient
{
    public class PABackend
    {
        public static List<string> ValidSessionKeys
        {
            get; set;
        }

        // A list of all voting results, sorted by the GUID of the "voting prompt/questions" 
        public static List<Dictionary<string, List<int>>> VotingResults
        {
            get; private set;
        }

        public static void AddVotingResult(Dictionary<string, List<int>> result)
        {
            VotingResults.Add(result);
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
            ValidSessionKeys = new List<string> { "asdasd" };

            CreateHostBuilder(port).Build().Run();
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