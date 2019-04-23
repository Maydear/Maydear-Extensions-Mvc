using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Maydear.Mvc.Caching.Redis
{
    /// <summary>
    /// Redis分布式缓存式访问令牌存储
    /// </summary>
    public class RedisAccessTokenStore : IAccessTokenStore
    {
        private const string KeyPrefix = "AccessToken:";
        private readonly RedisCache cache;
        private readonly RedisAccessTokenOptions accessTokenOptions;

        /// <summary>
        /// 构建Redis分布式缓存式访问令牌存储
        /// </summary>
        /// <param name="options">配置选项信息</param>
        public RedisAccessTokenStore(IOptions<RedisAccessTokenOptions> options)
        {
            accessTokenOptions = options.Value;
            cache = new RedisCache(new RedisCacheOptions()
            {
                Configuration = accessTokenOptions.Configuration
            });
        }

        /// <summary>
        /// 移除令牌
        /// </summary>
        /// <param name="key">访问令牌</param>
        /// <returns></returns>
        public Task RemoveAsync(string key)
        {
            cache.Remove($"{KeyPrefix}{key}");
            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="tokenValue"></param>
        /// <returns></returns>
        public Task RenewAsync(string key, string tokenValue)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(accessTokenOptions.Expires)); // TODO: configurable.
            return cache.SetAsync($"{KeyPrefix}{key}", tokenValue.ToBytes());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<string> RetrieveAsync(string key)
        {
            string accessTokenValue = cache.Get($"{KeyPrefix}{key}").ToTextString();
            cache.Refresh($"{KeyPrefix}{key}");
            return Task.FromResult(accessTokenValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenValue"></param>
        /// <returns></returns>
        public async Task<string> StoreAsync(string tokenValue)
        {
            string guid = Guid.NewGuid().ToString("N");
            await RenewAsync($"{KeyPrefix}{guid}", tokenValue);
            return guid;
        }
    }
}
