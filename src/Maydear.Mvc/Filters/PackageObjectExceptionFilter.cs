using Maydear.Exceptions;
using Maydear.Mvc.Exceptions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maydear.Mvc.Filters
{
    /// <summary>
    /// 包对象过滤器
    /// </summary>
    public sealed class PackageObjectExceptionFilter : IExceptionFilter, IAsyncExceptionFilter
    {
        /// <summary>
        /// 主机环境设置
        /// </summary>

        private readonly IHostingEnvironment hostingEnvironment;
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="hostingEnvironment">环境变量配置</param>
        /// <param name="loggerFactory">日志工厂</param>
        public PackageObjectExceptionFilter(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            this.hostingEnvironment = hostingEnvironment;
            logger = loggerFactory.CreateLogger<PackageObjectExceptionFilter>();
        }

        /// <summary>
        /// 发生异常时执行
        /// </summary>
        /// <param name="context">异常上下文</param>
        public void OnException(ExceptionContext context)
        {
            Guid requestId = Guid.NewGuid();

            if (context.HttpContext.Request.Headers.ContainsKey(Constants.REQUEST_ID_HEADER_KEY))
            {
                requestId = Guid.Parse(context.HttpContext.Request.Headers[Constants.REQUEST_ID_HEADER_KEY]);
            }
            object result = null;
            if (context.ActionDescriptor is Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)
            {
                Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor controllerActionDescriptor = context.ActionDescriptor as Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor;

                if (controllerActionDescriptor?.MethodInfo.ReturnType == typeof(bool))
                {
                    result = default(bool);
                }
                if (controllerActionDescriptor?.MethodInfo.ReturnType == typeof(int))
                {
                    result = int.MinValue;
                }
                if (controllerActionDescriptor?.MethodInfo.ReturnType == typeof(long))
                {
                    result = long.MinValue;
                }
                if (controllerActionDescriptor?.MethodInfo.ReturnType == typeof(decimal))
                {
                    result = decimal.MinValue;
                }
            }
            if (requestId == Guid.Empty)
            {
                requestId = Guid.NewGuid();
            }
            if (context.Exception is StatusCodeException)
            {
                context.Result = BuidResult(context.Exception as StatusCodeException, requestId, result);
            }
            else
            {
                if (hostingEnvironment.IsDevelopment())
                {
                    return;
                }
                logger.LogError(context.Exception.Message);
                context.Result = BuidResult(new ErrorException(context.Exception), requestId, result);
            }
            //异常已处理
            context.ExceptionHandled = true;
        }

        /// <summary>
        /// 异步发生异常时执行
        /// </summary>
        /// <param name="context">异常上下文</param>
        /// <returns></returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            OnException(context);
            return Task.CompletedTask;
        }

        /// <summary>
        ///  构造标准输出对象
        /// </summary>
        /// <param name="exception">微服务异常基类</param>
        /// <param name="requestId"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        private PackageObjectResult BuidResult(StatusCodeException exception, Guid requestId, object result = null)
        {
            return new PackageObjectResult(new PackageObject()
            {
                StatusCode = exception.StatusCode,
                Body = result,
                RequestId = requestId,
                Notification = exception.Message,
                Now = DateTimeOffset.Now
            });
        }
    }
}
