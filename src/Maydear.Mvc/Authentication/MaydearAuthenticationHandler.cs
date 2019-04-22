using Maydear.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Maydear.Mvc.Authentication
{
    /// <summary>
    /// Token认证处理器
    /// </summary>
    public class MaydearAuthenticationHandler :
        AuthenticationHandler<MaydearAuthenticationOptions>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="options"></param>
        /// <param name="logger"></param>
        /// <param name="encoder"></param>
        /// <param name="clock"></param>
        public MaydearAuthenticationHandler(
            IOptionsMonitor<MaydearAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        { }

        /// <summary>
        /// 处理事件
        /// </summary>
        protected new MaydearEvents Events
        {
            get => (MaydearEvents)base.Events;
            set => base.Events = value;
        }

        /// <summary>
        /// 创建事件
        /// </summary>
        /// <returns></returns>
        protected override Task<object> CreateEventsAsync()
        {
            return Task.FromResult<object>(new MaydearEvents());
        }

        /// <summary>
        /// 处理认证
        /// </summary>
        /// <returns></returns>
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return AuthenticateResult.NoResult();
            }

            string authorizationString = Request.Headers[HeaderNames.Authorization];
            //Authorization 的值不能为null或Empty
            if (string.IsNullOrEmpty(authorizationString))
            {
                return AuthenticateResult.NoResult();
            }

            ///
            string[] authorizationDir = authorizationString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            //Authorization 的值不是有效的格式
            if (authorizationDir.Length != 2)
            {
                return AuthenticateResult.NoResult();
            }

            if (authorizationDir[0].Equals(MaydearAuthenticationDefaults.AuthenticationScheme, StringComparison.OrdinalIgnoreCase))
            {
                return await AccessTokenAuthenticate(authorizationDir[1]);
            }

            return AuthenticateResult.NoResult();
        }

        /// <summary>
        /// 认证失败
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
        {
            AuthenticateResult authResult = await HandleAuthenticateOnceSafeAsync();
            ChallengeContext eventContext = new ChallengeContext(Context, Scheme, Options, properties)
            {
                AuthenticateFailure = authResult?.Failure
            };

            // Avoid returning error=invalid_token if the error is not caused by an authentication failure (e.g missing token).


            await Events.Challenge(eventContext);
            if (eventContext.Handled)
            {
                return;
            }

            Response.StatusCode = 401;
            Response.Headers.Append(HeaderNames.WWWAuthenticate, Options.Challenge);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessTokenKey"></param>
        /// <returns></returns>
        private async Task<AuthenticateResult> AccessTokenAuthenticate(string accessTokenKey)
        {
            try
            {
                if (string.IsNullOrEmpty(accessTokenKey))
                {
                    return AuthenticateResult.NoResult();
                }

                if (Options.AccessTokenStore == null)
                {
                    return AuthenticateResult.Fail($"No {nameof(Options.AccessTokenStore)}");
                }
                string accessTokenValue = await Options.AccessTokenStore.RetrieveAsync(accessTokenKey);
                if (string.IsNullOrWhiteSpace(accessTokenValue))
                {
                    ClaimsPrincipal principal = new ClaimsPrincipal(
                                             new ClaimsIdentity(
                                                 new[] {
                                                     new Claim("AccessToken", accessTokenKey, ClaimValueTypes.String, MaydearAuthenticationDefaults.ClaimsIssuer),
                                                     new Claim("AccessTokenValue", accessTokenValue, ClaimValueTypes.Integer64, MaydearAuthenticationDefaults.ClaimsIssuer)
                                                 },
                                                 MaydearAuthenticationDefaults.AuthenticationScheme
                     ));

                    Logger.MaydearValidationSucceeded();

                    AccessTokenValidatedContext validatedContext = new AccessTokenValidatedContext(Context, Scheme, Options)
                    {
                        Principal = principal,
                        AccessTokenValue = accessTokenValue
                    };

                    await Events.TokenValidated(validatedContext);
                    if (validatedContext.Result != null)
                    {
                        return validatedContext.Result;
                    }

                    validatedContext.Success();
                    return validatedContext.Result;
                }

                return AuthenticateResult.NoResult();
            }
            catch (Exception ex)
            {
                Logger.ErrorProcessingMessage(ex);

                AuthenticationFailedContext authenticationFailedContext = new AuthenticationFailedContext(Context, Scheme, Options)
                {
                    Exception = ex
                };

                await Events.AuthenticationFailed(authenticationFailedContext);
                if (authenticationFailedContext.Result != null)
                {
                    return authenticationFailedContext.Result;
                }

                throw;
            }
        }
    }
}
