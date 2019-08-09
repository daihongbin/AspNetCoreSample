using Autofac;
using Autofac.Extensions.DependencyInjection;
using DependencyInjectionSample.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;

namespace DependencyInjectionSample
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //注意第2行不生效，因为已经有一个IMydependency的注入了
            services.AddSingleton<IMyDependency, MyDependency>();
            //services.TryAddSingleton<IMyDependency, DifferentDependency>();

            //跟上面同理，第3行注入不生效
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IMyDep1, MyDep>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IMyDep2, MyDep>());
            services.TryAddEnumerable(ServiceDescriptor.Singleton<IMyDep1, MyDep>());

            services.AddTransient<IOperationTransient, Operation>();
            services.AddScoped<IOperationScoped,Operation>();
            services.AddSingleton<IOperationSingleton, Operation>();
            services.AddSingleton<IOperationSingletonInstance>(new Operation(Guid.Empty));

            services.AddTransient<OperationService, OperationService>();


            //IDispose
            services.AddScoped<Service1>();
            services.AddSingleton<Service2>();
            services.AddSingleton<ISomeService>(sp => new SomeServiceImplementation());

            services.AddSingleton<Service3>(new Service3());
            services.AddSingleton(new Service3());

        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
