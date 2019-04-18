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
        /// 请求号
        /// </summary>
        private const string REQUESTID_HEADER_KEY = "RequestId";

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        public TimeConsumingMiddlerware(RequestDelegate next,ILoggerFactory loggerFactory)
        {
            this.next = next;
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
            if (context.Request.Headers != null && !context.Request.Headers.ContainsKey(REQUESTID_HEADER_KEY))
            {
                context.Request.Headers.Add(REQUESTID_HEADER_KEY, Guid.NewGuid().ToString());
            }
            logger.LogInformation($"Start Request({context.Request.Headers[REQUESTID_HEADER_KEY]}) --> [{context.Request.Method}][{context.Request.Path}]");
            await next.Invoke(context);
            stopwatch.Stop();
            logger.LogInformation($"End Request({context.Request.Headers[REQUESTID_HEADER_KEY]}) --> [{context.Request.Method}][{context.Request.Path}][{stopwatch.Elapsed.TotalSeconds}ms]");
        }
    }
}
