using System;
using System.Threading.Tasks;

namespace JobOffersPortal.Application.Common.Interfaces
{
    public interface ICacheService
    {
        Task SetCacheValueAsync(string cacheKey, object response, TimeSpan timeToLive);
        Task<string> GetCacheValueAsync(string cacheKey);
    }
}
