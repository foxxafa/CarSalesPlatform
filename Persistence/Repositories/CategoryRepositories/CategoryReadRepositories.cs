using Application.Repositories.ICategoryRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CategoryRepositories
{
    public class CategoryReadRepositories : ReadRepository<Category>, ICategoryReadRepositories
    {
        public CategoryReadRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
