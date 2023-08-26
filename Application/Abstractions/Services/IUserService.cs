using Application.DTOs.User.CreateUserDTO;
using Application.DTOs.User.UpdateUserDTO;
using Application.Results;
using Domain.Entities.Identity;

namespace Application.Abstractions.Services
{
    public interface IUserService
    {
        Task<Result> CreateAsync(CreateUser user);
        Task<Result> UpdateAsync(UserDTO user);
        Task<Result> DeleteAsync(Guid userId);

        Task UpdateRefreshToken(string refreshToken,AppUser user,DateTime accessTokenDate,int addOnAccessTokenDate);
    }
}
