using System;
using System.Text;

namespace MiniWebServer.Context
{
    public class HttpResponse
    {
        /// <summary>
        /// 响应状态码
        /// </summary>
        public string StatusCode { get; set; }

        /// <summary>
        /// 响应状态描述
        /// </summary>
        public string StateDescription { get; set; }

        /// <summary>
        /// 响应内容描述
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// 响应报文的正文内容
        /// </summary>
        public byte[] Body { get; set; }

        //生成响应头信息
        public byte[] GetResponseHeader()
        {
            var strRequestHeader = string.Format(@"HTTP/1.1 {0} {1}
Content-Type: {2}
Accept-Ranges: bytes
Server: Microsoft-IIS/7.5
x-Powered-By: ASP.NET
Date: {3}
Content-Length: {4}",StatusCode,StateDescription,ContentType,string.Format("{0:R}",DateTime.Now,Body.Length));

            return Encoding.UTF8.GetBytes(strRequestHeader);
        }
    }
}
