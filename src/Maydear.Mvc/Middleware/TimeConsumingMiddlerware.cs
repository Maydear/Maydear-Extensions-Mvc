using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maydear.Mvc.Middleware
{
    /// <summary>
    /// 耗时日志
    /// </summary>
    public sealed class TimeConsumingMiddlerware
    {
        /// <summary>
        /// 请求委托
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        public TimeConsumingMiddlerware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }

            this.next = next ?? throw new ArgumentNullException(nameof(next));
            this.logger = loggerFactory.CreateLogger<TimeConsumingMiddlerware>();
        }

        /// <summary>
        /// 出发调用
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            stopwatch.Start();
            if (context.Request.Headers != null && !context.Request.Headers.ContainsKey(Constants.REQUEST_ID_HEADER_KEY))
            {
                context.Request.Headers.Add(Constants.REQUEST_ID_HEADER_KEY, Guid.NewGuid().ToString());
            }
            logger.LogInformation($"Request({context.Request.Headers[Constants.REQUEST_ID_HEADER_KEY]}) --> {context.Request.Method} {Url(context.Request)} ");
            await next.Invoke(context);
            stopwatch.Stop();
            logger.LogInformation($"Request finish ({context.Request.Headers[Constants.REQUEST_ID_HEADER_KEY]}) -->  {context.Request.Method} {Url(context.Request)} {stopwatch.Elapsed.TotalMilliseconds.ToString("F2")}ms");
        }

        private string Url(HttpRequest request)
        {
           return new StringBuilder()
                .Append(request.PathBase)
                .Append(request.Path)
                .Append(request.QueryString)
                .ToString();
        }

    }
}
