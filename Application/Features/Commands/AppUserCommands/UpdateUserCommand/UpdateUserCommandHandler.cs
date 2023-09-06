using Application.Abstractions.Services;
using Application.DTOs.User.UserDTO;
using Application.Results;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUserCommands.UpdateUserCommand
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommandRequest, Result>
    {
        readonly IUserService _userService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UpdateUserCommandHandler(IUserService userService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userService = userService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Result> Handle(UpdateUserCommandRequest request, CancellationToken cancellationToken)
        {
            var response = await _userService.UpdateAsync(new UserDTO
            {
                UserId = request.UserId,
                Email = request.Email,
                NameSurname = request.NameSurname,
                //Address=request.Address,
                //City = request.City,
                PhoneNumber = request.PhoneNumber,
                //Country = request.Country,
                //BirthDate = request.BirthDate,

            });

            if (response == null)
                return new ErrorResult("Kullanıcı güncellenemedi");

            if (request.Password != null && request.Password != "")
            {
                if (request.Password == request.PasswordConfirm)
                {
                    var user = await _userManager.FindByIdAsync(request.UserId);

                    // Mevcut şifreyi kontrol et
                    var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, request.CurrentPassword);
                    if (!isCurrentPasswordValid)
                    {
                        return new ErrorResult("Mevcut şifreniz doğru değil");
                    }

                    // Şifreyi değiştir
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.Password);
                    if (!changePasswordResult.Succeeded)
                    {
                        return new ErrorResult("Şifreniz güncellenemedi");
                    }

                    return new SuccessResult("Şifreniz ve bilgileriniz güncellendi");
                }
                else
                {
                    return new ErrorResult("Şifre ve doğrulama şifresi eşit değil");
                }
            }

            return new SuccessResult("Kullanıcı güncellendi");
        }
    }
}
