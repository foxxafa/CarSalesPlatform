using Application.Features.Commands.AppUserCommands.UpdateUserCommand;
using FluentValidation;
using System.Data;

namespace Application.Validators
{
    public class ProfileSettingValidator : AbstractValidator<UpdateUserCommandRequest>
    {
        public ProfileSettingValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email Gerekli");
            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon Numarası Gerekli");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad Soyad Gerekli");
        }
    }
}
 