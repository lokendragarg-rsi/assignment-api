using Assignment.Dto.Dto;
using System.IO;
using System.Net.Http.Json;
using System.Text.Json;

namespace Assignment.Services.StoryServices;

public class StoryService : IStoryService
{
    private HttpClient _client;

    /// <summary>
    /// The Story controller
    /// </summary>
    /// <param name="client"></param>
    public StoryService(HttpClient client)
    {
        _client = client;
    }


    #region get story details

    /// <summary>
    /// The get story details
    /// </summary>
    /// <returns></returns>
    public async Task<List<StoryDetailDto>> GetStoryDetails(int takeRecord)
    {
        var storyDetails = new List<StoryDetailDto>();
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
                    string storyDetailAPIUrl = "https://hacker-news.firebaseio.com/v0/item/"+ item + ".json?print=pretty";
                    HttpResponseMessage responseDetail = _client.GetAsync(storyDetailAPIUrl).Result;
                    if (responseDetail.IsSuccessStatusCode)
                    {
                        storyDetailItem = JsonSerializer.DeserializeAsync<StoryDetailDto>(responseDetail.Content.ReadAsStreamAsync().Result).Result;
                        if(storyDetailItem != null)
                        {
                            storyDetails.Add(storyDetailItem);
                        }
                    }
                });
            }
        }

        return storyDetails;
    }
    #endregion
}
