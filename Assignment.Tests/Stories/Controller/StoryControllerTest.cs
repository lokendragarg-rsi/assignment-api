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
            var okResult = await sut.GetStoryDetails(1);
            var result = ((RequestOutcome<List<StoryDetailDto>>)(okResult as OkObjectResult).Value);

            //Then
            result?.Data.Should().HaveCount(1);
            //Then
        }

        /// <summary>
        /// Should return zero count if no story details exist
        /// </summary>
        [Fact]
        public async Task Should_Return_Zero_Count_If_No_Story_Details_Exist()
        {
            //Given
            var StoryService = Substitute.For<IStoryService>();
            StoryService.GetStoryDetails(Arg.Any<int>()).Returns(new List<StoryDetailDto>());
            StoryController sut = new StoryControllerFixture().WithStoryService(StoryService);
            //When
            var okResult = await sut.GetStoryDetails(1);
            var result = ((RequestOutcome<List<StoryDetailDto>>)(okResult as OkObjectResult).Value);

            //Then
            result?.Data.Should().HaveCount(0);
            //Then
        }

        /// <summary>
        /// Should return zero count if no story details exist
        /// </summary>
        [Theory]
        [InlineData(1, "test1")]
        [InlineData(2, "test2")]
        public async Task Should_Return_Title_If_Request_Params_Provides(int takeRecord, string title)
        {
            //Given
            var StoryService = Substitute.For<IStoryService>();
            StoryService.GetStoryDetails(Arg.Any<int>()).Returns(Task.FromResult(new List<StoryDetailDto>()
                {
                    new StoryDetailDto()
                    {
                        title = title
                    }
                }));
            StoryController sut = new StoryControllerFixture().WithStoryService(StoryService);
            //When
            var okResult = await sut.GetStoryDetails(takeRecord);
            var result = ((RequestOutcome<List<StoryDetailDto>>)(okResult as OkObjectResult).Value);

            //Then
            result?.Data?[0].title.Should().Be(title);
            //Then
        }

        /// <summary>
        /// Should return zero count if no story details exist
        /// </summary>
        [Theory]
        [InlineData(1, "test1.com")]
        [InlineData(2, "test2.com")]
        public async Task Should_Return_Url_If_Request_Params_Provides(int takeRecord, string url)
        {
            //Given
            var StoryService = Substitute.For<IStoryService>();
            StoryService.GetStoryDetails(Arg.Any<int>()).Returns(Task.FromResult(new List<StoryDetailDto>()
                {
                    new StoryDetailDto()
                    {
                        url = url
                    }
                }));
            StoryController sut = new StoryControllerFixture().WithStoryService(StoryService);
            //When
            var okResult = await sut.GetStoryDetails(takeRecord);
            var result = ((RequestOutcome<List<StoryDetailDto>>)(okResult as OkObjectResult).Value);

            //Then
            result?.Data?[0].url.Should().Be(url);
            //Then
        }

        /// <summary>
        /// Should return error if less than zero record pass in story details
        /// </summary>
        [Fact]
        public async Task Should_Return_Error_If_Less_Than_Zero_Record_Pass_In_Story_Details()
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
