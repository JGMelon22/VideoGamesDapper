using VideoGamesDapper.Application.Command;
using VideoGamesDapper.Application.Handler;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Tests.Application.Commands;

public class UpdateVideoGameCommandHandlerTests
{
    private readonly IVideoGameRepository _videoGameRepository;

    public UpdateVideoGameCommandHandlerTests()
    {
        _videoGameRepository = A.Fake<IVideoGameRepository>();
    }

    [Fact]
    public async Task Handle_ValidCommand_CallsUpdateVideoGameAsync()
    {
        // Arrange
        int id = 1;
        var updatedVideoGame = new VideoGameInput("Vortex Battle", "Quantum Games", "Starlink Interactive", new DateTime(2001, 07, 17));
        var serviceResponse = new ServiceResponse<int>
        {
            Data = 1,
            Success = true,
            Message = string.Empty
        };
        var command = new UpdateVideoGameCommand(id, updatedVideoGame);
        var handler = new UpdateVideoGameCommandHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.UpdateVideoGameAsync(id, updatedVideoGame))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(1);
        result.Success.Should().BeTrue();
        result.Message.Should().BeEmpty();
        A.CallTo(() => _videoGameRepository.UpdateVideoGameAsync(id, updatedVideoGame))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_InvalidCommand_ReturnsErrorMessage()
    {
        // Arrange
        int id = 3;
        var updatedVideoGame = new VideoGameInput("Title With Update Problem", "Publisher With Update Problem", "Developer With Update Problem", DateTime.Now);
        var serviceResponse = new ServiceResponse<int>
        {
            Data = 0,
            Success = false,
            Message = $"Video Game with id {id} not found!"
        };
        var command = new UpdateVideoGameCommand(id, updatedVideoGame);
        var handler = new UpdateVideoGameCommandHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.UpdateVideoGameAsync(id, updatedVideoGame))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(0);
        result.Success.Should().BeFalse();
        result.Message.Should().Be(serviceResponse.Message);
        A.CallTo(() => _videoGameRepository.UpdateVideoGameAsync(id, updatedVideoGame))
            .MustHaveHappenedOnceExactly();
    }
}
