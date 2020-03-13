using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Maydear.Mvc.Caching.Memory
{
    /// <summary>
    /// 内存缓存式访问令牌存储
    /// </summary>
    public class MemoryCecheAccessTokenStore : IAccessTokenStore
    {
        private const string KeyPrefix = "AuthSessionStore-";
        private readonly IMemoryCache cache;
        private readonly AccessTokenOptions accessTokenOptions;

        /// <summary>
        /// 构造内存缓存式访问令牌存储
        /// </summary>
        /// <param name="memoryCache"></param>
        /// <param name="accessTokenOptions"></param>
        public MemoryCecheAccessTokenStore(IMemoryCache memoryCache,IOptions<AccessTokenOptions> accessTokenOptions)
        {
            cache = memoryCache;
            this.accessTokenOptions = accessTokenOptions.Value;
        }

        public Task<string> GetAsync(string key)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task RemoveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
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
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrWhiteSpace(tokenValue))
            {
                throw new ArgumentNullException(nameof(tokenValue));
            }
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(accessTokenOptions.Expires)); // TODO: configurable.
            cache.Set($"{KeyPrefix}{key}", tokenValue, options);
            return Task.FromResult(0);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Task<string> RetrieveAsync(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            cache.TryGetValue($"{KeyPrefix}{key}", out string accessTokenValue);
            return Task.FromResult(accessTokenValue);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenValue"></param>
        /// <returns></returns>
        public async Task<string> StoreAsync(string tokenValue)
        {
            if (string.IsNullOrWhiteSpace(tokenValue))
            {
                throw new ArgumentNullException(nameof(tokenValue));
            }
            var guid = Guid.NewGuid().ToString("N");
            await RenewAsync($"{guid}", tokenValue);
            return guid;

        }
    }
}
