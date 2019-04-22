using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;

namespace Maydear.Mvc.Authentication
{
    /// <summary>
    /// 令牌转递上下文
    /// </summary>
    public class ChallengeContext : PropertiesContext<MaydearAuthenticationOptions>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="scheme"></param>
        /// <param name="options"></param>
        /// <param name="properties"></param>
        public ChallengeContext(
            HttpContext context,
            AuthenticationScheme scheme,
            MaydearAuthenticationOptions options,
            AuthenticationProperties properties)
            : base(context, scheme, options, properties)
        { }

        /// <summary>
        /// 在身份验证过程中遇到的任何异常。
        /// </summary>
        public Exception AuthenticateFailure { get; set; }

        /// <summary>
        /// 如果为true，则将跳过任何默认逻辑。
        /// </summary>
        public bool Handled { get; private set; }
    }
}
