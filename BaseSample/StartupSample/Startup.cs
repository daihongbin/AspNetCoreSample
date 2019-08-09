using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StartupSample
{
    public class Startup
    {
        private readonly IHostingEnvironment _env;

        private readonly IConfiguration _config;

        private readonly ILoggerFactory _loggerFactory;

        public Startup(IHostingEnvironment env,IConfiguration config,ILoggerFactory loggerFactory)
        {
            _env = env;
            _config = config;
            _loggerFactory = loggerFactory;
        }
        
        // 这个方法主要是注册服务用的
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IStartupFilter, RequestSetOptionsStartupFilter>();

            services.AddOptions<AppOptions>();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);

            #region 输出当前环境的日志
            var logger = _loggerFactory.CreateLogger<Startup>();
            if (_env.IsDevelopment())
            {
                logger.LogInformation("Development envirment");
            }
            else
            {
                logger.LogInformation($"Environment:{_env.EnvironmentName}");
            }
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
                        
            app.UseMvc(routes =>
            {
                routes.MapRoute
                (
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}"
                );
            });
        }
    }
}
