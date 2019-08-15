using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using ConfigurationSample.Extensions;
using Microsoft.EntityFrameworkCore;

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
            {"-CLKey1", "CommandLineKey1"},
            {"-CLKey2", "CommandLineKey2"}
        };
        
        public static readonly Dictionary<string,string> _dict = new Dictionary<string, string>
        {
            {"MemoryCollectionKey1", "value1"},
            {"MemoryCollectionKey2", "value2"}
        };

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();

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

            //xml配置提供程序
            /*
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("config.xml", optional: true, reloadOnChange: true)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
            */

            //Key-per-file配置提供程序，一般用于docker托管方案
            /*
            var path = Path.Combine(Directory.GetCurrentDirectory(), "path//to//files");
            var config = new ConfigurationBuilder()
                .AddKeyPerFile(directoryPath: path, optional: true)
                .Build();

            var host = new WebHostBuilder()
                .UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>();
            */
            
            // 内存配置提供程序
            /*
            var config = new ConfigurationBuilder()
                .AddInMemoryCollection(_dict)
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
                    // 数组绑定至类，其实这句话的意思就是把某个内存集合添加到配置中
                    config.AddInMemoryCollection(arrayDict);
                    config.AddJsonFile("json_array.json", optional: false, reloadOnChange: false);
                    config.AddJsonFile("section.json", optional: false, reloadOnChange: false);
                    config.AddJsonFile("starship.json", optional: false,reloadOnChange:false);
                    config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);
                    config.AddJsonFile("missing_value.json", optional: false, reloadOnChange: false);
                    config.AddJsonFile("JsonArrayExample.json", optional: false, reloadOnChange: false);
                    
                    //使用自定义的配置
                    config.AddEFConfiguration(options => options.UseInMemoryDatabase("InMemoryDb"));
                    
                    //config.AddJsonFile("starship.json", optional: false, reloadOnChange: false);
                    //config.AddXmlFile("tvshow.xml", optional: false, reloadOnChange: false);
                    //config.AddEFConfiguration(options => options.UseInMemoryDataBase("InMemoryDb"));
                    config.AddCommandLine(args);
                })
                .UseStartup<Startup>();

        //交换映射
        public static IWebHostBuilder CreateSwapWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddCommandLine(args, _switchMappings);
                })
                .UseStartup<Startup>();

        //环境变量配置提供程序
        public static IWebHostBuilder CreateEnvironmentWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddEnvironmentVariables(prefix: "PREFIX_");
                })
                .UseStartup<Startup>();

        /*
         * 文件配置提供程序
         */

        //Ini配置提供程序
        public static IWebHostBuilder CreateIniWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddIniFile("config.ini", optional: true, reloadOnChange: true);
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

        // xml提供程序
        public static IWebHostBuilder CreateXmlWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    config.AddXmlFile("config.xml", optional: true, reloadOnChange: true);
                })
                .UseStartup<Startup>();

        // Key-per-file配置提供程序，一般用于docker托管方案，键为文件名，值为文件的内容
        public static IWebHostBuilder CreateKeyPerFileWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Directory.GetCurrentDirectory());
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "path//to//files");
                    config.AddKeyPerFile(directoryPath: path, optional: true);
                })
                .UseStartup<Startup>();
        
        // 内存配置提供程序
        public static IWebHostBuilder CreateMemoryWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) => { config.AddInMemoryCollection(_dict); })
                .UseStartup<Startup>();
    }
}