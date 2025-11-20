using Store.G02.Domain.Contracts;
using Store.G02.Services.Abstractions.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G02.Services.Cache
{
    public class CacheService(ICacheRepository _cacheRepository) : ICacheService
    {
        public async Task<string?> GetAsync(string key)
        {
           var result = await _cacheRepository.GetAsync(key); 
            return result;
        }

        public async Task SetAsync(string key, object value, TimeSpan duration)
        {
           await _cacheRepository.SetAsync(key, value, duration);       
        }
    }
}
