using Assignment.Dto.Dto;
using Assignment.Framework.Builder.Common;
using Assignment.Services.StoryServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace Assignment.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StoryController : ControllerBase
{
    private readonly IStoryService _storyService;
    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// The Story controller
    /// </summary>
    /// <param name="storyService"></param>
    /// <param name="memoryCache"></param>
    public StoryController(IStoryService storyService, IMemoryCache memoryCache)
    {
        _storyService = storyService;
        _memoryCache = memoryCache;
    }


    #region Story Details

    /// <summary>
    /// The get story details
    /// </summary>
    /// <returns></returns>
    [HttpGet("getstorydetails")]
    public async Task<ActionResult> GetStoryDetails([FromQuery] int takeRecord)
    {
        try
        {
            var cacheKey = "storydetails";
            if (!_memoryCache.TryGetValue(cacheKey, out List<StoryDetailDto> storyDetails))
            {
                if (takeRecord == 0)
                {
                    return BadRequest("Records should be greater than zero");
                }

                storyDetails = await _storyService.GetStoryDetails(takeRecord);

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
            return Ok(new RequestOutcome<List<StoryDetailDto>> { IsSuccess = true, Message = "Success", Data = storyDetails, StatusCode = 200 });
        }
        catch (Exception ex)
        {
            return Ok(new RequestOutcome<string> { IsSuccess = false, Message = ex.Message.ToString(), Data = null, StatusCode = 400 });
        }
    }

    #endregion
}
