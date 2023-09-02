using Application.DTOs.CarDTO.CarsPageCarsDTO;
using Application.DTOs.CarDTO.MyCarsDTO;
using Domain.Entities;

namespace Application.Repositories.ICarRepositories
{
    public interface ICarReadRepositories : IReadRepository<Car>
    {
        IEnumerable<CarsPageCarsDTO> GetCarsBySearchFilter(string searchFilter,int skip,int take);
        IEnumerable<MyCarDTO> GetCarsByUserId(Guid userId, int skip, int take);

        IEnumerable<CarsPageCarsDTO> GetCarsByFilter(
                                          Guid? colorId,
                                          Guid? brandId,
                                          Guid? categoryId,
                                          Guid? fuelTypeId,
                                          Guid? gearTypeId,
                                          int minPrice,
                                          int maxPrice,
                                          int skip,
                                          int take,
                                          string sortOrder);
    }
}
