using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Maydear.Mvc.Authentication
{
    /// <summary>
    /// 令牌认证服务上下文
    /// </summary>
    public class AccessTokenValidatedContext : ResultContext<MaydearAuthenticationOptions>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scheme"></param>
        /// <param name="options"></param>
        public AccessTokenValidatedContext(
          HttpContext context,
          AuthenticationScheme scheme,
          MaydearAuthenticationOptions options)
          : base(context, scheme, options)
        { }

        /// <summary>
        /// 令牌值
        /// </summary>
        public string AccessTokenValue { get; set; }
    }
}
