using AspNetCore.Mini.Core;
using System;

namespace AppCore
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .Build()
                .Run();

            Console.ReadKey();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            return new WebHostBuilder()
                .UseHttpListener()
                .Configure(app => app.Use(FooMiddleware).Use(BarMiddleware).Use(BazMiddleware));
        }

        #region 自定义中间件
        public static RequestDelegate FooMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Foo=>");
            await next(context);
        };

        public static RequestDelegate BarMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Bar=>");
            await next(context);
        };

        public static RequestDelegate BazMiddleware(RequestDelegate next) => async context =>
        {
            await context.Response.WriteAsync("Baz=>");
            await next(context);
        };

        #endregion
    }
}
