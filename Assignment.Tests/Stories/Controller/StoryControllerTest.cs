using Assignment.Dto.Dto;
using Assignment.API.Controllers;
using Assignment.Services.StoryServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Microsoft.Extensions.Caching.Memory;

namespace Assignment.Test.Story.Controller;

/// <summary>
/// The story controller test
/// </summary>
public class StoryControllerTest
{
    private readonly Mock<IStoryService> _storyServiceMock;
    private readonly StoryController _storyController;

    /// <summary>
    /// The story controller test
    /// </summary>
    public StoryControllerTest()
    {
        _storyServiceMock = new Mock<IStoryService>();
        _storyController = new StoryController(_storyServiceMock.Object);
    }

    /// <summary>
    /// Should return items if story details exist
    /// </summary>
    /// <returns></returns>
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
        _storyServiceMock.Setup(s => s.GetStoryDetails(1)).ReturnsAsync(resultModel);

        //When
        var result = await _storyController.GetStoryDetails(1);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        okResult.Should().NotBeNull();
        okResult.StatusCode.Should().Be(200);

        //Then
        okResult.Value.Should().BeEquivalentTo(resultModel);
        //Then
    }

    /// <summary>
    /// Should return zero count if no story details exist
    /// </summary>
    [Fact]
    public async Task Should_Return_Not_Found_If_No_Story_Details_Exist()
    {
        //Given
        _storyServiceMock.Setup(s => s.GetStoryDetails(1)).Returns(Task.FromResult((List<StoryDetailDto>)null));

        //When
        var result = await _storyController.GetStoryDetails(1);

        // Assert
        var notFoundResult = Assert.IsType<NotFoundResult>(result);
        notFoundResult.Should().NotBeNull();
        //Then
        notFoundResult.StatusCode.Should().Be(404);
        //Then
    }

    /// <summary>
    /// Should return error if less than zero record pass in request parameter for story details
    /// </summary>
    [Fact]
    public async Task Should_Return_Error_If_Less_Than_Zero_Record_Pass_In_Request_Parameter_For_Story_Details()
    {
        //Given
        _storyServiceMock.Setup(s => s.GetStoryDetails(-1)).Returns(Task.FromResult((new List<StoryDetailDto>())));

        //When
        var result = await _storyController.GetStoryDetails(-1);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        //Then
        badRequestResult.StatusCode.Should().Be(400);
        //Then
    }

    /// <summary>
    /// Should throw exception when no record exist
    /// </summary>
    [Fact]
    public async Task Should_Throw_Exception_When_No_Record_Exist()
    {
        //Given
        _storyServiceMock.Setup(s => s.GetStoryDetails(1)).Throws(new IOException());

        //When
        var result = await _storyController.GetStoryDetails(1);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);

        //Then
        badRequestResult.StatusCode.Should().Be(400);
        //Then
    }
}
