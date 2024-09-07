using MediatR;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Command;

public record UpdateVideoGameCommand(int Id, VideoGameInput UpdatedVideoGame) : IRequest<ServiceResponse<int>>;
