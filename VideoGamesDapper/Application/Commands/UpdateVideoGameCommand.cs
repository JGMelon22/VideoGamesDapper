using MediatR;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Queries;

public record UpdateVideoGameCommand(int Id, VideoGameInput NewVideoGame) : IRequest<ServiceResponse<VideoGameResponse>>;
