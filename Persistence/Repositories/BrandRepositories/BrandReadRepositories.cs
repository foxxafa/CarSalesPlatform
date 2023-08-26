using Application.Repositories.IBrandRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.BrandRepositories
{
    public class BrandReadRepositories : ReadRepository<Brand>, IBrandReadRepositories
    {
        public BrandReadRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
