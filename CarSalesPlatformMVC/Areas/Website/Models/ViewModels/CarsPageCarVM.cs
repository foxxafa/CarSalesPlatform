using Application.DTOs.CarDTO.CarsPageCarsDTO;

namespace CarSalesPlatformMVC.Areas.Website.Models.ViewModels
{
    public class CarsPageCarVM
    {
        public PaginationVM Pagination { get; set; }
        public IEnumerable<CarsPageCarsDTO> Car { get; set; }
        public List<string> CarsImage { get; set; }
        public int TotalCars { get; set; }
    }
}
