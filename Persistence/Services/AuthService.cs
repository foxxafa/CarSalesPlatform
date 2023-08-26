using Application.Abstractions.Services;
using Application.Abstractions.Token;
using Application.DTOs.Token;
using Application.Results;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        readonly ITokenHandler _tokenHandler;
        readonly UserManager<AppUser> _userManager;
        readonly SignInManager<AppUser> _SignInManager;
        readonly IUserService _userService;

        public AuthService(ITokenHandler tokenHandler, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _tokenHandler = tokenHandler;
            _userManager = userManager;
            _SignInManager = signInManager;
            _userService = userService;
        }

        public async Task<DataResult<(Token, string)>> LoginAsync(string UsernameOrEmail, string Password,int accessTokenLifeTime)
        {
            AppUser user = await _userManager.FindByNameAsync(UsernameOrEmail);

            if (user == null) user = await _userManager.FindByEmailAsync(UsernameOrEmail);

            if (user == null) return new ErrorDataResult<(Token,string)>("Kullanıcı bulunamadı");

            SignInResult result = await _SignInManager.CheckPasswordSignInAsync(user, Password, false);

            if (result.Succeeded)
            {
                Token token = await _tokenHandler.CreateAccessToken(accessTokenLifeTime, user);
                await _userService.UpdateRefreshToken(token.RefreshToken, user, token.Expiration, 90);

                return new SuccessDataResult<(Token, string)>((token, user.NameSurname));

            }
            else
                return new ErrorDataResult<(Token, string)>("Kimlik doğrulama hatası");
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
            AppUser? user=await _userManager.Users.SingleOrDefaultAsync(x => x.RefreshToken == refreshToken);

            if (user != null && user?.RefreshTokenEndTime>DateTime.UtcNow)
            {
                Token token=await _tokenHandler.CreateAccessToken(90,user);
                await _userService.UpdateRefreshToken(token.RefreshToken,user, token.Expiration, 90);
                return token;   
            }
            else
                throw new Exception("Kullanıcı bulunamadı");
        }
    }
}
