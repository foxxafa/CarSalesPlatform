using Application.DTOs.CarDTO.MyCarsDTO;
using Application.DTOs.PaginationDTO;
using Application.Results;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetCarsByUserId
{
    public class GetCarsByUserIdQueryRequest : IRequest<DataResult<(PaginationDTO, IEnumerable<MyCarDTO>, int)>>
    {
        public Guid UserId { get; set; }
        public int Page { get; set; }
        public int ItemsPerPage { get; set; } = 4;
    }


}
