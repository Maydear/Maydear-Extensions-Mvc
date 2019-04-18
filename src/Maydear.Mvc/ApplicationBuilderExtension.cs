using Maydear.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Mvc
{
   public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用云丝尚微服务构造器
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSeeShionMicroService(this IApplicationBuilder app)
        {
            app.UseMiddleware<TimeConsumingMiddlerware>();

            return app;
        }
    }
}
