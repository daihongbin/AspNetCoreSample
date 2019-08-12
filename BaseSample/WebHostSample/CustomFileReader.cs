using Microsoft.AspNetCore.Hosting;

namespace WebHostSample
{
    public class CustomFileReader
    {
        private readonly IHostingEnvironment _env;

        public CustomFileReader(IHostingEnvironment env)
        {
            _env = env;
        }

        public string ReadFile(string filePath)
        {
            var fileProvider = _env.WebRootFileProvider;
            return string.Empty;
        }

    }
}
