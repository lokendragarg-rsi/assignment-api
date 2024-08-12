using Assignment.Services.StoryServices;
using Assignment.Test.Story.Service;

namespace Assignment.Test.Stories.Service;

public sealed class StoryServiceTest
{
    public class StoryItemTest
    {
        /// <summary>
        /// Should Return Story Items If Greater Than Zero
        /// </summary>
        [Fact]
        public async Task Should_Return_Story_Items_If_Greater_Than_Zero()
        {
            //Given
            var StoryService = Substitute.For<IStoryService>();
            StoryService sut = new StoryServiceFixture().WithStoryService(StoryService);
            //When

            //When
            var result = (await sut.GetStoryDetails(200)).Count();

            //Then
            result.Should().BeGreaterThan(0);
        }

        /// <summary>
        /// Should return null if story items less than orequal to zero
        /// </summary>
        [Fact]
        public async Task Should_Return_Null_If_Story_Items_Less_Than_OrEqual_To_Zero()
        {
            //Given
            var StoryService = Substitute.For<IStoryService>();
            StoryService sut = new StoryServiceFixture().WithStoryService(StoryService);
            //When

            //When
            var result = (await sut.GetStoryDetails(-1)).Count();

            //Then
            result.Should().Be(0);
        }
    }
}
