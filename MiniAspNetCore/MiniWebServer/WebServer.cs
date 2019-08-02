using MiniWebServer.Context;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System;

namespace MiniWebServer
{
    public class WebServer
    {
        private Socket _socketWatch;

        private Thread _threadWatch;

        private bool _isEndService;

        public void StartListen(string ip, int port)
        {
            //创建Socket=>绑定IP和端口=>设置监听队列的长度=>开启监听连接
            _socketWatch = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socketWatch.Bind(new IPEndPoint(IPAddress.Parse(ip), port));
            _socketWatch.Listen(10);

            //创建Thread->后台执行
            _threadWatch = new Thread(ListenClientConnect);
            _threadWatch.IsBackground = true;
            _threadWatch.Start(_socketWatch);

            _isEndService = false;
            Console.WriteLine("~_~[消息]：您已经成功开启服务！");
        }

        private void ListenClientConnect(object obj)
        {
            Socket socketListen = obj as Socket;

            while (!_isEndService)
            {
                Socket proxySocket = socketListen.Accept();
                byte[] data = new byte[1024 * 1024 * 2];
                int length = proxySocket.Receive(data,0,data.Length,SocketFlags.None);

                //1.接收Http请求
                var requestText = Encoding.Default.GetString(data,0,length);
                HttpContext context = new HttpContext(requestText);

                //2.处理http请求
                var application = new HttpApplication();
                application.ProcessRequest(context);
                Console.WriteLine("{0} {1} from {2}",context.Request.HttpMethod,context.Request.Url,proxySocket.RemoteEndPoint.ToString());

                //3.响应http请求
                proxySocket.Send(context.Response.GetResponseHeader());
                proxySocket.Send(context.Response.Body);

                //4.即时关闭socket连接
                proxySocket.Shutdown(SocketShutdown.Both);
                proxySocket.Close();
            }
        }
    }
}
