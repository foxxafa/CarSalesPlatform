using Application.DTOs.CarDTO.CarsPageCarsDTO;
using Application.DTOs.CarDTO.MyCarsDTO;
using Application.Repositories.ICarRepositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Linq.Expressions;
using LinqKit; // PredicateBuilder için bu paketi indirebilirsiniz
using Microsoft.Extensions.Caching.Memory;

namespace Persistence.Repositories.CarRepositories
{
    public class CarReadRepositories : ReadRepository<Car>, ICarReadRepositories
    {
        private readonly CarSalesPlatformDbContext _context;
        private IMemoryCache _cache;
        public CarReadRepositories(CarSalesPlatformDbContext context, IMemoryCache cache) : base(context)
        {
            _context = context;
            _cache = cache;
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


        public IEnumerable<CarsPageCarsDTO> GetCarsBySearchFilter(string searchQuery,int skip,int take)
        {
            var query = _context.Cars.AsQueryable();

            var brands = _cache.Get<List<Brand>>("Brands").Select(b => b.Name.ToLower()).ToList();
            var fuelTypes = _cache.Get<List<FuelType>>("FuelTypes").Select(f => f.Type.ToLower()).ToList();
            var colors = _cache.Get<List<Color>>("Colors").Select(c => c.Name.ToLower()).ToList();
            var categories = _cache.Get<List<Category>>("Categories").Select(c => c.Name.ToLower()).ToList();
            var gearTypes = _cache.Get<List<GearType>>("GearTypes").Select(g => g.Type.ToLower()).ToList();

            var inputKeywords = searchQuery.Split(' ');

            var brandKeywords = inputKeywords.Where(k => brands.Contains(k.ToLower())).ToList();
            var fuelKeywords = inputKeywords.Where(k => fuelTypes.Contains(k.ToLower())).ToList();
            var colorKeywords = inputKeywords.Where(k => colors.Contains(k.ToLower())).ToList();
            var categoryKeywords = inputKeywords.Where(k => categories.Contains(k.ToLower())).ToList();
            var gearTypeKeywords = inputKeywords.Where(k => gearTypes.Contains(k.ToLower())).ToList();

            var predicate = PredicateBuilder.New<Car>(false);

            foreach (var brand in brandKeywords)
            {
                foreach (var color in colorKeywords)
                {
                    var combinedPredicate = PredicateBuilder.New<Car>(true);
                    combinedPredicate = combinedPredicate.And(car => car.Brand.Name.ToLower() == brand);
                    combinedPredicate = combinedPredicate.And(car => car.Color.Name.ToLower() == color);

                    predicate = predicate.Or(combinedPredicate);
                }
            }

            query = query.AsExpandable().Where(predicate).Skip(skip).Take(take);

            // Sonuçları CarId'ye göre benzersiz hale getir
            var uniqueResults = query
                .Select(car => new CarsPageCarsDTO
                {
                    CarId = car.Id,
                    Name = car.Name,
                    Mileage = car.Mileage,
                    Price = car.Price,
                    Year = car.Year,
                    Location = car.Location,
                    FuelType = car.FuelType.Type,
                    GearType = car.GearType.Type,
                })
                .GroupBy(car => car.CarId)
                .Select(group => group.First())
                .ToList();

            return uniqueResults;
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
                                                     int take,
                                                     string sortOrder)
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


            switch (sortOrder)
            {
                case "asc":
                    query = query.OrderBy(car => car.Price);
                    break;
                case "desc":
                    query = query.OrderByDescending(car => car.Price);
                    break;
                // Varsayılan olarak isme göre sırala
                default:
                    query = query.OrderBy(car => car.Name);
                    break;
            }

            return query
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
