using Application.Results;
using MediatR;

namespace Application.Features.Commands.AppUserCommands.DeleteUserImageCommand
{
    public class DeleteUserImageCommandRequest : IRequest<Result>
    {
        public string UserId { get; set; }
    }

}
