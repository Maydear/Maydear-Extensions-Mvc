using Maydear.Mvc.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System;

namespace Microsoft.AspNetCore.Authentication
{
    /// <summary>
    /// Token扩展
    /// </summary>
    public static class MaydearAuthenticationExtension
    {
        /// <summary>
        /// 认证构造器builder上注册token认证
        /// </summary>
        /// <param name="builder">认证构造器</param>
        /// <returns>认证构造器AuthenticationBuilder</returns>
        public static AuthenticationBuilder AddMaydear(this AuthenticationBuilder builder)
        {
            return builder.AddMaydear(MaydearAuthenticationDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// 认证构造器builder上注册token认证
        /// </summary>
        /// <param name="builder">认证构造器</param>
        /// <param name="authenticationScheme">认证架构</param>
        /// <returns>认证构造器AuthenticationBuilder</returns>
        public static AuthenticationBuilder AddMaydear(this AuthenticationBuilder builder, string authenticationScheme)
        {
            return builder.AddMaydear(authenticationScheme, configureOptions: null);
        }

        /// <summary>
        /// 认证构造器builder上注册token认证
        /// </summary>
        /// <param name="builder">认证构造器</param>
        /// <param name="configureOptions">配置项</param>
        /// <returns>认证构造器AuthenticationBuilder</returns>
        public static AuthenticationBuilder AddMaydear(this AuthenticationBuilder builder, Action<MaydearAuthenticationOptions> configureOptions)
        {
            return builder.AddMaydear(MaydearAuthenticationDefaults.AuthenticationScheme, configureOptions);
        }

        /// <summary>
        /// 认证构造器builder上注册token认证
        /// </summary>
        /// <param name="builder">认证构造器</param>
        /// <param name="authenticationScheme">认证架构</param>
        /// <param name="configureOptions">配置项</param>
        /// <returns>认证构造器AuthenticationBuilder</returns>
        public static AuthenticationBuilder AddMaydear(this AuthenticationBuilder builder, string authenticationScheme, Action<MaydearAuthenticationOptions> configureOptions)
        {
            return builder.AddMaydear(authenticationScheme, displayName: null, configureOptions: configureOptions);
        }

        /// <summary>
        /// 认证构造器builder上注册token认证
        /// </summary>
        /// <param name="builder">认证构造器</param>
        /// <param name="authenticationScheme">认证架构</param>
        /// <param name="displayName">显示名称</param>
        /// <param name="configureOptions">配置项</param>
        /// <returns></returns>
        public static AuthenticationBuilder AddMaydear(this AuthenticationBuilder builder, string authenticationScheme, string displayName, Action<MaydearAuthenticationOptions> configureOptions)
        {
            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<IPostConfigureOptions<MaydearAuthenticationOptions>, PostConfigureTokenAuthenticationOptions>());
            return builder.AddScheme<MaydearAuthenticationOptions, MaydearAuthenticationHandler>(authenticationScheme, displayName, configureOptions);
        }
    }
}
