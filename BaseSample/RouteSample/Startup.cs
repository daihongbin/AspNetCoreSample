using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.Extensions.DependencyInjection;

namespace RouteSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc(routes =>
            {
                // 通常是这样配置的
                /*
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                */
                
                // 添加了路由约束的路由
                routes.MapRoute(
                    name:"default",
                    template:"{controller=Home}/{action=Index}/{id:int}"); 
                
                // 其实也可以这样配置
                /*
                routes.MapRoute(
                    name: "default_route",
                    template: "{controller}/{action}/{id?}",
                    defaults: new {controller = "Home", action = "Index"});
                */
                
                // blog，与/Blog/All-About-Routing/Introduction路径匹配，controller和action拿的路由的默认值
                // article就是All-About-Routing/Introduction
                routes.MapRoute(
                    name: "blog",
                    template: "Blog/{**article}",
                    defaults: new {controller = "Blog", action = "ReadArticle"});

                // 添加了路由约束和数据令牌
                // 与 /en-US/Products/5 等 URL 路径相匹配，并且提取值 { controller = Products, action = Details, id = 5 } 和数据令牌 { locale = en-US }
                routes.MapRoute(
                    name: "us_english_products",
                    template: "en-US/Products/{id}",
                    defaults: new {controller = "Products", action = "Details"},
                    constraints: new {id = new IntRouteConstraint()},
                    dataTokens: new {locale = "en-US"});
            });

            #region 路由配置
            var trackPackageRouteHandler = new RouteHandler(context =>
            {
                var routeValues = context.GetRouteData().Values;
                return context.Response.WriteAsync($"Hello! Route Values: {string.Join(", ", routeValues)}");
            });
            
            var routeBuilder = new RouteBuilder(app,trackPackageRouteHandler);

            routeBuilder.MapRoute(
                "Track Package Route",
                "package/{operation:regex(^track|create$)}/{id:int}");

            routeBuilder.MapGet("hello/{name}", context =>
            {
                var name = context.GetRouteValue("name");
                return context.Response.WriteAsync($"Hi, {name}");
            });

            var route = routeBuilder.Build();
            app.UseRouter(route);
            #endregion
        }
    }
}