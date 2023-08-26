using Application.Results;
using MediatR;

namespace Application.Features.Commands.ColorCommands.DeleteColor
{
    public class DeleteColorCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
    }


}
