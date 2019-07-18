using Maydear.Mvc.Authentication;
using Maydear.Mvc.Filters;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        /// 
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
    }
}
