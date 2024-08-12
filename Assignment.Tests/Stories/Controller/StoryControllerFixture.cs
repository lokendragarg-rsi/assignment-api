using Assignment.API.Controllers;
using Assignment.Services.StoryServices;
using Microsoft.Extensions.Caching.Memory;

namespace Assignment.Test.Story.Controller;

/// <summary>
/// The StoryControllerFixture
/// </summary>
/// <seealso cref="Rocket.Surgery.Extensions.Testing.Fixtures.ITestFixtureBuilder" />
public class StoryControllerFixture : ITestFixtureBuilder
{
    /// <summary>
    /// The story service
    /// </summary>
    private IStoryService _storyService;

    private readonly IMemoryCache _memoryCache;

    /// <summary>
    /// 
    /// </summary>
    public StoryControllerFixture()
    {
        _storyService = NSubstitute.Substitute.For<IStoryService>();
        _memoryCache = NSubstitute.Substitute.For<IMemoryCache>();
    }

    /// <summary>
    /// Withes the story service.
    /// </summary>
    /// <param name="storyService">The story Service.</param>
    /// <returns></returns>
    public StoryControllerFixture WithStoryService(IStoryService storyService) => this.With(field: ref _storyService, value: storyService);

    /// <summary>
    /// Performs an implicit conversion from <see cref="StoryControllerFixture"/> to <see cref="StoryController"/>.
    /// </summary>
    /// <param name="fixture">The fixture.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator StoryController(StoryControllerFixture fixture) => fixture.Build();
    
    /// <summary>
    /// Builds this instance.
    /// </summary>
    /// <returns></returns>
    public StoryController Build() => new StoryController(_storyService, _memoryCache);
}
