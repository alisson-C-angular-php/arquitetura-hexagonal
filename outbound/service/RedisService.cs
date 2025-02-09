namespace crudBack.outbound.service
{
    using StackExchange.Redis;
    using System;
    using System.Threading.Tasks;

    public class RedisService
    {
        private readonly ConnectionMultiplexer _redis;

        public RedisService(IConfiguration configuration)
        {
            var host = configuration["Redis:Host"];
            var port = configuration["Redis:Port"];
            _redis = ConnectionMultiplexer.Connect($"{host}:{port}");
        }

        public async Task<string> GetFromCache(string key)
        {
            var db = _redis.GetDatabase();
            return await db.StringGetAsync(key);
        }

        public async Task SetToCache(string key, string value, TimeSpan expiry)
        {
            var db = _redis.GetDatabase();
            await db.StringSetAsync(key, value, expiry);
        }

        public async Task RemoveFromCache(string key)
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync(key);
        }
    }
}
