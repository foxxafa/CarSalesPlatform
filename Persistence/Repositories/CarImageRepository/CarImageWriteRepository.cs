using Application.Repositories.ICarImageRepository;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CarImageRepository
{
    public class CarImageWriteRepository : WriteRepository<CarImage>, ICarImageWriteRepository
    {
        public CarImageWriteRepository(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
