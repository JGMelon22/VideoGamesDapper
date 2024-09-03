using MediatR;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Queries;

public record GetVideoGameByIdQuery(int Id) : IRequest<ServiceResponse<VideoGameResponse>>;
