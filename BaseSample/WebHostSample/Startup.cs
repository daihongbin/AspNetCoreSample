using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace WebHostSample
{
    public class Startup
    {
        public IHostingEnvironment HostingEnvironment { get; }

        public Startup(IHostingEnvironment hostingEnvironment)
        {
            HostingEnvironment = hostingEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,IApplicationLifetime appLifeTime)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                
            }
            else
            {
                app.UseExceptionHandler("/error");
            }

            var contentRootPath = HostingEnvironment.ContentRootPath;
        }

        public async Task Invoke(HttpContext context, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {

            }
            else
            {

            }

            var contentRootPath = env.ContentRootPath;
        }

        private void OnStarted()
        {

        }

        private void OnStopping()
        {

        }

        private void OnStopped()
        {

        }
    }
}
