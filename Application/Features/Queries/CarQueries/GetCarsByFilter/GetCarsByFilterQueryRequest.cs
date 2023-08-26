using Application.DTOs.CarDTO.CarsPageCarsDTO;
using Application.DTOs.PaginationDTO;
using Application.Results;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetCarsByFilter
{
    public class GetCarsByFilterQueryRequest : IRequest<DataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>>
    {
        public Guid? ColorId { get; set; }
        public Guid? BrandId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? FuelTypeId { get; set; }
        public Guid? GearTypeId { get; set; }
        public int MinPrice { get; set; }
        public int MaxPrice { get; set; }

        public int Page { get; set; }
        public int ItemsPerPage { get; set; } = 12;
    }
}
