using Application.Results;
using MediatR;

namespace Application.Features.Commands.RequestStatusCommands.DeleteRequestStatus
{
    public class DeleteRequestStatusCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
