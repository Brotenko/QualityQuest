using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace PAClient
{
    public class PABackend
    {
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