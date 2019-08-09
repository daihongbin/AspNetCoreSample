using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class MyDependency:IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        private readonly string _stringKey;

        //从配置文件注入一个值进来
        public MyDependency(ILogger<MyDependency> logger,IConfiguration config)
        {
            _logger = logger;
            _stringKey = config["MyStringKey"];
        }

        public Task WriteMessage(string message)
        {
            _logger.LogInformation($"注入的配置值：{_stringKey}");
            _logger.LogInformation($"调用了{nameof(MyDependency.WriteMessage)}方法，信息:{message}");
            return Task.FromResult(0);
        }
    }
}