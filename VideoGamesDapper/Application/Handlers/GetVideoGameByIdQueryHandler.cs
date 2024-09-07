using MediatR;
using VideoGamesDapper.Application.Queries;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Handler;

public class GetVideoGameByIdQueryHandler : IRequestHandler<GetVideoGameByIdQuery, ServiceResponse<VideoGameResponse>>
{
    private readonly IVideoGameRepository _videoGameRepository;

    public GetVideoGameByIdQueryHandler(IVideoGameRepository videoGameRepository)
    {
        _videoGameRepository = videoGameRepository;
    }

    public async Task<ServiceResponse<VideoGameResponse>> Handle(GetVideoGameByIdQuery request, CancellationToken cancellationToken)
    {
        return await _videoGameRepository.GetVideoGameByIdAsync(request.Id);
    }
}
