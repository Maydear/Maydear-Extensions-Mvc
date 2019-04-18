using Maydear.Mvc.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maydear.Mvc.Filters
{
    /// <summary>
    /// 校验过滤器
    /// </summary>
    public sealed class ValidationActionFilter : IActionFilter, IAsyncActionFilter
    {
        /// <summary>
        /// 活动执行后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        /// <summary>
        /// Action执行中是触发
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                List<Notification> errors = new List<Notification>();
                foreach (KeyValuePair<string, ModelStateEntry> item in context.ModelState)
                {
                    if (item.Value.ValidationState == ModelValidationState.Invalid || item.Value.Errors.Count > 0)
                    {
                        errors.Add(new Notification() { FieldsName = item.Key, ErrorsMessage = item.Value.Errors.Select(a => a.ErrorMessage) });
                    }
                }
                throw new ValidFailureException($"[{string.Join(",", errors.Select(a => a.ToString()))}]");
            }
        }

        /// <summary>
        /// 异步Action执行中是触发
        /// </summary>
        /// <param name="context"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        public async Task OnActionExecutionAsync(
            ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (next == null)
            {
                throw new ArgumentNullException(nameof(next));
            }

            OnActionExecuting(context);
            if (context.Result == null)
            {
                OnActionExecuted(await next());
            }
        }

        internal class Notification
        {
            public string FieldsName { get; set; }
            public IEnumerable<string> ErrorsMessage { get; set; }

            public override string ToString()
            {
                return $"{{\"{FieldsName}\":\"{string.Join(",", ErrorsMessage)}\"}}";
            }
        }
    }
}
