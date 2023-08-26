using Application.Abstractions.Services;
using Application.DTOs.User.CreateUserDTO;
using Application.DTOs.User.UpdateUserDTO;
using Application.Results;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> CreateAsync(CreateUser user)
        {
            AppUser cuser = new AppUser()
            {
                Id = Guid.NewGuid(),
                Email = user.Email,
                UserName = user.Email,
                NameSurname = user.NameSurname
            };

            var existingUser = _userManager.FindByEmailAsync(cuser.Email).Result;

            if (existingUser != null)
                return new ErrorResult("Bu EPostaya kayıtlı kullanıcı mevcut!");


            IdentityResult result = await _userManager.CreateAsync(cuser, user.Password);

            if (!result.Succeeded)
            {
                var errorMessages = result.Errors.Select(error => $"{error.Code} - {error.Description}").ToList();
                return new ErrorResult(errorMessages);
            }

            var roleResult = await _userManager.AddToRoleAsync(cuser, "User");

            if (!roleResult.Succeeded)
            {
                // Rol ekleme başarısız oldu, hata döndürebiliriz
                return new ErrorResult("Rol ekleme başarısız!");
            }

            return new SuccessResult("kullanıcı oluşturuldu");
        }

        public async Task<Result> UpdateAsync(UserDTO user)
        {
            var existingUser = await _userManager.FindByIdAsync(user.UserId);
            if (existingUser == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı.");
            }

            existingUser.Email = user.Email;
            existingUser.NameSurname = user.NameSurname;
            //existingUser.Address = user.Address;
            existingUser.PhoneNumber = user.PhoneNumber;
            //existingUser.BirthDate = user.BirthDate;
            //existingUser.City = user.City;
            //existingUser.Country = user.Country;

            var result = await _userManager.UpdateAsync(existingUser);

            if (result.Succeeded)
            {
                return new SuccessResult("Kullanıcı güncellendi.");
            }
            else
            {
                var errorMessages = result.Errors.Select(error => $"{error.Code} - {error.Description}").ToList();
                return new ErrorResult(errorMessages);
            }
        }


        public async Task<Result> DeleteAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return new ErrorResult("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.DeleteAsync(user);


            if (result.Succeeded)
            {
                return new SuccessResult("Kullanıcı silindi.");
            }
            else
            {
                var errorMessages = result.Errors.Select(error => $"{error.Code} - {error.Description}").ToList();
                return new ErrorResult(errorMessages);
            }
        }


        public async Task UpdateRefreshToken(string refreshToken, AppUser user,DateTime accessTokenDate,int addOnAccessTokenDate)
        {
            if (user != null)
            {
                user.RefreshToken = refreshToken;
                user.RefreshTokenEndTime = accessTokenDate.AddSeconds(addOnAccessTokenDate);
                await _userManager.UpdateAsync(user);
            }
            else
                throw new Exception("kullanıcı bulunamadı");

        }
    }
}
