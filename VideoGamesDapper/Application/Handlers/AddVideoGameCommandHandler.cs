using MediatR;
using VideoGamesDapper.Application.Command;
using VideoGamesDapper.Interfaces;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Handler;

public class AddVideoGameCommandHandler : IRequestHandler<AddVideoGameCommand, ServiceResponse<int>>
{
    private readonly IVideoGameRepository _videoGameRepository;

    public AddVideoGameCommandHandler(IVideoGameRepository videoGameRepository)
    {
        _videoGameRepository = videoGameRepository;
    }

    public async Task<ServiceResponse<int>> Handle(AddVideoGameCommand request, CancellationToken cancellationToken)
    {
        return await _videoGameRepository.AddVideoGameAsync(request.NewVideoGame);
    }
}
