using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.FuelTypeQueries.GetByID
{
    public class GetByIDFuelTypeQueryRequest : IRequest<DataResult<FuelType>>
    {
        public string Id { get; set; }
    }
}
