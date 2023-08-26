using Domain.Entities.Common;

namespace Domain.Entities
{
    public class CarImage:BaseEntity
    {
        public string ImagePath { get; set; }  // Burada resmin yolu veya adı saklanabilir.
        public Guid CarId { get; set; } 
        public Car Car { get; set; }
    }
}
