using System;
using System.Collections.Generic;

namespace AspNetCore.Mini.Core
{
    public class WebHostBuilder : IWebHostBuilder
    {
        private IServer _server;

        private readonly List<Action<IApplicationBuilder>> _configures = new List<Action<IApplicationBuilder>>();

        /// <summary>
        /// 构建具体WebHost
        /// </summary>
        /// <returns></returns>
        public IWebHost Build()
        {
            var builder = new ApplicationBuilder();
            foreach (var configure in _configures)
            {
                configure(builder);
            }

            return new WebHost(_server, builder.Build());
        }

        /// <summary>
        /// 配置中间件
        /// </summary>
        /// <param name="configure"></param>
        /// <returns></returns>
        public IWebHostBuilder Configure(Action<IApplicationBuilder> configure)
        {
            _configures.Add(configure);
            return this;
        }

        /// <summary>
        /// 指定具体要使用的server
        /// </summary>
        /// <param name="server"></param>
        /// <returns></returns>
        public IWebHostBuilder UseServer(IServer server)
        {
            _server = server;
            return this;
        }
    }
}
