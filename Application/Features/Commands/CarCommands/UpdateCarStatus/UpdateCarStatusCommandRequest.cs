using Application.Results;
using MediatR;

namespace Application.Features.Commands.CarCommands.UpdateCarStatus
{
    public class UpdateCarStatusCommandRequest : IRequest<DataResult<bool>>
    {
        public Guid CarId { get; set; }
    }

}
