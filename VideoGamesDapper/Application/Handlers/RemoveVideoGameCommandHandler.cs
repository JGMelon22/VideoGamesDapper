using MediatR;
using VideoGamesDapper.Application.Command;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Handler;

public class RemoveVideoGameCommandHandler : IRequestHandler<RemoveVideoGameCommand, ServiceResponse<int>>
{
    private readonly IVideoGameRepository _videoGameRepository;

    public RemoveVideoGameCommandHandler(IVideoGameRepository videoGameRepository)
    {
        _videoGameRepository = videoGameRepository;
    }

    public async Task<ServiceResponse<int>> Handle(RemoveVideoGameCommand request, CancellationToken cancellationToken)
    {
        return await _videoGameRepository.RemoveVideoGameAsync(request.Id);
    }
}
