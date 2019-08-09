using Microsoft.Extensions.Options;

namespace StartupSample
{
    public class AppOptions : IOptions<AppOptions>
    {
        public AppOptions Value => this;

        public string Option { get; set; }
    }
}
