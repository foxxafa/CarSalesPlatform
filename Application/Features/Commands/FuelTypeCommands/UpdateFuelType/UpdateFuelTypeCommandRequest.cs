using Application.Results;
using MediatR;

namespace Application.Features.Commands.FuelTypeCommands.UpdateFuelType
{
    public class UpdateFuelTypeCommandRequest :IRequest<Result>
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
}
