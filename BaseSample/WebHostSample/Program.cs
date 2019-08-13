using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace WebHostSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            //CreateAnotherWebHostBuilder(args).Build().Run();

            /*
            //通过调用 Start 方法以非阻止方式运行主机
            var host = new WebHostBuilder().Build();
            using (host)
            {
                host.Start();
                Console.ReadLine();
            }
            */

            /*
            //如果 URL 列表传递给 Start 方法，该列表侦听指定的 URL：
            var urls = new List<string>
            {
                "http://*:5000",
                "http://localhost:5001"
            };

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseStartup<Startup>()
                .Start(urls.ToArray());

            using (host)
            {
                Console.ReadLine();
            }
            */

            /*
            //Start(RequestDelegate app)
            using (var host = WebHost.Start(app => app.Response.WriteAsync("Hello,Micahel!")))
            {
                Console.WriteLine("Use Ctrl + C to shutdown the host......");
                host.WaitForShutdown();
            }
            */

            //Start(string url, RequestDelegate app)
            /*
            using (var host = WebHost.Start("http://localhost:8080", app => app.Response.WriteAsync("Hello,Michael!")))
            {
                Console.WriteLine("Use Ctrl + C to shutdown the host...");
                host.WaitForShutdown();
            }
            */

            //Start(Action<IRouteBuilder> routeBuilder)
            /*
            using (var host = WebHost.Start(router =>
            {
                router.MapGet("hello/{name}", (req, res, data) =>
                 {
                     return res.WriteAsync($"Hello,{data.Values["name"]}!");
                 })
                .MapGet("buenosdias/{name}", (req, res, data) =>
                 {
                     return res.WriteAsync($"Buenos dias,{data.Values["name"]}");
                 })
                .MapGet("throw/{message?}", (req, res, data) =>
                 {
                     throw new Exception((string)data.Values["message"] ?? "Uh oh!");
                 })
                .MapGet("{greeting}/{name}", (req, res, data) =>
                 {
                     return res.WriteAsync($"{data.Values["greeting"]},{data.Values["name"]}!");
                 })
                .MapGet("", (req, res, data) =>
                 {
                     return res.WriteAsync("Hello,Michael!");
                 });
            }))
            {
                Console.WriteLine("Use Ctrl + C to shutdown the host...");
                host.WaitForShutdown();
            }
            */

            /*
            //Start(string url, Action<IRouteBuilder> routeBuilder)
            using (var host = WebHost.Start("http://localhost:8080", router =>
            {
                router.MapGet("hello/{name}", (req, res, data) =>
                {
                    return res.WriteAsync($"Hello,{data.Values["name"]}!");
                })
                .MapGet("buenosdias/{name}", (req, res, data) =>
                {
                    return res.WriteAsync($"Buenos dias,{data.Values["name"]}");
                })
                .MapGet("throw/{message?}", (req, res, data) =>
                {
                    throw new Exception((string)data.Values["message"] ?? "Uh oh!");
                })
                .MapGet("{greeting}/{name}", (req, res, data) =>
                {
                    return res.WriteAsync($"{data.Values["greeting"]},{data.Values["name"]}!");
                })
                .MapGet("", (req, res, data) =>
                {
                    return res.WriteAsync("Hello,Michael!");
                });
            }))
            {
                Console.WriteLine("Use Ctrl + C to shutdown the host...");
                host.WaitForShutdown();
            }
            */

            //StartWith(Action<IApplicationBuilder> app)
            /*
            using (var host = WebHost.StartWith(app => app.Use(next => 
            {
                return async context =>
                {
                    await context.Response.WriteAsync("Hello Michael!");
                };
            })))
            {
                Console.WriteLine("Use Ctrl + C to shutdown the host...");
                host.WaitForShutdown();
            }
            */

            //StartWith(string url, Action<IApplicationBuilder> app)
            using (var host = WebHost.StartWith("http://localhost:8080", app => app.Use(next =>
            {
                return async context =>
                {
                    await context.Response.WriteAsync("Hello Michael!");
                };
            })))
            {
                Console.WriteLine("Use Ctrl + C to shutdown the host...");
                host.WaitForShutdown();
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseDefaultServiceProvider((context,options) => 
            {
                options.ValidateScopes = true;
            })
            .UseSetting(WebHostDefaults.ApplicationKey, "WebHostSample") //显示指定应用程序名称
            .UseSetting(WebHostDefaults.DetailedErrorsKey, "true") //是否捕获详细错误
            //.UseSetting(WebHostDefaults.PreventHostingStartupKey,"true") //是否阻止承载启动
            //.UseSetting(WebHostDefaults.HostingStartupAssembliesKey,"assembly1;assembly2;") //设置承载启动程序集
            //.UseSetting(WebHostDefaults.HostingStartupExcludeAssembliesKey,"assembly3;assembly3;") //设置承载启动排除程序集
            //.PreferHostingUrls(false) //是否侦听WebHostBuilder配置的url
            .UseSetting("https_port", "8080") //设置HTTPS端口
            .CaptureStartupErrors(true) //是否捕获启动错误
            .UseEnvironment(EnvironmentName.Development) //设置应用程序环境
            .UseContentRoot(Directory.GetCurrentDirectory()) //指定web程序从哪里搜索文件
            .UseUrls("http://localhost:5000") //配置应用url
            .UseShutdownTimeout(TimeSpan.FromSeconds(10)) //指定等待web主机关闭的时长
            .ConfigureAppConfiguration((hostingContext, config) =>  //配置web服务配置
            {
                //config.AddXmlFile("appsettings.xml",optional:true,reloadOnChange:true); //指定使用其他配置文件
            })
            .ConfigureLogging(logging => //配置日志
            {
                logging.SetMinimumLevel(LogLevel.Warning); //设置最小日志级别
            })
            .ConfigureKestrel((context, options) =>
            {
                options.Limits.MaxRequestBodySize = 20000000; //设置最大请求体大小
            })
            //.UseWebRoot("") //指定静态资源路径
            //.UseStartup("StartupAssemblyName") //指定启动程序集
            .UseStartup<Startup>();


        //重写配置
        public static IWebHostBuilder CreateAnotherWebHostBuilder(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("hostsettings.json", optional: true)
                .AddCommandLine(args)
                .Build();

            return WebHost.CreateDefaultBuilder(args)
                .UseUrls("http://*:5000")
                .UseConfiguration(config)
                .Configure(app =>
                {
                    app.Run(context => context.Response.WriteAsync("Hello,World!"));
                });
        }
    }
}
