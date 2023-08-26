using Application.Repositories.ICarImageRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CarImageRepository
{
    public class CarImageReadRepository : ReadRepository<CarImage>, ICarImageReadRepository
    {
        public CarImageReadRepository(CarSalesPlatformDbContext context) : base(context)
        {

        }
    }
}
