using Application.Features.Commands.CarCommands.CreateCar;
using FluentValidation;

namespace Application.Validators
{
    public class CreateCarValidator : AbstractValidator<CreateCarCommandRequest>
    {
        public CreateCarValidator()
        {

        }
    }
}
