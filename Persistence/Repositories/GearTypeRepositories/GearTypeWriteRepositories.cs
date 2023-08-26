using Application.Repositories.IGearTypeRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.GearTypeRepositories
{
    public class GearTypeWriteRepositories : WriteRepository<GearType>, IGearTypeWriteRepositories
    {
        public GearTypeWriteRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}