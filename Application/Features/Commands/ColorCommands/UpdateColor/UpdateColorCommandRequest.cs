using Application.Results;
using MediatR;

namespace Application.Features.Commands.ColorCommands.UpdateColor
{
    public class UpdateColorCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
