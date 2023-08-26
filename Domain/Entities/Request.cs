using Domain.Entities.Common;
using Domain.Entities.Identity;
namespace Domain.Entities
{
    public class Request : BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid CarId { get; set; }
        public Car Car { get; set; }

        public Guid RequestStatusId { get; set; }
        public RequestStatus Status { get; set; }
    }
}
