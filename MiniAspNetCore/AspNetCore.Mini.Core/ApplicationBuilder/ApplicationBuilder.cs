﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AspNetCore.Mini.Core
{
    public class ApplicationBuilder : IApplicationBuilder
    {
        private readonly List<Func<RequestDelegate, RequestDelegate>> _middlewares = new List<Func<RequestDelegate, RequestDelegate>>();

        /// <summary>
        /// 构建请求处理管道
        /// </summary>
        /// <returns></returns>
        public RequestDelegate Build()
        {
            _middlewares.Reverse(); //倒置注册中间件集合的顺序

            return httpContext => 
            {
                //注册默认中间件，返回404
                RequestDelegate next = _ =>
                {
                    _.Response.StatusCode = 404;
                    return Task.CompletedTask;
                };

                //构建中间件处理管道
                foreach (var middleware in _middlewares)
                {
                    next = middleware(next);
                }

                return next(httpContext);
            };
        }

        /// <summary>
        /// 注册中间件
        /// </summary>
        /// <param name="middleware"></param>
        /// <returns></returns>
        public IApplicationBuilder Use(Func<RequestDelegate, RequestDelegate> middleware)
        {
            _middlewares.Add(middleware);
            return this;
        }
    }
}
