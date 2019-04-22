using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

namespace Maydear.Mvc.Authentication
{
    /// <summary>
    /// 认证失败上下文
    /// </summary>
    public class AuthenticationFailedContext : ResultContext<MaydearAuthenticationOptions>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="scheme">认证架构</param>
        /// <param name="options">认证选项</param>
        public AuthenticationFailedContext(
            HttpContext context,
            AuthenticationScheme scheme,
            MaydearAuthenticationOptions options)
            : base(context, scheme, options)
        { }

        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception Exception { get; set; }
    }
}
