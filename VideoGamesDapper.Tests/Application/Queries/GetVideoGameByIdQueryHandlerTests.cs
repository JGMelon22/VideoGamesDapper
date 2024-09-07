using VideoGamesDapper.Application.Handler;
using VideoGamesDapper.Application.Queries;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Tests.Application.Queries;

public class GetVideoGameByIdQueryHandlerTests
{
    private readonly IVideoGameRepository _videoGameRepository;

    public GetVideoGameByIdQueryHandlerTests()
    {
        _videoGameRepository = A.Fake<IVideoGameRepository>();
    }

    [Fact]
    public async Task Handle_ValidId_ReturnsVideoGameResponse()
    {
        // Arrange
        int id = 1;
        var videoGameRespose = new VideoGameResponse
        {
            Id = id,
            Title = "Sonic",
            Publisher = "Sega",
            Developer = "Sega",
            ReleaseDate = new DateTime(1991, 06, 23)
        };
        var serviceResponse = new ServiceResponse<VideoGameResponse>
        {
            Data = videoGameRespose,
            Success = true,
            Message = string.Empty
        };
        var query = new GetVideoGameByIdQuery(id);
        var handler = new GetVideoGameByIdQueryHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.GetVideoGameByIdAsync(id))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(videoGameRespose);
        result.Success.Should().BeTrue();
        result.Message.Should().BeEmpty();
        A.CallTo(() => _videoGameRepository.GetVideoGameByIdAsync(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_InvalidId_ReturnsErrorMessage()
    {
        // Arrange
        int id = 2;
        var videoGameRespose = new VideoGameResponse
        {
            Id = 6,
            Title = "Need for Speed Underground",
            Publisher = "EA",
            Developer = "Black Box",
            ReleaseDate = new DateTime(2003, 11, 17)
        };
        var serviceResponse = new ServiceResponse<VideoGameResponse>
        {
            Data = videoGameRespose,
            Success = false,
            Message = $"Video Game with id {id} not found!"
        };
        var query = new GetVideoGameByIdQuery(id);
        var handler = new GetVideoGameByIdQueryHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.GetVideoGameByIdAsync(id))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Success.Should().BeFalse();
        result.Message.Should().NotBeEmpty();
        result.Message.Should().Be(serviceResponse.Message);
        A.CallTo(() => _videoGameRepository.GetVideoGameByIdAsync(id))
            .MustHaveHappenedOnceExactly();
    }
}
