using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Maydear.Mvc
{
    /// <summary>
    /// 自定义跨域处理服务，增加通配符二级域名策略
    /// </summary>
    /// <example>
    /// <code>
    /// public class Startup
    /// {
    ///     public Startup(IConfiguration configuration)
    ///     {
    ///         Configuration = configuration;
    ///     }
    /// 
    ///     private readonly string corsPolicyName = "_myAllowSpecificOrigins";
    /// 
    ///     public IConfiguration Configuration { get; }
    /// 
    ///     public void ConfigureServices(IServiceCollection services)
    ///     {
    /// 
    ///         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
    ///         services.AddWildcardCors(options =>
    ///         {
    ///             options.AddPolicy(corsPolicyName,
    ///             builder =>
    ///             {
    ///                 builder.WithOrigins("*.test.com");
    ///             });
    ///         });
    ///     }
    /// 
    ///     public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    ///     {
    ///         if (env.IsDevelopment())
    ///         {
    ///             app.UseDeveloperExceptionPage();
    ///         }
    /// 
    ///         app.UseCors(corsPolicyName);
    ///         app.UseMvc();
    ///     }
    /// }
    /// </code>
    /// </example>
    public class WildcardCorsService : CorsService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        /// <param name="loggerFactory"></param>
        public WildcardCorsService(IOptions<CorsOptions> options, ILoggerFactory loggerFactory)
           : base(options, loggerFactory) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="policy"></param>
        /// <param name="result"></param>
        public override void EvaluatePreflightRequest(HttpContext context, CorsPolicy policy, CorsResult result)
        {
            var origin = context.Request.Headers[CorsConstants.Origin];
            EvaluateOriginForWildcard(policy.Origins, origin);
            base.EvaluatePreflightRequest(context, policy, result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="policy"></param>
        /// <param name="result"></param>
        public override void EvaluateRequest(HttpContext context, CorsPolicy policy, CorsResult result)
        {
            var origin = context.Request.Headers[CorsConstants.Origin];
            EvaluateOriginForWildcard(policy.Origins, origin);
            base.EvaluateRequest(context, policy, result);
        }

        private void EvaluateOriginForWildcard(IList<string> origins, string origin)
        {
            //只在没有匹配的origin的情况下进行操作
            if (!origins.Contains(origin))
            {
                //查询所有以星号开头的origin （如果有多个通配符域名策略，每个都设置）
                var wildcardDomains = origins.Where(o => o.StartsWith("*"));
                if (wildcardDomains.Any())
                {
                    //遍历以星号开头的origin 
                    foreach (var wildcardDomain in wildcardDomains)
                    {
                        if (origin.EndsWith(wildcardDomain.Substring(1))
                            || origin.EndsWith("//" + wildcardDomain.Substring(2)))
                        {
                            origins.Add(origin);
                            break;
                        }
                    }
                }
            }
        }
    }
}
