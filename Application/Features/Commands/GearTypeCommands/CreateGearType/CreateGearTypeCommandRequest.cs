using Application.Results;
using MediatR;

namespace Application.Features.Commands.GearTypeCommands.CreateGearType
{
    public class CreateGearTypeCommandRequest : IRequest<Result>
    {
        public string Type { get; set; }
    }
}
