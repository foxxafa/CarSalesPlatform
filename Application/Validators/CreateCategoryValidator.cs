using Application.Features.Commands.CategoryCommands.CreateCategory;
using FluentValidation;

namespace Application.Validators
{
    public class CreateCategoryValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Kategori adı boş bırakılamaz.")
                .Length(2, 50).WithMessage("Kategori adı en az 2, en fazla 50 karakter olmalıdır.");
        }
    }
}
