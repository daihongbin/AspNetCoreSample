using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace GenericWebHostSample
{
    //这个里面针对主机的配置，主要是给非web主机项目进行使用，web主机有其专属配置。
    public class Program
    {
        private IHost _host;

        public static async Task Main(string[] args)
        {
            //var host = CreateWebHostBuilder(args).Build();
            //await host.RunAsync();

            var host = new HostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())//设置主机从哪里开始搜索文件
                .UseEnvironment(Microsoft.AspNetCore.Hosting.EnvironmentName.Development)//设置应用环境
                .ConfigureServices((hostContext, services) =>
                {
                    services.Configure<HostOptions>(option =>
                    {
                        //将5秒的关闭超时值增加至20秒
                        option.ShutdownTimeout = TimeSpan.FromSeconds(20);
                    });

                    if (hostContext.HostingEnvironment.IsDevelopment())
                    {
                        //开发环境
                    }
                    else
                    {
                        //其他环境
                    }

                    //services.AddHostedService<LifetimeEventsHostedService>(); //生存期事件
                    //services.AddHostedService<TimedHostedService>(); //定时后台任务
                })
                .ConfigureHostConfiguration(configHost =>  //主机配置
                {
                    configHost.SetBasePath(Directory.GetCurrentDirectory());
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    configHost.AddEnvironmentVariables(prefix: "PREFIX_"); //添加环境变量，并添加前缀
                    configHost.AddCommandLine(args); //添加命令行配置
                })
                .ConfigureAppConfiguration((hostContext, configApp) =>
                {
                    configApp.SetBasePath(Directory.GetCurrentDirectory());
                    configApp.AddJsonFile("hostsettings.json", optional: true);
                    configApp.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json");
                    configApp.AddEnvironmentVariables(prefix: "PREFIX_");
                    configApp.AddCommandLine(args);
                })
                .ConfigureLogging((hostContext,configLogging) => //配置ILoggingBuilder
                {
                    configLogging.AddConsole();
                    configLogging.AddDebug();
                })
                .UseConsoleLifetime()//侦听Ctrl + C
                .UseServiceProviderFactory<ServiceContainer>(new ServiceContainerFactory()) //自己配置的服务容器
                .ConfigureContainer<ServiceContainer>((hostContext,container) => 
                {

                })
                //.UseHostedService<TimedHostedService>()
                .Build();

            //host.Run(); //Run 运行应用并阻止调用线程，直到关闭主机
            //host.RunConsoleAsync(); //启用控制台支持、生成和启动主机，以及等待 Ctrl+C/SIGINT 或 SIGTERM 关闭
            await host.StartAsync(); //RunAsync 运行应用并返回在触发取消令牌或关闭时完成的 Task

            /*
            using (host)
            {
                host.Start(); //同步启动主机
                await host.StopAsync(TimeSpan.FromSeconds(5)); //尝试在提供的超时时间内停止主机
            }
            */

            /*
            using (host)
            {
                await host.StopAsync(); //启动应用
                await host.StopAsync(); //停止应用
            }
            */

            /*
            using (host)
            {
                host.Start(); //WaitForShutdown 通过 IHostLifetime 触发，例如 ConsoleLifetime（侦听 Ctrl+C/SIGINT 或 SIGTERM）
                host.WaitForShutdown(); //WaitForShutdown 调用 StopAsync
            }
            */

            /*
            using (host)
            {
                await host.StartAsync();
                await host.WaitForShutdownAsync(); //会调用StopAsync
            }
            */


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseContentRoot(Directory.GetCurrentDirectory())//设置主机从哪里开始搜索文件
                .UseEnvironment(Microsoft.AspNetCore.Hosting.EnvironmentName.Development) //设置应用环境
                .UseStartup<Startup>();

        public async Task StartAsync() => await _host.StartAsync();

        public async Task StopAsync()
        {
            using (_host)
            {
                await _host.StopAsync(TimeSpan.FromSeconds(5));
            }
        }
        
    }
}
