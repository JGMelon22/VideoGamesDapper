using MediatR;
using VideoGamesDapper.Application.Command;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Handler;

public class UpdateVideoGameCommandHandler : IRequestHandler<UpdateVideoGameCommand, ServiceResponse<int>>
{
    private readonly IVideoGameRepository _videoGameRepository;

    public UpdateVideoGameCommandHandler(IVideoGameRepository videoGameRepository)
    {
        _videoGameRepository = videoGameRepository;
    }

    public async Task<ServiceResponse<int>> Handle(UpdateVideoGameCommand request, CancellationToken cancellationToken)
    {
        return await _videoGameRepository.UpdateVideoGameAsync(request.Id, request.UpdatedVideoGame);
    }
}
