using Application.Features.Commands.AppUserCommands.CreateUserCommand;
using FluentValidation;

namespace Application.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta gereklidir.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre gereklidir.");
            RuleFor(x => x.PasswordConfirm).NotEmpty().WithMessage("Şifre doğrulaması gerekli");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad Soyad kısımı boş bırakılamaz");
            RuleFor(x => x).Must(x => x.Password == x.PasswordConfirm)
                .WithMessage("Şifre ve şifre doğrulaması eşleşmiyor.");

        }
    }
}
