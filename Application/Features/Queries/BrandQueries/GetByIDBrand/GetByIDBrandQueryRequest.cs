using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.BrandQueries.GetByIDBrand
{
    public class GetByIDBrandQueryRequest : IRequest<DataResult<Brand>>
    {
        public string Id { get; set; }
    }
}
