using System;
using System.IO;
using System.Reflection;

namespace MiniWebServer.Context
{
    public class HttpApplication : IHttpHandler
    {
        /// <summary>
        /// 对请求上下文进行处理
        /// </summary>
        /// <param name="context"></param>
        public void ProcessRequest(HttpContext context)
        {
            //1.获取网站根路径
            var basePath = AppDomain.CurrentDomain.BaseDirectory;
            var fileName = Path.Combine(basePath + "\\MyWebSite",context.Request.Url.TrimStart('/'));
            var fileExtension = Path.GetExtension(context.Request.Url);

            //2.处理动态文件请求
            if(".aspx".Equals(fileExtension) || ".ashx".Equals(fileExtension))
            {
                var className = Path.GetFileNameWithoutExtension(context.Request.Url);
                var currentAssemblyName = Assembly.GetExecutingAssembly().GetName();
                var handler = Assembly.Load(currentAssemblyName).CreateInstance(currentAssemblyName + ".Page" + className) as IHttpHandler;
                handler.ProcessRequest(context);
                return;
            }

            //3.处理静态文件请求
            if (!File.Exists(fileName))
            {
                context.Response.StatusCode = "404";
                context.Response.StateDescription = "Not Found";
                context.Response.ContentType = "text/html";

                var notExistHtml = Path.Combine(basePath,@"MyWebSite\notfound.html");
                context.Response.Body = File.ReadAllBytes(notExistHtml);
            }
            else
            {
                context.Response.StatusCode = "200";
                context.Response.StateDescription = "OK";
                context.Response.ContentType = Path.Combine(Path.GetExtension(context.Request.Url));
                context.Response.Body = File.ReadAllBytes(fileName);
            }
        }

        public string GetContentType(string fileExtension)
        {
            var type = "text/html; charset=UTF-8";
            switch (fileExtension)
            {
                case ".aspx":
                case ".html":
                case ".htm":
                    type = "text/html;charset=UTF-8";
                    break;
                case ".png":
                    type = "image/png";
                    break;
                case ".gif":
                    type = "image/gif";
                    break;
                case ".jpg":
                case ".jpeg":
                    type = "image/jpeg";
                    break;
                case ".css":
                    type = "text/css";
                    break;
                case ".js":
                    type = "application/x-javascript";
                    break;
                default:
                    type = "text/plain; charset=gbk";
                    break;
            }

            return type;
        }
    }
}
