using Assignment.Services.MemoryCacheServices;
using Assignment.Services.StoryAPIService;
using Assignment.Services.StoryServices;
using AutoFixture;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Assignment.Dto.Enum;
using Assignment.Dto.Dto;

namespace Assignment.Test.Stories.Service
{
    /// <summary>
    /// The memory cache service test
    /// </summary>
    public class MemoryCacheServiceTest
    {
        private readonly Mock<IMemoryCache> _mockMemoryCache;       
        private readonly Mock<ICacheEntry> _mockCacheEntry;
        private readonly MemoryCacheService _memoryCacheService;

        /// <summary>
        /// The memory cache service test
        /// </summary>
        public MemoryCacheServiceTest()
        {
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockCacheEntry = new Mock<ICacheEntry>();
            _memoryCacheService = new MemoryCacheService(_mockMemoryCache.Object);
        }

        /// <summary>
        /// Should throw exception on retrieve value from cache in story details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void Should_Throw_Exception_On_Save_Value_In_Cache_For_Story_Details()
        {
            // Arrange
            var key = "storydetails";
            var resultModel = new List<StoryDetailDto>();
            _mockMemoryCache.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Throws(new Exception("Object reference not set to an instance of an object."));

            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };

            // Act
            var result = _memoryCacheService.Set(key, resultModel, cacheExpiryOptions);

            // Assert
            Assert.Equal(MemoryCacheStatusOption.Error, result.CacheStatus);
            Assert.NotNull(result.Error);
            Assert.Equal("Object reference not set to an instance of an object.", result.Error.Message);
        }

        /// <summary>
        /// Should throw exception on retrieve value from cache
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void Should_Throw_Exception_On_Retrieve_Value_From_Cache()
        {
            // Arrange
            var key = "storydetails";
            var resultModel = new List<StoryDetailDto>();
            _mockMemoryCache.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Throws(new Exception());

            // Act
            var result = _memoryCacheService.TryGetValue(key,out resultModel);

            // Assert
            Assert.Equal(result, false);
        }

        /// <summary>
        /// Should save value in cache for story details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void Should_Save_Value_In_Cache_For_Story_Details()
        {
            // Arrange
            var key = "storydetails";
            string? keyPayload = null;
            var resultModel = new List<StoryDetailDto>();
            _mockMemoryCache.Setup(mc => mc.CreateEntry(It.IsAny<object>())).Callback((object k) => keyPayload = (string)k).Returns(_mockCacheEntry.Object);

            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };

            // Act
            var result = _memoryCacheService.Set(key, resultModel, cacheExpiryOptions);

            // Assert
            Assert.Equal(MemoryCacheStatusOption.Cached, result.CacheStatus);
            Assert.Null(result.Error);
        }

        /// <summary>
        /// Should retrieve value when return false in cache for story details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void Should_Retrieve_Value_When_Return_False_In_Cache_For_Story_Details()
        {
            // Arrange
            var key = "storydetails";
            var resultModel = new List<StoryDetailDto>();
            _mockMemoryCache.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Returns(false);

            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };

            // Act
            var result = _memoryCacheService.TryGetValue(key, out It.Ref<object>.IsAny);

            // Assert
            //Assert.Equal(MemoryCacheStatusOption.Cached, result.CacheStatus);
            Assert.Equal(result, false);
        }

        /// <summary>
        /// Should retrieve value when return true in cache for story details
        /// </summary>
        /// <returns></returns>
        [Fact]
        public void Should_Retrieve_Value_When_Return_True_In_Cache_For_Story_Details()
        {
            // Arrange
            var key = "storydetails";
            var resultModel = new List<StoryDetailDto>();
            _mockMemoryCache.Setup(cache => cache.TryGetValue(key, out It.Ref<object>.IsAny)).Returns(true);

            //setting up cache options
            var cacheExpiryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(5),
                Priority = CacheItemPriority.Normal,
                SlidingExpiration = TimeSpan.FromMinutes(5),
            };

            // Act
            var result = _memoryCacheService.TryGetValue(key, out It.Ref<object>.IsAny);

            // Assert
            //Assert.Equal(MemoryCacheStatusOption.Cached, result.CacheStatus);
            Assert.Equal(result, true);
        }
    }
}
