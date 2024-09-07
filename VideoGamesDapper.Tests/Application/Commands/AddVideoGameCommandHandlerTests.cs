using VideoGamesDapper.Application.Command;
using VideoGamesDapper.Application.Handler;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Tests.Application.Commands;

public class AddVideoGameCommandHandlerTests
{
    private readonly IVideoGameRepository _videoGameRepository;

    public AddVideoGameCommandHandlerTests()
    {
        _videoGameRepository = A.Fake<IVideoGameRepository>();
    }

    [Fact]
    public async Task Handle_ValidCommand_CallsAddVideoGameAsync()
    {
        // Arrage
        var newVideoGame = new VideoGameInput("Resident Evil Village", "Capcom", "Capcom", new DateTime(2021, 05, 01));
        var serviceResponse = new ServiceResponse<int>()
        {
            Data = 1,
            Success = true,
            Message = string.Empty
        };
        var command = new AddVideoGameCommand(newVideoGame);
        var handler = new AddVideoGameCommandHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.AddVideoGameAsync(newVideoGame))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(1);
        result.Success.Should().BeTrue();
        result.Message.Should().BeEmpty();
        A.CallTo(() => _videoGameRepository.AddVideoGameAsync(newVideoGame))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_invalidCommand_ReturnsErrorMessage()
    {
        // Arrage
        var newVideoGame = new VideoGameInput("Title With Insert Problem", "Publisher With Insert Problem", "Developer With Insert Problem", DateTime.Now);
        var serviceResponse = new ServiceResponse<int>()
        {
            Data = 0,
            Success = false,
            Message = $"An error ocurred while inserting a new register."
        };
        var command = new AddVideoGameCommand(newVideoGame);
        var handler = new AddVideoGameCommandHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.AddVideoGameAsync(newVideoGame))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(0);
        result.Success.Should().BeFalse();
        result.Message.Should().Be(serviceResponse.Message);
        A.CallTo(() => _videoGameRepository.AddVideoGameAsync(newVideoGame))
            .MustHaveHappenedOnceExactly();
    }
}
