using Application.DTOs.CarDTO.CarsPageCarsDTO;
using Application.DTOs.CarDTO.MyCarsDTO;
using Application.Repositories.ICarRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories.CarRepositories
{
    public class CarReadRepositories : ReadRepository<Car>, ICarReadRepositories
    {
        private readonly CarSalesPlatformDbContext _context;
        public CarReadRepositories(CarSalesPlatformDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<int> CountAsync(Expression<Func<Car, bool>> filter)
        {
            return await _context.Set<Car>().CountAsync(filter);
        }

        public IEnumerable<MyCarDTO> GetCarsByUserId(Guid userId, int skip, int take)
        {
            return _context.Cars
                .Where(car => car.UserId == userId)
                .OrderBy(car => car.Name)
                .Skip(skip)
                .Take(take)
                .Select(car => new MyCarDTO
                                            {
                                                Id = car.Id,
                                                Name = car.Name,
                                                IsActive = car.CarIsActive
                                            }).ToList();
        }

        public IEnumerable<CarsPageCarsDTO> GetCarsByFilter(
                                                     Guid? colorId,
                                                     Guid? brandId,
                                                     Guid? categoryId,
                                                     Guid? fuelTypeId,
                                                     Guid? gearTypeId,
                                                     int minPrice,
                                                     int maxPrice,
                                                     int skip,
                                                     int take)
        {
            var query = _context.Cars.AsQueryable();

            // Sadece CarIsActive == true olanları getir
            query = query.Where(car => car.CarIsActive);

            // Diğer ID'ler için de benzer filtreler ekleyebiliriz
            if (colorId.HasValue)
            {
                query = query.Where(car => car.ColorId == colorId.Value);
            }

            if (brandId.HasValue)
            {
                query = query.Where(car => car.BrandId == brandId.Value);
            }

            if (categoryId.HasValue)
            {
                query = query.Where(car => car.CategoryId == categoryId.Value);
            }

            if (fuelTypeId.HasValue)
            {
                query = query.Where(car => car.FuelTypeId == fuelTypeId.Value);
            }

            if (gearTypeId.HasValue)
            {
                query = query.Where(car => car.GearTypeId == gearTypeId.Value);
            }

            // Fiyat aralığı için filtreleme
            query = query.Where(car => car.Price >= minPrice && car.Price <= maxPrice);

            return query
                .OrderBy(car => car.Name)
                .Skip(skip)
                .Take(take)
                .Select(car => new CarsPageCarsDTO
                {
                    CarId = car.Id,
                    Name = car.Name,
                    Mileage = car.Mileage,
                    Price = car.Price,
                    Year = car.Year,
                    Location = car.Location,
                    FuelType=car.FuelType.Type,
                    GearType=car.GearType.Type,

                })
                .ToList();
        }


    }
}
