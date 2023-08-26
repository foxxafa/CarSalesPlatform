using Application.Repositories.ICategoryRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CategoryRepositories
{
    public class CategoryWriteRepositories : WriteRepository<Category>, ICategoryWriteRepositories
    {
        public CategoryWriteRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
