using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class MyDependency:IMyDependency
    {
        private readonly ILogger<MyDependency> _logger;

        private readonly string _stringKey;

        //�������ļ�ע��һ��ֵ����
        public MyDependency(ILogger<MyDependency> logger,IConfiguration config)
        {
            _logger = logger;
            _stringKey = config["MyStringKey"];
        }

        public Task WriteMessage(string message)
        {
            _logger.LogInformation($"ע�������ֵ��{_stringKey}");
            _logger.LogInformation($"������{nameof(MyDependency.WriteMessage)}��������Ϣ:{message}");
            return Task.FromResult(0);
        }
    }
}