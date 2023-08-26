using Application.Results;
using MediatR;

namespace Application.Features.Commands.CarCommands.DeleteCarImageByCarId
{
    public class DeleteCarImageByCarIdCommandRequest : IRequest<Result>
    {
        public string imageId { get; set; }
    }

}
