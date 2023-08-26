using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetImagesByCarId
{
    public class GetImagesByCarIdQueryRequest : IRequest<DataResult<IEnumerable<CarImage>>>
    {
        public string CarId { get; set; }
    }
}
