using Application.Abstractions.Services;
using Application.DTOs.Token;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.AppUserCommands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, DataResult<(Token,string)>>
    {
        readonly IAuthService _authService;

        public LoginUserCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DataResult<(Token, string)>> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            var dataResult = await _authService.LoginAsync(request.UsernameOrEmail, request.Password, 60*60);

            if (dataResult.IsSuccess)
            {
                return new SuccessDataResult<(Token, string)>(dataResult.Data, dataResult.Message);
            }

            return new ErrorDataResult<(Token, string)>(dataResult.Message);
        }
    }
}
