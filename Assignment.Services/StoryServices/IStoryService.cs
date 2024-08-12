using Assignment.Dto.Dto;

namespace Assignment.Services.StoryServices;

public interface IStoryService
{
    /// <summary>
    /// The get story details
    /// </summary>
    /// <returns></returns>
    Task<List<StoryDetailDto>> GetStoryDetails(int takeRecord);
}
