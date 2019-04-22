using Microsoft.Extensions.Caching.Redis;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Maydear.Mvc.Caching.Redis
{
    public class RedisAccessTokenStore : IAccessTokenStore
    {
        private readonly RedisCache cache;
        private readonly RedisAccessTokenOptions accessTokenOptions;
        public RedisAccessTokenStore(IOptions<RedisAccessTokenOptions> options)
        {
            accessTokenOptions = options.Value;
            cache = new RedisCache(new RedisCacheOptions());
        }

        public Task RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task RenewAsync(string key, string tokenValue)
        {
            throw new NotImplementedException();
        }

        public Task<string> RetrieveAsync(string key)
        {
            throw new NotImplementedException();
        }

        public Task<string> StoreAsync(string tokenValue)
        {
            throw new NotImplementedException();
        }
    }
}
