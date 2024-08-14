using Assignment.Dto.Dto;
using Assignment.Dto.Enum;
using Microsoft.Extensions.Caching.Memory;

namespace Assignment.Services.MemoryCacheServices;

public class MemoryCacheService : IMemoryCacheService
{
    private readonly IMemoryCache _memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public MemoryCacheResultDto Set<T>(string key, T cache, MemoryCacheEntryOptions expirtaion)
    {
        MemoryCacheResultDto objCacheDetails = new MemoryCacheResultDto();
        try
        {
            _memoryCache.Set(key, cache, expirtaion);
            objCacheDetails.Error = null;
            objCacheDetails.CacheStatus = MemoryCacheStatusOption.Cached;
        }
        catch (Exception ex)
        {
            objCacheDetails.Error = ex;
            objCacheDetails.CacheStatus = MemoryCacheStatusOption.Error;
        }
        return objCacheDetails;
    }

    public bool TryGetValue<T>(string Key, out T cache)
    {
        try
        {
            if (_memoryCache.TryGetValue(Key, out T cachedItem))
            {
                cache = cachedItem;
                return true;
            }
            cache = default(T);
            return false;
        }
        catch (Exception ex)
        {
            cache = default(T);
            return false;
        }
    }
}
