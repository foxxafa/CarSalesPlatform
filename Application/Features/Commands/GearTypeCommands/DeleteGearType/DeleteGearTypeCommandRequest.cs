using Application.Results;
using MediatR;

namespace Application.Features.Commands.GearTypeCommands.DeleteGearType
{
    public class DeleteGearTypeCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
