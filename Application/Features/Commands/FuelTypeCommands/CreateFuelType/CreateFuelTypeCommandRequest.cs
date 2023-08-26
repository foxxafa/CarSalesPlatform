using Application.Results;
using MediatR;

namespace Application.Features.Commands.FuelTypeCommands.CreateFuelType
{
    public class CreateFuelTypeCommandRequest : IRequest<Result>
    {
        public string Type { get; set; }
    }
}
