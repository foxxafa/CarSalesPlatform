using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.FuelTypeQueries.GetAllFuelType
{
    public class GetAllFuelTypeQueryRequest : IRequest<DataResult<List<FuelType>>>
    {

    }
}
