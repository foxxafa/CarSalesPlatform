using Application.DTOs.CarDTO.CarsPageCarsDTO;
using Application.DTOs.PaginationDTO;
using Application.Results;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetCarsBySearchFilter
{
    public class GetCarsBySearchFilterQueryRequest : IRequest<DataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>>
    {
        public string sortOrder { get; set; }
        public string Query { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; } = 12;
    }
}
