using Application.Repositories.IRequestStatusRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.RequestStatusRepository
{
    public class RequestStatusWriteRepository : WriteRepository<RequestStatus>, IRequestStatusWriteRepositories
    {
        public RequestStatusWriteRepository(CarSalesPlatformDbContext context) : base(context)
        {
        }
    }
}
