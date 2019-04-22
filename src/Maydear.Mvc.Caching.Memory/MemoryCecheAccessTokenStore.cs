using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Maydear.Mvc.Caching.Memory
{
    public class MemoryCecheAccessTokenStore : IAccessTokenStore
    {
        private const string KeyPrefix = "AuthSessionStore-";
        private readonly IMemoryCache cache;
        private readonly AccessTokenOptions accessTokenOptions;

        public MemoryCecheAccessTokenStore(IOptions<AccessTokenOptions> accessTokenOptions)
        {
            cache = new MemoryCache(new MemoryCacheOptions());
            this.accessTokenOptions = accessTokenOptions.Value;
        }

        public Task RemoveAsync(string key)
        {
            cache.Remove($"{KeyPrefix}{key}");
            return Task.FromResult(0);
        }

        public Task RenewAsync(string key, string tokenValue)
        {
            MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
            options.SetSlidingExpiration(TimeSpan.FromSeconds(accessTokenOptions.Expires)); // TODO: configurable.
            cache.Set($"{KeyPrefix}{key}", tokenValue, options);

            return Task.FromResult(0);

        }

        public Task<string> RetrieveAsync(string key)
        {
            cache.TryGetValue($"{KeyPrefix}{key}", out string accessTokenValue);
            return Task.FromResult(accessTokenValue);
        }

        public async Task<string> StoreAsync(string tokenValue)
        {
            var guid = Guid.NewGuid().ToString("N");
            await RenewAsync($"{KeyPrefix}{guid}", tokenValue);
            return guid;

        }
    }
}
