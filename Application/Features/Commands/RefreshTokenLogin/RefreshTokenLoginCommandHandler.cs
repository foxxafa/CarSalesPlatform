using Application.Abstractions.Services;
using Application.DTOs.Token;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, DataResult<Token>>
    {
        readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<DataResult<Token>> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            Token token= await _authService.RefreshTokenLoginAsync(request.RefreshToken);

            if (token == null)
                return new ErrorDataResult<Token>("Başarısız");

            return new SuccessDataResult<Token>(token,"Başarılı");
        }
    }
}
