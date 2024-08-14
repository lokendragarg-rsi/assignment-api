using Assignment.Dto.Dto;
using Assignment.API.Controllers;
using Assignment.Services.StoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Framework.Builder.Common;
using Microsoft.Extensions.Logging;
using Moq;
using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using NSubstitute;
using Castle.Core.Configuration;
using Assignment.Services.StoryAPIService;
using NSubstitute.Extensions;

namespace Assignment.Test.Stories.Service
{
    /// <summary>
    /// The story service test
    /// </summary>
    public class StoryServiceTest
    {
        private readonly Mock<IConfiguration> _mockConfiguration;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<HttpClient> _mockClient;
        private readonly Mock<IStoryApiService> _mockStoryApiService;
        private readonly StoryService _storyService;
        private readonly IFixture _fixture;

        public StoryServiceTest()
        {
            _mockStoryApiService = new Mock<IStoryApiService>();
            _fixture = new Fixture();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockClient = new Mock<HttpClient>();
            _storyService = new StoryService(_mockClient.Object, _mockMemoryCache.Object, _mockStoryApiService.Object);
        }

        [Fact]
        public async Task Should_Match_Items_If_Story_Details_Exist()
        {
            var resultModel = new List<StoryDetailDto>()
                {
                    new StoryDetailDto()
                    {
                        title = "Test",
                        url = "test"
                    }
                };
            //Given
            _mockStoryApiService.Setup(s => s.GetStoryItemDetails(1)).ReturnsAsync(resultModel);

            var key = "storydetails";
            _mockMemoryCache.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Returns(true);

            //When
            var result = await _storyService.GetStoryDetails(1);

            // Assert
            Assert.NotNull(result);
            //Assert.Equal(3, result.TotalNoOfItems);
            //Assert.Equal(2, result.Items.Count()); // Because PageSize is 2 and Page is 1, so only first 2 items
        }
    }
}
