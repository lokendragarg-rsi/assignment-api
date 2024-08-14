using Assignment.Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Services.StoryAPIService
{
    public interface IStoryApiService
    {
        Task<List<StoryDetailDto>> GetStoryItemDetails(int takeRecord);
    }
}
