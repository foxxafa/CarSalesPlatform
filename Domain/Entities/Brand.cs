using Domain.Entities.Common;

namespace Domain.Entities
{

    public class Brand : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
