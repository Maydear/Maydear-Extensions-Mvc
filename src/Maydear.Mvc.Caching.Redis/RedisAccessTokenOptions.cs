using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maydear.Mvc.Caching.Redis
{
    /// <summary>
    /// Redis配置
    /// </summary>
    public class RedisAccessTokenOptions : AccessTokenOptions,IOptions<RedisAccessTokenOptions>
    {
        /// <summary>
        /// Redis配置
        /// </summary>
        public string Configuration { get; set; }

        RedisAccessTokenOptions IOptions<RedisAccessTokenOptions>.Value => this;
    }
}
