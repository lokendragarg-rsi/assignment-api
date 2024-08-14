using Assignment.Dto.Dto;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace Assignment.Services.StoryAPIService;

public class StoryApiService : IStoryApiService
{
    private HttpClient _client;

    /// <summary>
    /// The Story controller
    /// </summary>
    /// <param name="client"></param>
    public StoryApiService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<StoryDetailDto>> GetStoryItemDetails(int takeRecord)
    {
        List<StoryDetailDto> storyDetails = new List<StoryDetailDto>();
        if (takeRecord > 0)
        {
            string apiUrl = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
            HttpResponseMessage response = await _client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var storyDetailIds = await JsonSerializer.DeserializeAsync<List<int>>(await response.Content.ReadAsStreamAsync());
                if (storyDetailIds != null)
                {
                    var itemIds = storyDetailIds.Take(takeRecord).ToList();
                    StoryDetailDto storyDetailItem = new StoryDetailDto();
                    Parallel.ForEach(itemIds, new ParallelOptions() { MaxDegreeOfParallelism = 25 }, item =>
                    {
                        _client = new HttpClient();
                        string storyDetailAPIUrl = "https://hacker-news.firebaseio.com/v0/item/" + item + ".json?print=pretty";
                        HttpResponseMessage responseDetail = _client.GetAsync(storyDetailAPIUrl).Result;
                        if (responseDetail.IsSuccessStatusCode)
                        {
                            storyDetailItem = JsonSerializer.DeserializeAsync<StoryDetailDto>(responseDetail.Content.ReadAsStreamAsync().Result).Result;
                            if (storyDetailItem != null && !string.IsNullOrEmpty(storyDetailItem.url) && !string.IsNullOrEmpty(storyDetailItem.title))
                            {
                                storyDetails.Add(storyDetailItem);
                            }
                        }
                    });
                }
            }
        }
        return storyDetails;
    }
}
