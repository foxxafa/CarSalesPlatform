using Application.Repositories.IGearTypeRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.GearTypeRepositories
{
    public class GearTypeReadRepositories : ReadRepository<GearType>, IGearTypeReadRepositories
    {
        public GearTypeReadRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
