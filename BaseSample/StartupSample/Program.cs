using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StartupSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {

                })
                .ConfigureServices(services => 
                {

                })
                .Configure(app => 
                {
                    var loggerFactory = app.ApplicationServices
                        .GetRequiredService<ILoggerFactory>();
                    var logger = loggerFactory.CreateLogger<Program>();
                    var env = app.ApplicationServices.GetRequiredService<IHostingEnvironment>();
                });
    }
}
