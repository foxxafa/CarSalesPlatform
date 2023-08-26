using Application.Repositories;
using Application.Repositories.IColorRepositories;
using Application.Repositories.IGearTypeRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ColorRepositories
{
    public class ColorReadRepositories : ReadRepository<Color>, IColorReadRepositories
    {
        public ColorReadRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
