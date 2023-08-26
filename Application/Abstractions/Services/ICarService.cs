using Application.Results;
using Domain.Entities;

namespace Application.Abstractions.Services
{
    public interface ICarService
    {
        Task<Result> CreateCarAsync(Car car);
        Task<Result> UpdateCarAsync(Car car);
        Task<Result> DeleteCarAsync(Guid carId);



        Task<DataResult<List<Brand>>> GetCarBrandsAsync();
        Task<DataResult<List<Color>>> GetCarColorsAsync();
        Task<DataResult<List<FuelType>>> GetCarFuelTypesAsync();
        Task<DataResult<List<GearType>>> GetCarGearTypesAsync();
        Task<DataResult<List<Category>>> GetCarCategoriesAsync();
    }
}
