using StackExchange.Redis;
using Store.G02.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Store.G02.Persistence.Repository
{
    public class CacheRepository(IConnectionMultiplexer connection) : ICacheRepository
    {

        private readonly IDatabase  _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
          var redisValue = await  _database.StringGetAsync(key);
            return redisValue;

        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
           await _database.StringSetAsync(key,JsonSerializer.Serialize(value), duration);  
        }
    }
}
