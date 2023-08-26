using Application.DTOs.CarDTO.MyCarsDTO;

namespace CarSalesPlatformMVC.Areas.Website.Models.ViewModels
{
    public class MyCarsPageVM
    {
        public PaginationVM Pagination { get; set; }
        public IEnumerable<MyCarDTO> Car { get; set; }
        public List<string> CarsImage { get; set; }
        public int TotalCars { get; set; }
    }
}
