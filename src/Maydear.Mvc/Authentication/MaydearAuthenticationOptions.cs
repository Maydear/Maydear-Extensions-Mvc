using Maydear.Mvc;
using Microsoft.AspNetCore.Authentication;
using System;

namespace Maydear.Mvc.Authentication
{
    /// <summary>
    /// 令牌认证项
    /// </summary>
    public class MaydearAuthenticationOptions : AuthenticationSchemeOptions
    {
        /// <summary>
        /// 跳过架构
        /// </summary>
        public string Challenge { get; set; } = MaydearAuthenticationDefaults.AuthenticationScheme;

        /// <summary>
        /// 访问令牌的仓储
        /// </summary>
        public IAccessTokenStore AccessTokenStore { get; set; }

        /// <summary>
        /// 声明发行方
        /// </summary>
        public new string ClaimsIssuer { get; set; } = MaydearAuthenticationDefaults.ClaimsIssuer;

        /// <summary>
        /// 生存时间
        /// </summary>
        public TimeSpan Expires { get; set; }
    }
}
