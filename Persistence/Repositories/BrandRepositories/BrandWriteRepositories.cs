using Application.Repositories.IBrandRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.BrandRepositories
{
    public class BrandWriteRepositories : WriteRepository<Brand>, IBrandWriteRepositories
    {
        public BrandWriteRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
