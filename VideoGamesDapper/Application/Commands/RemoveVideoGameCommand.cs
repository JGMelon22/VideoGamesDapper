using MediatR;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Queries;

public record RemoveVideoGameCommand(int Id) : IRequest<ServiceResponse<bool>>;
