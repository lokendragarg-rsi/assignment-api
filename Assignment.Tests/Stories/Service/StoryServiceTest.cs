using Assignment.Dto.Dto;
using Assignment.Services.StoryServices;
using Moq;
using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using Assignment.Services.StoryAPIService;
using Assignment.Services.MemoryCacheServices;

namespace Assignment.Test.Stories.Service
{
    /// <summary>
    /// The story service test
    /// </summary>
    public class StoryServiceTest
    {
        private readonly Mock<IMemoryCacheService> _mockMemoryCache;
        private readonly Mock<HttpClient> _mockClient;
        private readonly Mock<IStoryApiService> _mockStoryApiService;
        private readonly StoryService _storyService;
        private readonly IFixture _fixture;
        private readonly Mock<ICacheEntry> _mockCacheEntry;


        /// <summary>
        /// The story service test
        /// </summary>
        public StoryServiceTest()
        {
            _mockStoryApiService = new Mock<IStoryApiService>();
            _fixture = new Fixture();
            _mockMemoryCache = new Mock<IMemoryCacheService>();
            _mockClient = new Mock<HttpClient>();
            _mockCacheEntry = new Mock<ICacheEntry>();
            _storyService = new StoryService(_mockClient.Object, _mockStoryApiService.Object, _mockMemoryCache.Object);
        }

        /// <summary>
        /// Should match records if story items found
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Match_Records_If_Story_Items_Found()
        {
            //object
            var resultModel = new List<StoryDetailDto>() { new StoryDetailDto() { title = "test", url = "https://test.com" } };

            //When
            var cacheKey = "storydetails";
            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };;
            _mockMemoryCache.Setup(mc => mc.Set(cacheKey, resultModel, cacheExpiryOptions));

            _mockStoryApiService.Setup(s => s.GetStoryItemDetails(1)).ReturnsAsync(resultModel);
            // Assert
            var result = await _storyService.GetStoryDetails(1);

            // Then
            Assert.NotNull(result);
            Assert.Equal(resultModel, result);
            // Then
        }

        /// <summary>
        /// Should return null when item does not exist on story details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Return_Null_When_Item_Does_Not_Exist_On_Story_Details()
        {
            //object
            var resultModel = new List<StoryDetailDto>() { new StoryDetailDto() { title = "test", url = "https://test.com" } };

            //When
            var cacheKey = "storydetails";
            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };
            _mockMemoryCache.Setup(mc => mc.Set(cacheKey, resultModel, cacheExpiryOptions));

            _mockStoryApiService.Setup(s => s.GetStoryItemDetails(1)).Returns(Task.FromResult((List<StoryDetailDto>)null));
            // Assert
            var result = await _storyService.GetStoryDetails(1);

            // Then
            Assert.Null(result);
            // Then
        }

        /// <summary>
        /// Should return data from cache on story details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task Should_Return_Data_From_Cache_On_Story_Details()
        {
            //object
            var resultModel = new List<StoryDetailDto>() { new StoryDetailDto() { title = "test", url = "https://test.com" }, new StoryDetailDto() { title = "test1", url = "https://test1.com" } };

            var key = _fixture.Create<string>();
            var objectToCache = _fixture.Create<List<StoryDetailDto>>();

            _mockStoryApiService.Setup(api => api.GetStoryItemDetails(0)).ReturnsAsync(resultModel);

            var cacheKey = "storydetails";
            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };
            _mockMemoryCache.Setup(cache => cache.TryGetValue(cacheKey,out resultModel)).Returns(true);

            // Act
            var result = await _storyService.GetStoryDetails(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }
    }
}
