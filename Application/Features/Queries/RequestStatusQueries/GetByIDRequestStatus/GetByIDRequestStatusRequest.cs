using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.RequestStatusQueries.GetByIDRequestStatus
{
    public class GetByIDRequestStatusRequest :IRequest<DataResult<RequestStatus>>
    {
        public string Id { get; set; }
    }
}
