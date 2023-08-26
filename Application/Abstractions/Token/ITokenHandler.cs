using Domain.Entities.Identity;
using T = Application.DTOs;

namespace Application.Abstractions.Token
{
    public interface ITokenHandler 
    {
        Task<T.Token.Token> CreateAccessToken(int second,AppUser user);
        string CreateRefreshToken();
    }
}
