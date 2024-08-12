using Assignment.Framework.Builder.Factory;
using Assignment.Services.StoryServices;

namespace Assignment.Test.Story.Service;

public class StoryServiceFixture : ITestFixtureBuilder
{
    private HttpClient _client;
    private IResponseBuilderFactory _responseBuilder;

    /// <summary>
    /// 
    /// </summary>
    public StoryServiceFixture()
    {
        _client = NSubstitute.Substitute.For<HttpClient>();
        _responseBuilder = Substitute.For<IResponseBuilderFactory>();
    }

    /// <summary>
    /// Withes the story service.
    /// </summary>
    /// <param name="storyService">The story Service.</param>
    /// <returns></returns>
    public StoryServiceFixture WithStoryService(IStoryService storyService) => this.With(field: ref _client, value: _client);

    /// <summary>
    /// Performs an implicit conversion from <see cref="StoryServiceFixture" /> to <see cref="StoryService" />.
    /// </summary>
    /// <param name="fixture">The fixture.</param>
    /// <returns>
    /// The result of the conversion.
    /// </returns>
    public static implicit operator StoryService(StoryServiceFixture fixture) => fixture.Build();
    /// <summary>
    /// Builds this instance.
    /// </summary>
    /// <returns></returns>
    private StoryService Build() => new StoryService(_client);
}
