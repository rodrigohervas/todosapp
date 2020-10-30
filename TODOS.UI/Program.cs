using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;

namespace TODOS.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //get config from appsettings.json
            var serilogConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            //create Serilog Logger object, using serilogConfiguration
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(serilogConfiguration)
                .CreateLogger();

            //Add Serilog self-logging
            Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

            try
            {
                //Build and Run the app host
                Log.Information("TODOList App is Starting up");
                CreateHostBuilder(args).Build().Run();
                Log.Information("TODOList App has Started up successfully");
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "TODOList App start-up failed");
            }
            finally
            {
                //close Log object and flush its memory
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog() //set Serilog as the logging provider
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
