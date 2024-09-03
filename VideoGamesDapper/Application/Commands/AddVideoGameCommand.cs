using MediatR;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Queries;

public record AddVideoGameCommand(VideoGameInput NewVideoGame) : IRequest<ServiceResponse<VideoGameResponse>>;
