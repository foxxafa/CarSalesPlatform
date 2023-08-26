using Application.Abstractions.Services;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.AppUserCommands.CreateUserCommand
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, Result>
    {
        readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            Result response = await _userService.CreateAsync(new()
            {
                Email = request.Email,
                Password = request.Password,
                PasswordConfirm = request.PasswordConfirm,
                NameSurname = request.NameSurname
            });

            return response;
        }
    }
}
