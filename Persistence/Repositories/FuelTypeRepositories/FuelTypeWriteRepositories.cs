using Application.Repositories.IFuelTypeRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.FuelTypeRepositories
{
    public class FuelTypeWriteRepositories : WriteRepository<FuelType>, IFuelTypeWriteRepositories
    {
        public FuelTypeWriteRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
