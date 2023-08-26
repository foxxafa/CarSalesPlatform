using Application.DTOs.Token;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.AppUserCommands.LoginUser
{
    public class LoginUserCommandRequest : IRequest<DataResult<(Token,string)>>
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
