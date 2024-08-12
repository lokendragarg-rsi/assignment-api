using Assignment.Dto.Dto;
using Assignment.API.Controllers;
using Assignment.Services.StoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Assignment.Framework.Builder.Common;

namespace Assignment.Test.Story.Controller;

public class StoryControllerTest
{
    public class StoryItemTest
    {
        /// <summary>
        /// Should Return Items If Story Details Exist
        /// </summary>
        [Fact]
        public async Task Should_Return_Items_If_Story_Details_Exist()
        {
            //Given
            var StoryService = Substitute.For<IStoryService>();
            StoryService.GetStoryDetails(Arg.Any<int>()).Returns(Task.FromResult(new List<StoryDetailDto>()
                {
                    new StoryDetailDto()
                    {
                        title = "Test",
                        url = "test"
                    }
                }));
            StoryController sut = new StoryControllerFixture().WithStoryService(StoryService);
            //When
            var okResult = await sut.GetStoryDetails(200);
            var result = ((RequestOutcome<List<StoryDetailDto>>)(okResult as OkObjectResult).Value);

            //Then
            result?.Data.Should().HaveCount(1);
            //Then
        }

        /// <summary>
        /// Should Return Error If Story Details NotFound
        /// </summary>
        [Fact]
        public async Task Should_Return_Error_If_Story_Details_NotFound()
        {
            //Given
            var StoryService = Substitute.For<IStoryService>();
            StoryService.GetStoryDetails(Arg.Any<int>()).Returns(Task.FromResult(new List<StoryDetailDto>()));
            StoryController sut = new StoryControllerFixture().WithStoryService(StoryService);
            //When
            var okResult = await sut.GetStoryDetails(0);
            var result = (okResult as BadRequestObjectResult);
            //Then
            result?.StatusCode.Should().Be(400);
            //Then
        }
    }
}
