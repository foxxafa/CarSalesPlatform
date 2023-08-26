using Application.Abstractions.Services;
using Application.Results;
using MediatR;
namespace Application.Features.Commands.AppUserCommands.DeleteUserCommand
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, Result>
    {
        readonly IUserService _userService;

        public DeleteUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<Result> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.DeleteAsync(request.UserId);
            return response;
        }
    }
}
