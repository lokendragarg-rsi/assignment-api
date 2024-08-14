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

    /// <summary>
    /// The Story controller
    /// </summary>
    /// <param name="storyService"></param>
    public StoryController(IStoryService storyService)
    {
        _storyService = storyService;
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
            if (takeRecord <= 0)
            {
                return BadRequest("Take records should be greater than zero");
            }

            List<StoryDetailDto> storyDetails = await _storyService.GetStoryDetails(takeRecord);
            return storyDetails != null ? Ok(storyDetails) : NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message.ToString());
        }
    }

    #endregion
}
