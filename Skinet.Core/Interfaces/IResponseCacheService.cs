using System;
using System.Threading.Tasks;

namespace Skinet.Core.Interfaces
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCachedResponseAsync(string cackeKey);
    }
}
