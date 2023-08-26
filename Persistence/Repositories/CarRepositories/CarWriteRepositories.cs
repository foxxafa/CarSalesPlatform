using Application.Repositories.ICarRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CarRepositories
{
    public class CarWriteRepositories : WriteRepository<Car>, ICarWriteRepositories
    {
        public CarWriteRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
