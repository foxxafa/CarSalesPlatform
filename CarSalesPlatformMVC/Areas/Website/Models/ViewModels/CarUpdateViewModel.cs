using Application.Features.Commands.CarCommands.UpdateCar;
using Domain.Entities;

namespace CarSalesPlatformMVC.Areas.Website.Models.ViewModels
{
    public class CarUpdateViewModel
    {
        public UpdateCarCommandRequest CarRequest { get; set; }
        public IEnumerable<CarImage> CarImages { get; set; }

    }

}
