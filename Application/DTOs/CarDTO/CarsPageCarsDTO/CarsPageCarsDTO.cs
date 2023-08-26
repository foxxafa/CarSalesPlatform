namespace Application.DTOs.CarDTO.CarsPageCarsDTO
{
    public class CarsPageCarsDTO
    {
        public Guid CarId { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        //public string CarHeader { get; set; }
        public string Location { get; set; }
        public string FuelType { get; set; }
        public string GearType { get; set; }
    }
}
