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
                .UseStartup<Startup>();
                // 此处可以进行类似Startup类中类似的配置，下面只是演示一下，实际中，很少会这么做
                /*
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
                */
    }
}
