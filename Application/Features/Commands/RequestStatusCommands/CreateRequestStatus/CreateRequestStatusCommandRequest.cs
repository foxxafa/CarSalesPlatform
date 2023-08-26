using Application.Results;
using MediatR;

namespace Application.Features.Commands.RequestStatusCommands.CreateRequestStatus
{
    public class CreateRequestStatusCommandRequest : IRequest<Result>
    {
        public string Name { get; set; }
    }
}

