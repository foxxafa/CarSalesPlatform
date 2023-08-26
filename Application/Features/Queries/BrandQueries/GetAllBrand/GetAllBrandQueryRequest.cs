using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.BrandQueries.GetAllBrand
{
    public class GetAllBrandQueryRequest :IRequest<DataResult<List<Brand>>>
    {

    }
}
