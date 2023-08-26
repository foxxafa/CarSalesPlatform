using Application.Features.Commands.BrandCommands.CreateBrand;
using FluentValidation;

namespace Application.Validators
{
    public class CreateBrandValidator : AbstractValidator<CreateBrandCommandRequest>
    {
        public CreateBrandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Marka adı boş bırakılamaz.")
                .Length(2, 50).WithMessage("Marka adı en az 2, en fazla 50 karakter olmalıdır.");
        }
    }
}
