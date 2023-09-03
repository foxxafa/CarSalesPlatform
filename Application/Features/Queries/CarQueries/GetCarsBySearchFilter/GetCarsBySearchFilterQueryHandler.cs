using Application.DTOs.CarDTO.CarsPageCarsDTO;
using Application.DTOs.PaginationDTO;
using Application.Repositories.ICarRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetCarsBySearchFilter
{
    public class GetCarsBySearchFilterQueryHandler : IRequestHandler<GetCarsBySearchFilterQueryRequest, DataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>>
    {
        private readonly ICarReadRepositories _carReadRepositories;
        public GetCarsBySearchFilterQueryHandler(ICarReadRepositories carReadRepositories)
        {
            _carReadRepositories = carReadRepositories;
        }

        public async Task<DataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>> Handle(GetCarsBySearchFilterQueryRequest request, CancellationToken cancellationToken)
        {
            int skip = (request.Page - 1) * request.ItemsPerPage;
            int take = request.ItemsPerPage;

            var cars = _carReadRepositories.GetCarsBySearchFilter(request.Query,skip,take,request.sortOrder);

            int totalCars = _carReadRepositories.GetAll().Count();
            int totalPages = (int)Math.Ceiling((double)totalCars / request.ItemsPerPage);


            PaginationDTO pagination = new PaginationDTO()
            {
                CurrentPage = request.Page,
                TotalPages = totalPages,
            };

            return new SuccessDataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>((pagination, cars, totalCars), "Başarılı");
        }
    }
}
