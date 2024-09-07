using MediatR;
using VideoGamesDapper.Models;

namespace VideoGamesDapper.Application.Command;

public record RemoveVideoGameCommand(int Id) : IRequest<ServiceResponse<int>>;
