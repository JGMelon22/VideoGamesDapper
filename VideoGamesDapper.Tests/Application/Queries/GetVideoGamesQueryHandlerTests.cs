using VideoGamesDapper.Application.Handler;
using VideoGamesDapper.Application.Queries;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Tests.Application.Queries;

public class GetVideoGamesQueryHandlerTests
{
    private readonly IVideoGameRepository _videoGameRepository;

    public GetVideoGamesQueryHandlerTests()
    {
        _videoGameRepository = A.Fake<IVideoGameRepository>();
    }

    [Fact]
    public async Task Handle_CollectionIsPopulated_ReturnsVideoGameResponse()
    {
        // Arrange
        var videoGameRespose = new List<VideoGameResponse>
        {
            new ()
            {
                Id = 2,
                Title = "Galactic Quest",
                Publisher = "Starsoft",
                Developer = "Nebula Studios",
                ReleaseDate = new DateTime(1993, 11, 05)
            },
            new ()
            {
                Id = 3,
                Title = "Mystic Legends",
                Publisher = "Eldertide",
                Developer = "Arcane Games",
                ReleaseDate = new DateTime(1995, 04, 12)
            },
            new ()
            {
                Id = 4,
                Title = "Cyber Racer",
                Publisher = "FutureTech",
                Developer = "Neon Drive",
                ReleaseDate = new DateTime(1997, 09, 30)
            },
            new ()
            {
                Id = 5,
                Title = "Dragon's Fury",
                Publisher = "Mythic Realm",
                Developer = "Firebrand Studios",
                ReleaseDate = null
            },
            new ()
            {
                Id = 6,
                Title = "Vortex Battle",
                Publisher = "Quantum Games",
                Developer = "Starlink Interactive",
                ReleaseDate = new DateTime(2001, 07, 17)
            }
        };
        var serviceResponse = new ServiceResponse<ICollection<VideoGameResponse>>
        {
            Data = videoGameRespose,
            Success = true,
            Message = string.Empty
        };
        var query = new GetVideoGamesQuery();
        var handler = new GetVideoGamesQueryHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.GetAllVideoGamesAsync())
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data!.Count().Should().Be(5);
        result.Success.Should().BeTrue();
        result.Message.Should().BeEmpty();
        A.CallTo(() => _videoGameRepository.GetAllVideoGamesAsync())
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task Handle_CollectionIsEmpty_ReturnsNoResponse()
    {
        // Arrange
        var videoGameRespose = new List<VideoGameResponse> { };

        var serviceResponse = new ServiceResponse<ICollection<VideoGameResponse>>
        {
            Data = videoGameRespose,
            Success = true,
            Message = string.Empty
        };
        var query = new GetVideoGamesQuery();
        var handler = new GetVideoGamesQueryHandler(_videoGameRepository);

        A.CallTo(() => _videoGameRepository.GetAllVideoGamesAsync())
            .Returns(Task.FromResult(serviceResponse));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Data!.Count().Should().Be(0);
        result.Success.Should().BeTrue();
        result.Message.Should().BeEmpty();
        A.CallTo(() => _videoGameRepository.GetAllVideoGamesAsync())
            .MustHaveHappenedOnceExactly();
    }
}
