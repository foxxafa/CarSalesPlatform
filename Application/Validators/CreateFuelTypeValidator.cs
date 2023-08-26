using Application.Features.Commands.FuelTypeCommands.CreateFuelType;
using FluentValidation;

namespace Application.Validators
{
    public class CreateFuelTypeValidator : AbstractValidator<CreateFuelTypeCommandRequest>
    {
        public CreateFuelTypeValidator()
        {
            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Yakıt türü adı boş bırakılamaz.")
                .Length(2, 50).WithMessage("Yakıt türü adı en az 2, en fazla 50 karakter olmalıdır.");
        }
    }
}
