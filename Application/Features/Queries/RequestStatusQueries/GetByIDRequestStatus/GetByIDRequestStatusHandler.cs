using Application.Repositories.IRequestStatusRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.RequestStatusQueries.GetByIDRequestStatus
{
    public class GetByIDRequestStatusHandler : IRequestHandler<GetByIDRequestStatusRequest, DataResult<RequestStatus>>
    {
        readonly IRequestStatusReadRepositories _requestStatusReadRepositories;

        public GetByIDRequestStatusHandler(IRequestStatusReadRepositories requestStatusReadRepositories)
        {
            _requestStatusReadRepositories = requestStatusReadRepositories;
        }

        public async Task<DataResult<RequestStatus>> Handle(GetByIDRequestStatusRequest request, CancellationToken cancellationToken)
        {
            var requestStatus = await _requestStatusReadRepositories.GetByIdAsync(request.Id, false);

            if (requestStatus==null)
                return new ErrorDataResult<RequestStatus>();

            return new SuccessDataResult<RequestStatus>();
        }
    }
}
