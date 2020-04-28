using System;
using System.IO;
using System.Threading.Tasks;
using Demo.Data;
using Demo.Data.Factories;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLog;
using NLog.Web;

namespace Demo.Api
{
    public class Program
    {
        public static Logger Logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static async Task Main(string[] args)
        {
            try
            {
                Logger.Trace("Application start");
                IWebHost webHost = CreateWebHostBuilder(args).Build();

                new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

                using (var scope = webHost.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;

                    var factory = services.GetRequiredService<IRepositoryContextFactory>();

                    await DbInitializer.Initialize(factory); // auto-migration on start
                }

                webHost.Run();
            }
            catch (Exception exception)
            {
                Logger.Error(exception, "Exception on application start ");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseNLog();
    }
}
