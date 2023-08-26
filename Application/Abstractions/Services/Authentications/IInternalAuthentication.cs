using Application.Results;

namespace Application.Abstractions.Services.Authentications
{
    public interface IInternalAuthentication // iç kaynaktan gelen login
    {
        Task<DataResult<(DTOs.Token.Token, string)>> LoginAsync(string UsernameOrEmail, string Password,int accessTokenLifeTime);
        Task<DTOs.Token.Token> RefreshTokenLoginAsync(string refreshToken);
    }
}
