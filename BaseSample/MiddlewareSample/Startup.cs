using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace MiddlewareSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/map1",HandleMapTest1);
            app.Map("/map2",HandleMapTest2);

            //如果QueryString包含branch参数则进入这个map
            app.MapWhen(context => 
            {
                return context.Request.Query.ContainsKey("branch");
            },HandleBranch);

            //Map也支持嵌套
            app.Map("/level1",level1App => 
            {
                level1App.Map("/level2a",level2AApp => 
                {

                });

                level1App.Map("/level2b",level2BApp => 
                {

                });
            });

            //匹配多个段
            app.Map("/map1/seg1",HandleMultiSeg);

            // 此处为一个中间件
            app.Use(async (context,next) => 
            {
                await next.Invoke();
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");
            });
        }

        #region 注册一些map中间件
        private static void HandleMapTest1(IApplicationBuilder app)
        {
            app.Run(async context => 
            {
                await context.Response.WriteAsync("Map Test 1");
            });
        }

        private static void HandleMapTest2(IApplicationBuilder app)
        {
            app.Run(async context => 
            {
                await context.Response.WriteAsync("Map Test 2");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Run(async context => 
            {
                var branchVer = context.Request.Query["branch"];
                await context.Response.WriteAsync($"Branch used = {branchVer}");
            });
        }

        private static void HandleMultiSeg(IApplicationBuilder app)
        {
            app.Run(async context => 
            {
                await context.Response.WriteAsync("Map multiple sements.");
            });
        }
        #endregion
    }
}
