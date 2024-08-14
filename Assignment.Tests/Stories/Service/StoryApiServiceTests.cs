using Assignment.Dto.Dto;
using Assignment.Services.MemoryCacheServices;
using Assignment.Services.StoryAPIService;
using Assignment.Services.StoryServices;
using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Test.Stories.Service
{
    /// <summary>
    /// The story api service tests
    /// </summary>
    public class StoryApiServiceTests
    {
        private readonly Mock<HttpClient> _mockClient;
        private readonly StoryApiService _storyApiService;
        private readonly Mock<IStoryApiService> _mockStoryApiService;
        private readonly IFixture _fixture;

        /// <summary>
        /// The story api service test
        /// </summary>
        public StoryApiServiceTests()
        {
            _fixture = new Fixture();
            _mockClient = new Mock<HttpClient>();
            _mockStoryApiService = new Mock<IStoryApiService>();
            _storyApiService = new StoryApiService(_mockClient.Object);
        }

        /// <summary>
        /// Should return zero count when no story details found
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Return_Zero_Count_When_No_Story_Details_Found()
        {
            //object
            var resultModel = new List<StoryDetailDto>() { new StoryDetailDto() { title = "test", url = "https://test.com" } };

            _mockStoryApiService.Setup(api => api.GetStoryItemDetails(0)).ReturnsAsync(resultModel);

            // Assert
            var result = await _storyApiService.GetStoryItemDetails(0);

            // Then
            Assert.NotNull(result);
            Assert.Equal(0, result.Count());
            // Then
        }

        /// <summary>
        /// Should return data when story details found
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Return_Data_When_Story_Details_Found()
        {
            //object
            var resultModel = new List<StoryDetailDto>() { new StoryDetailDto() { title = "test", url = "https://test.com" } };

            _mockStoryApiService.Setup(api => api.GetStoryItemDetails(1)).ReturnsAsync(resultModel);
            
            // Assert
            var result = await _storyApiService.GetStoryItemDetails(1);

            // Then
            Assert.NotNull(result);
            Assert.Equal(1, result.Count());
            // Then
        }
    }
}
