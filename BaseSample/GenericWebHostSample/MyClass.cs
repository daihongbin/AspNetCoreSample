using Microsoft.AspNetCore.Hosting;

namespace GenericWebHostSample
{
    //构造函数注入应用托管环境
    public class MyClass
    {
        private readonly IHostingEnvironment _env;

        private readonly IApplicationLifetime _appLifeTime;

        public MyClass(Microsoft.AspNetCore.Hosting.IHostingEnvironment env,IApplicationLifetime appLifeTime)
        {
            _env = env;
            _appLifeTime = appLifeTime;
        }

        public void DoSomething()
        {
            var environmentName = _env.EnvironmentName;
        }

        public void ShutDown()
        {
            _appLifeTime.StopApplication();
        }
    }
}
