using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maydear.Mvc
{
    /// <summary>
    /// 上下文扩展方法
    /// </summary>
    public static class HttpContextExtension
    {
        /// <summary>
        /// 获取用户的Ip地址。
        /// </summary>
        /// <param name="context">Http上下文。</param>
        /// <returns>返回客户端ip地址</returns>
        public static string GetUserIpAddress(this HttpContext context)
        {
            string ip = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (string.IsNullOrEmpty(ip))
            {
                ip = context.Connection.RemoteIpAddress.ToString();
            }
            if (ip.Any(a => a == ','))
            {
                return ip.Substring(0, ip.IndexOf(","));
            }

            return ip;
        }
    }
}
