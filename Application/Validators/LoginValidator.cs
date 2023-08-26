using Application.Features.Commands.AppUserCommands.LoginUser;
using FluentValidation;

namespace Application.Validators
{
    public class LoginValidator : AbstractValidator<LoginUserCommandRequest>
    {
        public LoginValidator()
        {
            RuleFor(x => x.UsernameOrEmail).NotEmpty().WithMessage("Kullanıcı adı veya e-posta gereklidir.");
            RuleFor(x => x.UsernameOrEmail).NotNull();

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre gereklidir.");
            RuleFor(x => x.Password).NotNull();

        }
    }
}
