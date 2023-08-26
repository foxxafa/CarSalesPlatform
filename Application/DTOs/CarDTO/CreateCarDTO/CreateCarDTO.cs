namespace Application.DTOs.CarDTO.CreateCarDTO
{
    public class CreateCarDTO
    {
        public Guid CarId { get; set; }
        public Guid ColorId { get; set; }
        public Guid UserId { get; set; }
        public Guid FuelTypeId { get; set; }
        public Guid GearTypeId { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }

        public string Location { get; set; }
        public string CarHeader { get; set; }
        public decimal Price { get; set; }
        public decimal Mileage { get; set; }
        public int Year { get; set; }
        public int SeatCount { get; set; }
        public decimal FuelTankCapacity { get; set; }
        public string EngineDescription { get; set; }
        //public string EngineType { get; set; } fueltype yerine bunu kullanmak lazım aslında
        public string Description { get; set; }
        //public string Version { get; set; }
        public string? Name { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }


        public bool airConditioner { get; set; }
        public bool door { get; set; }
        public bool antiLock { get; set; }
        public bool brake { get; set; }
        public bool steering { get; set; }
        public bool airbag { get; set; }
        public bool windows { get; set; }
        public bool passengerAirbag { get; set; }
        public bool player { get; set; }
        public bool sensor { get; set; }
        public bool seats { get; set; }
        public bool engineWarning { get; set; }
        public bool locking { get; set; }
        public bool headlamps { get; set; }
    }
}
