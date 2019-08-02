using System;

namespace MiniWebServer
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer webServer = new WebServer();
            webServer.StartListen("127.0.0.1",1234);
            Console.ReadLine();
        }
    }
}
