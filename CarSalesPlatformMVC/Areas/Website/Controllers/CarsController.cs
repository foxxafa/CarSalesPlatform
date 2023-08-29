using Application.Abstractions.Services;
using Application.Features.Queries.CarQueries.GetCarsByFilter;
using Application.Features.Queries.CarQueries.GetImagesByCarId;
using CarSalesPlatformMVC.Areas.Website.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    public class CarsController : Controller
    {
        readonly IMediator _mediator;
        readonly ICarService _carService;

        public CarsController(IMediator mediator, ICarService carService)
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

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> GetFiltredCars(GetCarsByFilterQueryRequest request)
        {

            var response = await _mediator.Send(request);

            PaginationVM paginationVM = new PaginationVM()
            {
                CurrentPage = response.Data.Item1.CurrentPage,
                TotalPages = response.Data.Item1.TotalPages
            };

            GetImagesByCarIdQueryRequest imageRequest = new GetImagesByCarIdQueryRequest();

            List<string> imagePathList = new List<string>();
            foreach (var car in response.Data.Item2)
            {
                imageRequest.CarId = car.CarId.ToString();
                var imageResponse = await _mediator.Send(imageRequest);
                imagePathList.Add(imageResponse.Data.SingleOrDefault(x=> x.IsCover==true).ImagePath);
            }

            var viewModel = new CarsPageCarVM
            {
                Pagination = paginationVM,
                Car = response.Data.Item2,
                TotalCars = response.Data.Item3,
                CarsImage = imagePathList
            };

            return Json(viewModel);
        }
    }
}
