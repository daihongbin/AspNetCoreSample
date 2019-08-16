using System.Reflection;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EnvironmentSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            // 使用接受程序集的UseStartup方法
            var assemblyName = typeof(Startup).GetTypeInfo().Assembly.FullName;

            return WebHost.CreateDefaultBuilder(args).UseStartup(assemblyName);
        }
            
    }
}