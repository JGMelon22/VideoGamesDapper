using VideoGamesDapper.Application.Command;
using VideoGamesDapper.Application.Handler;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Tests.Application.Commands;

public class RemoveVideoGameCommandHandlerTests
{
    private readonly IVideoGameRepository _videoGameRepository;

    public RemoveVideoGameCommandHandlerTests()
    {
        _videoGameRepository = A.Fake<IVideoGameRepository>();
    }

    [Fact]
    public async Task Handle_ValidCommand_CallsRemoveVideoGameAsync()
    {
        // Arrage
        int id = 1;
        var serviceResponse = new ServiceResponse<int>()
        {
            Data = 1,
            Success = true,
            Message = string.Empty
        };
        var command = new RemoveVideoGameCommand(id);
        var handler = new RemoveVideoGameCommandHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.RemoveVideoGameAsync(id))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(1);
        result.Success.Should().BeTrue();
        result.Message.Should().BeEmpty();
        A.CallTo(() => _videoGameRepository.RemoveVideoGameAsync(id))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_invalidCommand_ReturnsErrorMessage()
    {
        // Arrage
        int id = 27;
        var serviceResponse = new ServiceResponse<int>()
        {
            Data = 0,
            Success = false,
            Message = $"Video Game with id {id} not found!"
        };
        var command = new RemoveVideoGameCommand(id);
        var handler = new RemoveVideoGameCommandHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.RemoveVideoGameAsync(id))
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data.Should().Be(0);
        result.Success.Should().BeFalse();
        result.Message.Should().Be(serviceResponse.Message);
        A.CallTo(() => _videoGameRepository.RemoveVideoGameAsync(id))
            .MustHaveHappenedOnceExactly();
    }
}
