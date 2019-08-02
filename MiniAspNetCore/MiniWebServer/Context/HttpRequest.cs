using System.Collections.Generic;

namespace MiniWebServer.Context
{
    public class HttpRequest
    {
        /// <summary>
        /// 请求方式
        /// </summary>
        public string HttpMethod { get; set; }

        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Http协议版本
        /// </summary>
        public string HttpVersion { get; set; }

        /// <summary>
        /// 请求头
        /// </summary>
        public Dictionary<string,string> HeaderDictionary { get; set; }

        /// <summary>
        /// 请求体
        /// </summary>
        public Dictionary<string,string> BodyDictionary { get; set; }

        public HttpRequest(string requestText)
        {
            var lines = requestText.Replace("\r\n", "\r").Split('\r');
            var requestLines = lines[0].Split(' ');

            //获取Http请求方式，请求的url地址，http协议版本
            HttpMethod = requestLines[0];
            Url = requestLines[1];
            HttpVersion = requestLines[2];
        }
    }
}
