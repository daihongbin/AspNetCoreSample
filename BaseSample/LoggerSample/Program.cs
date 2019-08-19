using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace LoggerSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // CreateWebHostBuilder(args).Build().Run();
            
            // CreateLoggerScopeWebHostBuilder(args).Build().Run();
            
            CreateLoggerProviderWebHostBuilder(args).Build().Run();

            // 添加提供程序
            /*
            var webHost = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    var env = hostingContext.HostingEnvironment;
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                    config.AddEnvironmentVariables();
                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                })
                .UseStartup<Startup>()
                .Build();
            
            webHost.Run();
            */

            //这里应该是讲怎么获取logger
            /*
            var host = CreateWebHostBuilder(args).Build();

            var todoRepository = host.Services.GetRequiredService<ITodoRepository>();
            todoRepository.Add(new Core.Model.TodoItem() { Name = "Feed the dog" });
            todoRepository.Add(new Core.Model.TodoItem() { Name = "Walk the dog" });

            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Seeded the database.");
            
            host.Run();
            */
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                });
        
        // 演示在代码中配置筛选规则，配置文件中配置筛选规则已经在appsettings.json文件中有所体现
        // 第二个 AddFilter 使用类型名称来指定调试提供程序。 第一个 AddFilter 应用于全部提供程序，因为它未指定提供程序类型。
        public static IWebHostBuilder CreateLoggerFilterWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.AddFilter("System", LogLevel.Debug)
                        .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Trace);
                });
        
        // 演示配置最低日志级别
        public static IWebHostBuilder CreateMinimumLevelWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Warning));
        
        // 筛选器函数
        public static IWebHostBuilder CreateFilterFunctionWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logBuilder =>
                {
                    logBuilder.AddFilter((provider, category, logLevel) =>
                    {
                        if (provider == "Microsoft.Extensions.Logging.Console.ConsoleLoggerProvider" 
                            && category == "LoggerSample.Controllers.TodoController")
                        {
                            return false;
                        }
                        return true;
                    });
                });
        
        // 启用日志作用域
        public static IWebHostBuilder CreateLoggerScopeWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole(options => { options.IncludeScopes = true; });
                    logging.AddDebug();
                });
        
        // 各式各样的内置日志提供程序
        public static IWebHostBuilder CreateLoggerProviderWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    // 控制台日志提供程序
                    logging.AddConsole();
                    
                    // EventSource提供程序,使用PerfView实用工具进行查看日志
                    logging.AddEventSourceLogger();
                    
                    // Windows EventLog提供程序，需要Microsoft.Extensions.Logging.EventLog包
                    //logging.AddEventLog();
                    
                    // TraceSource提供程序，需要Microsoft.Extensions.Logging.TraceSource包
                    //logging.AddTraceSource(sourceSwitchName);
                    
                    // Azure应用服务提供程序，需要Microsoft.Extensions.Logging.AzureAppServices包，由于国内使用Azure服务较少，就不写了
                    //logging.AddAzureWebAppDiagnostics();
                });
        
        // 关于Azure应用服务提供程序的配置
        /*
        public static IWebHostBuilder CreateAzureWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
                .ConfigureServices(serviceCollection => serviceCollection
                    .Configure<AzureFileLoggerOptions>(options =>
                    {
                        options.FileName = "azure-diagnostics-";
                        options.FileSizeLimit = 50 * 1024;
                        options.RetainedFileCountLimit = 5;
                    }).Configure<AzureBlobLoggerOptions>(options =>
                    {
                        options.BlobName = "log.txt";
                    }))
                .UseStartup<Startup>();
        */
    }
}