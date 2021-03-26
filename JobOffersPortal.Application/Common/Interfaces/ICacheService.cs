using System;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheValueAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<string> GetCacheValueAsync(string cacheKey);
    }
}
