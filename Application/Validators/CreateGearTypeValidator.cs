using Application.Features.Commands.GearTypeCommands.CreateGearType;
using FluentValidation;

namespace Application.Validators
{
    public class CreateGearTypeValidator : AbstractValidator<CreateGearTypeCommandRequest>
    {
        public CreateGearTypeValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Vites türü adı boş bırakılamaz.")
                .Length(2, 50).WithMessage("Vites türü adı en az 2, en fazla 50 karakter olmalıdır.");
        }
    }
}
