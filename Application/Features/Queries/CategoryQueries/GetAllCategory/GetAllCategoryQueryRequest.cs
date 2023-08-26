using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.CategoryQueries.GetAllCategory
{
    public class GetAllCategoryQueryRequest:IRequest<DataResult<List<Category>>>
    {
    }
}
