using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace GenericWebHostSample
{
    internal class LifetimeEventsHostedService : IHostedService
    {
        private readonly ILogger _logger;

        private readonly IApplicationLifetime _appLifeTime;

        public LifetimeEventsHostedService(ILogger<LifetimeEventsHostedService> logger,IApplicationLifetime appLifeTime)
        {
            _logger = logger;
            _appLifeTime = appLifeTime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _appLifeTime.ApplicationStarted.Register(OnStarted);
            _appLifeTime.ApplicationStopping.Register(OnStopping);
            _appLifeTime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private void OnStarted() => _logger.LogInformation($"{nameof(OnStarted)}函数被调用！");

        private void OnStopping() => _logger.LogInformation($"{nameof(OnStopping)}函数被调用！");

        private void OnStopped() => _logger.LogInformation($"{nameof(OnStopped)}函数被调用！");

    }
}
