using Application.Results;
using MediatR;

namespace Application.Features.Commands.RequestStatusCommands.UpdateRequestStatus
{
    public class UpdateRequestStatusCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
