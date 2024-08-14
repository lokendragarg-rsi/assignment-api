using Assignment.Dto.Dto;
using Assignment.Services.MemoryCacheServices;
using Assignment.Services.StoryAPIService;
using Microsoft.Extensions.Caching.Memory;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Assignment.Services.StoryServices;

public class StoryService : IStoryService
{
    private HttpClient _client;

    private readonly IStoryApiService _storyApiService;

    private readonly IMemoryCacheService _memoryCacheService;

    /// <summary>
    /// The Story controller
    /// </summary>
    /// <param name="client"></param>
    public StoryService(HttpClient client, IStoryApiService storyApiService, IMemoryCacheService memoryCacheService)
    {
        _client = client;
        _storyApiService = storyApiService;
        _memoryCacheService = memoryCacheService;
    }


    #region get story details

    /// <summary>
    /// The get story details
    /// </summary>
    /// <returns></returns>
    public async Task<List<StoryDetailDto>> GetStoryDetails(int takeRecord)
    {
        var cacheKey = "storydetails";
        if (!_memoryCacheService.TryGetValue(cacheKey, out List<StoryDetailDto> storyDetails))
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
            _memoryCacheService.Set(cacheKey, storyDetails, cacheExpiryOptions);
        }

        return storyDetails;
    }
    #endregion
}
