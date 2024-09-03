using MediatR;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Queries;

public record GetVideoGamesQuery() : IRequest<ServiceResponse<ICollection<VideoGameResponse>>>;
