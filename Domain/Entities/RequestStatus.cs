using Domain.Entities.Common;

namespace Domain.Entities
{
    public class RequestStatus : BaseEntity 
    {
        public string Name { get; set; }
        public ICollection<Request> Requests { get; set;} 
    }
}
