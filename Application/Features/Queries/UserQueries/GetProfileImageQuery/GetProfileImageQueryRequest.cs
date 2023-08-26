using Application.Results;
using MediatR;

namespace Application.Features.Queries.UserQueries.GetProfileImageQuery
{
    public class GetProfileImageQueryRequest : IRequest<DataResult<string>>
    {
        public string UserId { get; set; }
    }
}
