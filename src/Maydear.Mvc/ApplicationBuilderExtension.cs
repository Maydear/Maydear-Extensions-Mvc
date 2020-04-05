using Maydear.Mvc.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationBuilderExtension
    {
        /// <summary>
        /// 使用云丝尚微服务构造器
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMaydearMvc(this IApplicationBuilder app)
        {
            return app.UseMaydearMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Default}/{action=index}");
            });
        }

        /// <summary>
        /// 使用云丝尚微服务构造器
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configureRoutes"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseMaydearMvc(this IApplicationBuilder app, Action<IRouteBuilder> configureRoutes)
        {
            app.UseAuthentication();
            app.UseMiddleware<TimeConsumingMiddlerware>();
            app.UseMvc(configureRoutes);
            return app;
        }
    }
}
