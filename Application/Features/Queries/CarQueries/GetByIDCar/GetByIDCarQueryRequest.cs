using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetByIDCar
{
    public class GetByIDCarQueryRequest : IRequest<DataResult<Car>>
    {
        public string Id { get; set; }
    }
}
