using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Maydear.Mvc.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class PackageResultFilter : IResultFilter, IAsyncResultFilter
    {
        /// <summary>
        /// Result执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuted(ResultExecutedContext context)
        {
        }

        /// <summary>
        /// Result执行中
        /// </summary>
        /// <param name="context"></param>
        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is OkObjectResult)
            {
                return;
            }

            if (context.Result is ObjectResult)
            {
                ObjectResult result = context.Result as ObjectResult;
                Guid requestId = Guid.NewGuid();
                if (context.HttpContext != null && 
                    context.HttpContext.Request != null && 
                    context.HttpContext.Request.Headers != null && 
                    context.HttpContext.Request.Headers.ContainsKey(Constants.REQUEST_ID_HEADER_KEY))
                {
                    if (!Guid.TryParse(context.HttpContext.Request.Headers[Constants.REQUEST_ID_HEADER_KEY], out requestId))
                    {
                        requestId = Guid.NewGuid();
                    }
                }
                //如果是空值则返回404异常
                if (result.Value == null)
                {
                    context.Result = new PackageObjectResult(new PackageObject()
                    {
                        StatusCode = (int)Mvc.StatusCode.NotFound,
                        Body = null,
                        RequestId = requestId,
                        Notification = Mvc.StatusCode.NotFound.GetDescription(),
                        Now = DateTimeOffset.Now
                    });
                }
                else
                {
                    PackageObjectResult packageObjectResult = new PackageObjectResult(result, requestId);
                    if (packageObjectResult.Value != null)
                    {
                        packageObjectResult.Value.RequestId = requestId;
                    }

                    context.Result = packageObjectResult;
                }
            }
        }

        /// <summary>
        /// 异步Result执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            OnResultExecuting(context);
            if (!context.Cancel)
            {
                OnResultExecuted(await next());
            }
        }
    }
}
