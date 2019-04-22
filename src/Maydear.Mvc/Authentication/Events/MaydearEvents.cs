using System;
using System.Threading.Tasks;

namespace Maydear.Mvc.Authentication
{
    /// <summary>
    /// Token认证事件
    /// </summary>
    public class MaydearEvents
    {
        #region AuthenticationFaile
        /// <summary>
        /// Token认证失败事件
        /// </summary>
        public Func<AuthenticationFailedContext, Task> OnAuthenticationFailed { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Token认证失败事件
        /// </summary>
        /// <param name="context">Token认证失败上下文</param>
        /// <returns></returns>
        public virtual Task AuthenticationFailed(AuthenticationFailedContext context)
        {
            return OnAuthenticationFailed(context);
        }

        #endregion

        #region AccessTokenValidated

        /// <summary>
        /// Token认证校验事件
        /// </summary>
        public Func<AccessTokenValidatedContext, Task> OnTokenValidated { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Token认证校验事件
        /// </summary>
        /// <param name="context">Token认证校验上下文</param>
        /// <returns></returns>
        public virtual Task TokenValidated(AccessTokenValidatedContext context)
        {
            return OnTokenValidated(context);
        }

        #endregion

        #region Challenge
        /// <summary>
        /// Token认证转发事件
        /// </summary>
        public Func<ChallengeContext, Task> OnChallenge { get; set; } = context => Task.CompletedTask;

        /// <summary>
        /// Token认证转发事件
        /// </summary>
        /// <param name="context">Token认证转发上下文</param>
        /// <returns></returns>
        public virtual Task Challenge(ChallengeContext context)
        {
            return OnChallenge(context);
        }

        #endregion
    }
}
