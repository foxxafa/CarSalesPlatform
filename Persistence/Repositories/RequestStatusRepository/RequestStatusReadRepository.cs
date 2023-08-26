using Application.Repositories.IGearTypeRepositories;
using Application.Repositories.IRequestStatusRepositories;
using Domain.Entities;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.RequestStatusRepository
{
    public class RequestStatusReadRepository : ReadRepository<RequestStatus>, IRequestStatusReadRepositories
    {
        public RequestStatusReadRepository(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
