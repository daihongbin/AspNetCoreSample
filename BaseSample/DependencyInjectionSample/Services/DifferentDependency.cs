using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DependencyInjectionSample.Services
{
    public class DifferentDependency : IMyDependency
    {
        private readonly ILogger<DifferentDependency> _logger;

        public DifferentDependency(ILogger<DifferentDependency> logger)
        {
            _logger = logger;
        }
        public Task WriteMessage(string message)
        {
            _logger.LogInformation($"我是不一样的注入：{message}");
            return Task.CompletedTask;
        }
    }
}
