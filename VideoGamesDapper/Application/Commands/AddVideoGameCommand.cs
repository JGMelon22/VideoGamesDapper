using MediatR;
using VideoGamesDapper.DTOs;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Command;

public record AddVideoGameCommand(VideoGameInput NewVideoGame) : IRequest<ServiceResponse<int>>;
