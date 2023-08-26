using Application.Results;
using MediatR;

namespace Application.Features.Commands.AppUserCommands.DeleteUserCommand
{
    public class DeleteUserCommandRequest : IRequest<Result>
    {
        public Guid UserId { get; set; }
    }
}
