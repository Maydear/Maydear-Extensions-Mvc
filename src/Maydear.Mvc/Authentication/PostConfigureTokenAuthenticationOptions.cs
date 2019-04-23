using Maydear.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace Maydear.Mvc.Authentication
{
    /// <summary>
    /// 加载配置令牌认证项
    /// </summary>
    public class PostConfigureTokenAuthenticationOptions : IPostConfigureOptions<MaydearAuthenticationOptions>
    {
        /// <summary>
        /// 令牌服务
        /// </summary>
        private readonly IAccessTokenStore accessTokenStore;

        /// <summary>
        /// 
        /// </summary>
        private readonly AccessTokenOptions accessTokenOptions;

        /// <summary>
        /// 构造函数
        /// </summary>
        public PostConfigureTokenAuthenticationOptions(IAccessTokenStore accessTokenStore, IOptions<AccessTokenOptions> accessTokenOptions)
        {
            if (accessTokenOptions == null)
            {
                throw new ArgumentNullException(nameof(accessTokenOptions));
            }

            this.accessTokenStore = accessTokenStore ?? throw new ArgumentNullException(nameof(accessTokenStore));
            this.accessTokenOptions = accessTokenOptions.Value;
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="options">配置项</param>
        public void PostConfigure(string name, MaydearAuthenticationOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (options.AccessTokenStore == null)
            {
                options.AccessTokenStore = accessTokenStore;
                options.Expires = TimeSpan.FromSeconds(accessTokenOptions.Expires);
            }
        }
    }
}
