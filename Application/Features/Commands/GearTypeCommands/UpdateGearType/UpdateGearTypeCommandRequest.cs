using Application.Results;
using MediatR;

namespace Application.Features.Commands.GearTypeCommands.UpdateGearType
{
    public class UpdateGearTypeCommandRequest :IRequest<Result>
    {
        public string Id { get; set; }
        public string Type { get; set; }
    }
}
