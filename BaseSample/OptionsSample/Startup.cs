using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OptionsSample.Models;

namespace OptionsSample
{
    public class Startup
    {
        private readonly IConfiguration _config;
        
        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MyOptions>(_config);

            // 还可以这样写
            /*var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true);
            var config = configBuilder.Build();
            services.Configure<MyOptions>(config);*/

            // 通过委托配置简单选项
            services.Configure<MyOptionsWithDelegateConfig>(myOptions =>
            {
                myOptions.Option1 = "value1_configured_by_delegate";
                myOptions.Option2 = 500;
            });

            // 这个取的配置文件里的
            services.Configure<MyOptions>("named_options_1", _config);

            // 这个是自己给，然后用的默认值
            services.Configure<MyOptions>("named_options_2",
                myOptions => { myOptions.Option1 = "named_options_2_value1_from_action"; });

            //覆盖所有配置值
            services.ConfigureAll<MyOptions>(myOptions => { myOptions.Option1 = "全局"; });

            //services.AddOptions<MyOptions>().Configure(o => o.Option1 = "11");

            //services.AddOptions<MyOptions>("optionalName").Configure(o => o.Option1 = "named");
            
            // DI配置选项，没搞懂这个有啥用V.V
            /*
            services.AddOptions<MyOptions>("optionalName")
                .Configure<Service1, Service2, Service3, Service4, Service5>((o, s, s2, s3, s4, s5) =>
                {
                    o.Option1 = DoSomethingWith(s, s2, s3, s4, s5);
                });
            */
            
            // 选项验证，还是没搞懂
            /*
            services.AddOptions<MyOptions>("optionalOptionsName")
                .Configure(o => { })
                .Validate(o => YourValidationShouldReturnTrueIfValid(o), "custom error");

            var monitor = services.BuildServiceProvider().GetService<IOptionsMonitor<MyOptions>>();

            try
            {
                var options = monitor.Get("optionalOptionsName");
            }
            catch (OptionsValidationException e)
            {
                
            }
            */
            
            // 选项后期配置
            services.PostConfigure<MyOptions>(myOptions => { myOptions.Option1 = "post_configured_option1_value"; });

            // 就是只配置某一个的意思
            //services.PostConfigure<MyOptions>("named_options_1", myOptions => { myOptions.Option1 = "命名选项后期配置"; });

            // 无差别配置所有
            services.PostConfigureAll<MyOptions>(myOptions => { myOptions.Option1 = "配置所有"; });
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IOptionsMonitor<MyOptions> optionsAccessor)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            var option1 = optionsAccessor.CurrentValue.Option1;
            Console.WriteLine("******************" + option1);
            
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvcWithDefaultRoute();
        }
    }
}