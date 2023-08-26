using Application.DTOs.CarDTO.CarsPageCarsDTO;
using Application.DTOs.PaginationDTO;
using Application.Repositories.ICarRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetCarsByFilter
{
    public class GetCarsByFilterQueryHandler : IRequestHandler<GetCarsByFilterQueryRequest, DataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>>
    {
        private readonly ICarReadRepositories _carReadRepositories;
        public GetCarsByFilterQueryHandler(ICarReadRepositories carReadRepositories)
        {
            _carReadRepositories = carReadRepositories;
        }

        public async Task<DataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>> Handle(GetCarsByFilterQueryRequest request, CancellationToken cancellationToken)
        {
            int skip=(request.Page - 1) * request.ItemsPerPage;
            int take = request.ItemsPerPage;

            var cars = _carReadRepositories.GetCarsByFilter(
                request.ColorId,
                request.BrandId,
                request.CategoryId,
                request.FuelTypeId,
                request.GearTypeId,
                request.MinPrice,
                request.MaxPrice,
                skip,
                take);

            int totalCars = _carReadRepositories.GetAll().Count();
            int totalPages = (int)Math.Ceiling((double)totalCars / request.ItemsPerPage);

            PaginationDTO pagination = new PaginationDTO()
            {
                CurrentPage = request.Page,
                TotalPages = totalPages,
            };

            return new SuccessDataResult<(PaginationDTO, IEnumerable<CarsPageCarsDTO>, int)>((pagination, cars, totalCars), "Başarılı");

            //return Task.FromResult(new SuccessDataResult<(PaginationDTO, IEnumerable<MyCarDTO>, int)>((pagination, cars, totalCars), "Başarılı") as DataResult<(PaginationDTO, IEnumerable<MyCarDTO>, int)>);

        }
    }
}
