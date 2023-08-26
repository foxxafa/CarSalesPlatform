using Application.Results;
using MediatR;

namespace Application.Features.Commands.CarCommands.DeleteCar
{
    public class DeleteCarCommandRequest : IRequest<Result>
    {
        public string carId { get; set; }
    }
}
