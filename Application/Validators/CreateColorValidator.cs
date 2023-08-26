using Application.Features.Commands.ColorCommands.CreateColor;
using FluentValidation;

namespace Application.Validators
{
    public class CreateColorValidator : AbstractValidator<CreateColorCommandRequest>
    {
        public CreateColorValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Renk adı boş bırakılamaz.")
                .Length(2, 50).WithMessage("Renk adı en az 2, en fazla 50 karakter olmalıdır.");
        }
    }
}
