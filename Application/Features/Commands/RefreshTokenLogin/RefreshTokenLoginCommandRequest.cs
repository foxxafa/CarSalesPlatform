using Application.DTOs.Token;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.RefreshTokenLogin
{
    public class RefreshTokenLoginCommandRequest : IRequest<DataResult<Token>>
    {
        public string RefreshToken { get; set; }
    }
}
