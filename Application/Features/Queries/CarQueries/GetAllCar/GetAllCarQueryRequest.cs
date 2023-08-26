using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetAllCar
{
    public class GetAllCarQueryRequest : IRequest<DataResult<List<Car>>>
    {
        
    }
}
