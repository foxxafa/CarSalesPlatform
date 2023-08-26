using Application.Results;
using MediatR;

namespace Application.Features.Commands.FuelTypeCommands.DeleteFuelType
{
    public class DeleteFuelTypeCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
