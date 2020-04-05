using Maydear.Mvc;
using Maydear.Mvc.Authentication;
using Maydear.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 添加mvc
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaydearMvc(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = MaydearAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = MaydearAuthenticationDefaults.AuthenticationScheme;
            }).AddMaydear();
            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationActionFilter>();
                options.Filters.Add<PackageObjectExceptionFilter>();
                options.Filters.Add<PackageResultFilter>();
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            return services;
        }

        /// <summary>
        /// 添加通配符跨域
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddWildcardCors(this IServiceCollection services, Action<CorsOptions> options)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            services.Configure(options);
            services.AddTransient<ICorsService, WildcardCorsService>();
            return services;
        }
    }
}
