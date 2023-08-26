using Domain.Entities.Common;

namespace Domain.Entities
{
    public class FuelType : BaseEntity
    {
        public string Type { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
