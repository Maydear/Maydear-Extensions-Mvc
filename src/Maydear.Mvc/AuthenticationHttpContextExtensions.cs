using Maydear.Mvc.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Maydear.Mvc
{
    /// <summary>
    /// 授权相关的Http扩展
    /// </summary>
    public static class AuthenticationHttpContextExtensions
    {
        /// <summary>
        /// 异步执行用户登录
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="accessTokenValue">访问令牌值</param>
        /// <returns></returns>
        public static Task<string> SignInAsync(this HttpContext context, string accessTokenValue)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }
            if (accessTokenValue.IsNullOrWhiteSpace())
            {
                throw new ArgumentNullException(nameof(accessTokenValue));
            }
            if (context.RequestServices == null)
            {
                throw new MissingMemberException("HttpContext.RequestServices is null");
            }

            IAccessTokenStore accessTokenStore = context.RequestServices.GetRequiredService<IAccessTokenStore>();
            return accessTokenStore.StoreAsync(accessTokenValue);
        }

        /// <summary>
        /// 执行用户登录
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="accessTokenValue">访问令牌值</param>
        /// <returns></returns>
        public static string SignIn(this HttpContext context, string accessTokenValue)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.RequestServices == null)
            {
                throw new MissingMemberException("HttpContext.RequestServices is null");
            }

            IAccessTokenStore accessTokenStore = context.RequestServices.GetRequiredService<IAccessTokenStore>();
            return accessTokenStore.StoreAsync(accessTokenValue).Result;
        }

        /// <summary>
        /// 异步执行用户登录
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="accessTokenObject">访问令牌对象</param>
        /// <typeparam name="T">待序列化的对象</typeparam>
        /// <returns></returns>
        public static Task<string> SignInAsync<T>(this HttpContext context, T accessTokenObject)
        {
            string accessTokenValue = Newtonsoft.Json.JsonConvert.SerializeObject(accessTokenObject);
            return SignInAsync(context, accessTokenValue);
        }

        /// <summary>
        /// 异步执行用户登出
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public static async Task SignOutAsync(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.RequestServices == null)
            {
                throw new MissingMemberException("HttpContext.RequestServices is null");
            }

            IAccessTokenStore accessTokenStore = context.RequestServices.GetRequiredService<IAccessTokenStore>();
            string accessTokenKey = GetAccessToken(context);
            if (string.IsNullOrEmpty(accessTokenKey))
            {
                return;
            }

            await accessTokenStore.RemoveAsync(accessTokenKey);
        }

        /// <summary>
        /// 异步刷新token内容
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <param name="accessTokenObject">访问令牌对象</param>
        /// <returns></returns>
        public static async Task RefreshTokenValueAsync<T>(this HttpContext context, T accessTokenObject)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.RequestServices == null)
            {
                throw new MissingMemberException("HttpContext.RequestServices is null");
            }

            IAccessTokenStore accessTokenStore = context.RequestServices.GetRequiredService<IAccessTokenStore>();
            string accessTokenKey = GetAccessToken(context);
            if (string.IsNullOrEmpty(accessTokenKey))
            {
                return;
            }
            string accessTokenValue = Newtonsoft.Json.JsonConvert.SerializeObject(accessTokenObject);
            await accessTokenStore.RenewAsync(accessTokenKey, accessTokenValue);
        }

        /// <summary>
        /// 执行用户登出
        /// </summary>
        /// <param name="context">Http上下文</param>
        public static void SignOut(this HttpContext context)
        {
            SignOutAsync(context).Wait();
        }

        /// <summary>
        /// 获取当前令牌的访问信息
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public static Task<string> GetAccessTokenValueAsync(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.RequestServices == null)
            {
                throw new MissingMemberException("HttpContext.RequestServices is null");
            }

            IAccessTokenStore accessTokenStore = context.RequestServices.GetRequiredService<IAccessTokenStore>();
            string accessTokenKey = GetAccessToken(context);

            if (string.IsNullOrEmpty(accessTokenKey))
            {
                throw new UnAuthorizedException();
            }
            var accessTokenValue = accessTokenStore.RetrieveAsync(accessTokenKey);

            return accessTokenValue;
        }

        /// <summary>
        /// 获取访问令牌对象
        /// </summary>
        /// <typeparam name="T">泛型对象必须和<see cref="SignInAsync{T}(HttpContext, T)"/>配对使用</typeparam>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public static async Task<T> GetAccessTokenObjectAsync<T>(this HttpContext context)
        {
            string accessTokenValue = await GetAccessTokenValueAsync(context);

            if (string.IsNullOrEmpty(accessTokenValue))
            {
                throw new UnAuthorizedException();
            }

            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(accessTokenValue);
            }
            catch (Exception ex)
            {
                throw new ForbiddenException(ex);
            }
        }
        /// <summary>
        /// 获取请求的Header中Authorization的值
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        internal static string GetAuthorization(this HttpContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (context.Request == null)
            {
                throw new ArgumentNullException(nameof(context.Request));
            }

            return context.Request.Headers["Authorization"];
        }

        /// <summary>
        /// 获取请求的AccessToken值
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        public static string GetAccessToken(this HttpContext context)
        {
            string authorizationValue = GetAuthorization(context);

            if (string.IsNullOrEmpty(authorizationValue))
            {
                return null;
            }
            string[] authorizationValueSplit = authorizationValue.Split(' ');
            if (authorizationValueSplit.Length != 2)
            {
                return null;
            }

            return authorizationValueSplit[1];
        }
    }
}
