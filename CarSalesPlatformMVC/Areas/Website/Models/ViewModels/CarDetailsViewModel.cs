using Domain.Entities;

namespace CarSalesPlatformMVC.Areas.Website.Models.ViewModels
{
    public class CarDetailsViewModel
    {
        public ICollection<CarImage> Images { get; set; }
        public string ProfileImagePath { get; set; }
        public string NameSurname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public string CarHeader { get; set; }
        public string Name { get; set; }
        public string ColorName { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public string FuelTypeName { get; set; }
        public string GearTypeName { get; set; }
        public decimal Mileage { get; set; }
        public int Year { get; set; }
        public string EngineDescription { get; set; }
        public int SeatCount { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int FuelTankCapacity { get; set; }

        public string Latitude { get; set; }
        public string Longitude { get; set; }


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
    }

}
