using Assignment.Dto.Dto;
using Assignment.Services.StoryAPIService;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Assignment.Services.StoryServices;

public class StoryService : IStoryService
{
    private HttpClient _client;

    private readonly IMemoryCache _memoryCache;

    private readonly IStoryApiService _storyApiService;

    /// <summary>
    /// The Story controller
    /// </summary>
    /// <param name="client"></param>
    public StoryService(HttpClient client, IMemoryCache memoryCache, IStoryApiService storyApiService)
    {
        _client = client;
        _memoryCache = memoryCache;
        _storyApiService = storyApiService;
    }


    #region get story details

    /// <summary>
    /// The get story details
    /// </summary>
    /// <returns></returns>
    public async Task<List<StoryDetailDto>> GetStoryDetails(int takeRecord)
    {
        var cacheKey = "storydetails";
        if (!_memoryCache.TryGetValue(cacheKey, out List<StoryDetailDto> storyDetails))
        {
            storyDetails = await _storyApiService.GetStoryItemDetails(takeRecord);

            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };
            //setting cache entries
            _memoryCache.Set(cacheKey, storyDetails, cacheExpiryOptions);
        }

        return storyDetails;
    }
    #endregion
}
