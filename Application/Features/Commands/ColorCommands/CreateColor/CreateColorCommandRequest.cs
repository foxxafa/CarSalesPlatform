using Application.Results;
using MediatR;

namespace Application.Features.Commands.ColorCommands.CreateColor
{
    public class CreateColorCommandRequest : IRequest<Result>
    {
        public string Name { get; set; }
    }
}
