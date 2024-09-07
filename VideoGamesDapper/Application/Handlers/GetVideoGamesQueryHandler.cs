using MediatR;
using VideoGamesDapper.Application.Queries;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Handler;

public class GetVideoGamesQueryHandler : IRequestHandler<GetVideoGamesQuery, ServiceResponse<ICollection<VideoGameResponse>>>
{
    private readonly IVideoGameRepository _videoGameRespository;

    public GetVideoGamesQueryHandler(IVideoGameRepository videoGameRespository)
    {
        _videoGameRespository = videoGameRespository;
    }

    public async Task<ServiceResponse<ICollection<VideoGameResponse>>> Handle(GetVideoGamesQuery request, CancellationToken cancellationToken)
    {
        return await _videoGameRespository.GetAllVideoGamesAsync();
    }
}
