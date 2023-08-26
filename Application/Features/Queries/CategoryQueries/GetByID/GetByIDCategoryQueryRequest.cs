using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.CategoryQueries.GetByID
{
    public class GetByIDCategoryQueryRequest : IRequest<DataResult<Category>>
    {
        public string Id { get; set; }
    }

}
