using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course.Gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>

            //here is for json files
            Host.CreateDefaultBuilder(args).ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddJsonFile($"configuration.{hostingContext.HostingEnvironment.EnvironmentName.ToLower()}.json").AddEnvironmentVariables();
            })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
