using Application.Abstractions.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    public class HomeController : Controller
    {
        readonly IMediator _mediator;
        readonly ICarService _carService;

        public HomeController(IMediator mediator, ICarService carService)
        {
            _mediator = mediator;
            _carService = carService;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Brands = (await _carService.GetCarBrandsAsync()).Data.ToList();
            ViewBag.Colors = (await _carService.GetCarColorsAsync()).Data.ToList();
            ViewBag.FuelTypes = (await _carService.GetCarFuelTypesAsync()).Data.ToList();
            ViewBag.Categories = (await _carService.GetCarCategoriesAsync()).Data.ToList();
            ViewBag.GearTypes = (await _carService.GetCarGearTypesAsync()).Data.ToList();

            return View();
        }
    }
}
