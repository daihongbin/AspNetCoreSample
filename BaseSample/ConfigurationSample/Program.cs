using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConfigurationSample
{
    public class Program
    {
        public static Dictionary<string, string> arrayDict = new Dictionary<string, string>
        {
            {"array:entries:0", "value0"},
            {"array:entries:1", "value1"},
            {"array:entries:2", "value2"},
            {"array:entries:4", "value4"},
            {"array:entries:5", "value5"}
        };

        public static readonly Dictionary<string, string> _switchMappings = new Dictionary<string, string>
        {
            { "-CLKey1", "CommandLineKey1" },
            { "-CLKey2", "CommandLineKey2" }
        };

        public static void Main(string[] args)
        {
            //CreateWebHostBuilder(args).Build().Run();

            //直接创建 WebHostBuilder 时，请使用以下配置调用 UseConfiguration
            /*
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
            */

            //交换映射
            //CreateSwapWebHostBuilder(args).Build().Run();

            //环境配置
            /*
            //可以直接这样使用
            var config = new ConfigurationBuilder()
                .AddEnvironmentVariables("CUSTOM_")
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
            */

            //文件配置提供程序
            //ini配置提供程序
            /*
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddIniFile("config.ini",optional:true,reloadOnChange:true)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
            */

            //json配置提供程序
            /*
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: true, reloadOnChange: true)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
            */


        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddInMemoryCollection(arrayDict);
                    config.AddJsonFile("json_array.json", optional: false, reloadOnChange: false);
                    //config.AddJsonFile("starship.json", optional: false, reloadOnChange: false);
                    //config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);
                    //config.AddEFConfiguration(options => options.UseInMemoryDataBase("InMemoryDb"));
                    config.AddCommandLine(args);
                })
                .UseStartup<Startup>();

        //交换映射
        public static IWebHostBuilder CreateSwapWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext,config) => 
            {
                config.AddCommandLine(args,_switchMappings);
            })
            .UseStartup<Startup>();

        //环境变量配置提供程序
        public static IWebHostBuilder CreateEnvironmenWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.AddEnvironmentVariables(prefix:"PREFIX_");
            })
            .UseStartup<Startup>();

        /*
         * 文件配置提供程序
         */

        //Ini配置提供程序
        public static IWebHostBuilder CreateIniWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext,config) => 
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddIniFile("config.ini",optional:true,reloadOnChange:true);
            })
            .UseStartup<Startup>();

        //json配置提供程序
        public static IWebHostBuilder CreateJsonWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory());
                config.AddJsonFile("config.json", optional: true, reloadOnChange: true);
            })
            .UseStartup<Startup>();
        
    }
}
