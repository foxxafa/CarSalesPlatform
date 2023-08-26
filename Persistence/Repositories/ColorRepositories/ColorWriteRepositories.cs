using Application.Repositories.IColorRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ColorRepositories
{
    public class ColorWriteRepositories : WriteRepository<Color>, IColorWriteRepositories
    {
        public ColorWriteRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
