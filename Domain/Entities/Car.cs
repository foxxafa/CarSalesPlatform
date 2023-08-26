using Domain.Entities.Common;
using Domain.Entities.Identity;
namespace Domain.Entities
{
    public class Car :BaseEntity
    {
        public Guid UserId { get; set; }
        public AppUser User { get; set; }

        public Guid ColorId { get; set; }
        public Color Color { get; set; }

        public Guid FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }

        public Guid GearTypeId { get; set; }
        public GearType GearType { get; set; }

        public Guid BrandId { get; set; }
        public Brand Brand { get; set; }

        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public bool CarIsActive { get; set; }
        public string CarHeader { get; set; }
        public int SeatCount { get; set; }
        public int FuelTankCapacity { get; set; }
        public string EngineDescription { get; set; }
        //public string EngineType { get; set; } fuel type yerine kullanılabilir
        public string Description { get; set; }
        //public string Version { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Location { get; set; }

        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~

        public bool AirConditioner { get; set; }
        public bool Door { get; set; }
        public bool AntiLock { get; set; }
        public bool Brake { get; set; }
        public bool Steering { get; set; }
        public bool Airbag { get; set; }
        public bool Windows { get; set; }
        public bool PassengerAirbag { get; set; }
        public bool Player { get; set; }
        public bool Sensor { get; set; }
        public bool Seats { get; set; }
        public bool EngineWarning { get; set; }
        public bool Locking { get; set; }
        public bool Headlamps { get; set; }


        //~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
        public ICollection<CarImage> Images { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
