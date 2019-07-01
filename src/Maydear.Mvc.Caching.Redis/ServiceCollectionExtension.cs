using Maydear.Mvc;
using Maydear.Mvc.Caching.Redis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
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
        /// 默认过期时间（秒）
        /// </summary>
        private const long DefaultExpires = 3600;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaydearRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }
            return services.AddMaydearRedisCache(options =>
            {
                options.Expires = (configuration.GetSection("AccessToken:Expires")?.Value).AsLongOrDefault(DefaultExpires);
                options.Configuration = configuration.GetSection("AccessToken:Configuration")?.Value;
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="setupAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddMaydearRedisCache(this IServiceCollection services, Action<RedisAccessTokenOptions> setupAction)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (setupAction == null)
            {
                throw new ArgumentNullException(nameof(setupAction));
            }
            services.AddOptions();
            services.Configure(setupAction);
            services.AddMemoryCache();
            services.TryAddTransient<IAccessTokenStore, RedisAccessTokenStore>();
            return services;
        }
    }
}
