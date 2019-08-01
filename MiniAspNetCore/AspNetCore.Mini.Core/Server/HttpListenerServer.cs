using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    public class HttpListenerServer : IServer
    {
        private readonly HttpListener _httpListener;

        private readonly string[] _urls;

        public HttpListenerServer(params string[] urls)
        {
            _httpListener = new HttpListener();
            //绑定默认监听地址（默认为5000端口）
            _urls = urls.Any() ? urls : new string[] { "http://localhost:5000/" };
        }

        public async Task RunAsync(RequestDelegate handler)
        {
            Array.ForEach(_urls, url => _httpListener.Prefixes.Add(url));

            if (!_httpListener.IsListening)
            {
                //启动HttpListener
                _httpListener.Start();
            }
            Console.WriteLine($"[Info]: Server started and is listening on :{string.Join(";", _urls)}");


            while (true)
            {
                //等待传入的请求，该方法将阻塞进程，直至收到请求
                var listenContext = await _httpListener.GetContextAsync();
                Console.WriteLine($"{listenContext.Request.HttpMethod} {listenContext.Request.RawUrl} HTTP/{listenContext.Request.ProtocolVersion}");

                //获取抽象封装后的HttpListenerFeature
                var feature = new HttpListenerFeature(listenContext);

                //获取封装后的Feature集合
                var features = new FeatureCollection().Set<IHttpRequestFeature>(feature).Set<IHttpResponseFeature>(feature);

                //创建HttpContext
                var httpContext = new HttpContext(features);
                Console.WriteLine("[Info]:Server process one HTTP request start.");

                //开始执行中间件
                await handler(httpContext);
                Console.WriteLine("[Info]:Server process one HTTP request end.");

                //关闭响应
                listenContext.Response.Close();
            }
        }
    }

    public static partial class Extensions
    {
        public static IWebHostBuilder UseHttpListener(this IWebHostBuilder builder, params string[] urls)
        {
            return builder.UseServer(new HttpListenerServer(urls));
        }
    }
}
