using Application.Repositories.IFuelTypeRepositories;
using Application.Repositories.IGearTypeRepositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.FuelTypeRepositories
{
    public class FuelTypeReadRepositories : ReadRepository<FuelType>, IFuelTypeReadRepositories
    {
        public FuelTypeReadRepositories(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
