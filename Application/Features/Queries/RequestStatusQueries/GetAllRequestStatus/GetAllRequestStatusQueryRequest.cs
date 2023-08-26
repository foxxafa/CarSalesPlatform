using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.RequestStatusQueries.GetAllRequestStatus
{
    public class GetAllRequestStatusQueryRequest :IRequest<DataResult<List<RequestStatus>>>
    {

    }

}
