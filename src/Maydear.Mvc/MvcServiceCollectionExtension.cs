using Maydear.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    public static class MvcServiceCollectionExtension
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaydearMvc(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            services.AddMvc(options =>
            {
                options.Filters.Add<ValidationActionFilter>();
                options.Filters.Add<PackageObjectExceptionFilter>();
                options.Filters.Add<PackageResultFilter>();
            });
            return services;
        }
    }
}
