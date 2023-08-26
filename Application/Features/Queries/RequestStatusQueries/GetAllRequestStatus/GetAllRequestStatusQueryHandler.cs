using Application.Repositories.IRequestStatusRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.RequestStatusQueries.GetAllRequestStatus
{
    public class GetAllRequestStatusQueryHandler : IRequestHandler<GetAllRequestStatusQueryRequest, DataResult<List<RequestStatus>>>
    {
        readonly IRequestStatusReadRepositories _requestStatusReadRepositories;

        public GetAllRequestStatusQueryHandler(IRequestStatusReadRepositories requestStatusReadRepositories)
        {
            _requestStatusReadRepositories = requestStatusReadRepositories;
        }

        public async Task<DataResult<List<RequestStatus>>> Handle(GetAllRequestStatusQueryRequest request, CancellationToken cancellationToken)
        {
            var requestStatuses = await _requestStatusReadRepositories.GetAll(false).ToListAsync(cancellationToken);

            if (requestStatuses.Any())
                return new SuccessDataResult<List<RequestStatus>>(requestStatuses);

            return new ErrorDataResult<List<RequestStatus>>();
        }
    }

}
