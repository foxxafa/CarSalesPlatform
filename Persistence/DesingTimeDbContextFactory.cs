using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Persistence.Contexts;

namespace Persistence
{
    public class DesingTimeDbContextFactory : IDesignTimeDbContextFactory<CarSalesPlatformDbContext>
    {
        public CarSalesPlatformDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CarSalesPlatformDbContext>();
            optionsBuilder.UseSqlServer(Configuration.ConnectionString);

            return new CarSalesPlatformDbContext(optionsBuilder.Options);
        }
    }
}
