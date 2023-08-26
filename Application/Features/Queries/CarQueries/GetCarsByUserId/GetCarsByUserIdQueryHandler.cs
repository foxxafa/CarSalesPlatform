using Application.DTOs.CarDTO.MyCarsDTO;
using Application.DTOs.PaginationDTO;
using Application.Repositories.ICarRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetCarsByUserId
{
    public class GetCarsByUserIdQueryHandler : IRequestHandler<GetCarsByUserIdQueryRequest, DataResult<(PaginationDTO, IEnumerable<MyCarDTO>, int)>>
    {
        readonly ICarReadRepositories _carReadRepositories;

        public GetCarsByUserIdQueryHandler(ICarReadRepositories carReadRepositories)
        {
            _carReadRepositories = carReadRepositories;
        }

        public async Task<DataResult<(PaginationDTO, IEnumerable<MyCarDTO>, int)>> Handle(GetCarsByUserIdQueryRequest request, CancellationToken cancellationToken)
        {
            var cars = _carReadRepositories.GetCarsByUserId(request.UserId, (request.Page - 1) * request.ItemsPerPage, request.ItemsPerPage);

            int totalCars = _carReadRepositories.GetWhere(car => car.UserId == request.UserId).Count();
            int totalPages = (int)Math.Ceiling((double)totalCars / request.ItemsPerPage);

            PaginationDTO pagination = new PaginationDTO()
            {
                CurrentPage = request.Page,
                TotalPages = totalPages,
            };

            return new SuccessDataResult<(PaginationDTO, IEnumerable<MyCarDTO>, int)>((pagination, cars, totalCars), "Başarılı");
        }

    }
}
