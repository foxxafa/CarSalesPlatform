using Application.Results;
using MediatR;

namespace Application.Features.Commands.AppUserCommands.CreateUserCommand
{
    public class CreateUserCommandRequest : IRequest<Result>
    {
        public string NameSurname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
    }
}
