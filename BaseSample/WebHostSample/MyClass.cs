using Microsoft.AspNetCore.Hosting;

namespace WebHostSample
{
    public class MyClass
    {
        private readonly IApplicationLifetime _appLifeTime;

        public MyClass(IApplicationLifetime appLifeTime)
        {
            _appLifeTime = appLifeTime;
        }

        public void ShutDown()
        {
            _appLifeTime.StopApplication();
        }
    }
}
