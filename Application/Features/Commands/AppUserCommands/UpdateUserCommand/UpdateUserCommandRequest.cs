using Application.DTOs.User.UpdateUser;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.AppUserCommands.UpdateUserCommand
{
    public class UpdateUserCommandRequest : UpdateUserDTO, IRequest<Result>
    {
    }
}
