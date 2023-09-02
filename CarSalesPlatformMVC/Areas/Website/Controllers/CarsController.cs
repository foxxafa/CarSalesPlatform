using Application.Abstractions.Services;
using Application.Features.Queries.CarQueries.GetCarsByFilter;
using Application.Features.Queries.CarQueries.GetCarsBySearchFilter;
using Application.Features.Queries.CarQueries.GetImagesByCarId;
using CarSalesPlatformMVC.Areas.Website.Models.ViewModels;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    public class CarsController : Controller
    {
        readonly IMediator _mediator;
        readonly ICarService _carService;
        private IMemoryCache _cache;

        public CarsController(IMediator mediator, ICarService carService, IMemoryCache cache)
        {
            _mediator = mediator;
            _carService = carService;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Brands = _cache.Get<List<Brand>>("Brands");
            ViewBag.Colors = _cache.Get<List<Color>>("Colors");
            ViewBag.FuelTypes = _cache.Get<List<FuelType>>("FuelTypes");
            ViewBag.Categories = _cache.Get<List<Category>>("Categories");
            ViewBag.GearTypes = _cache.Get<List<GearType>>("GearTypes");

            return View();
        }

        [HttpGet("[controller]/[action]")]
        public async Task<IActionResult> GetCarsBySearchFilter(GetCarsBySearchFilterQueryRequest request)
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
                imagePathList.Add(imageResponse.Data.SingleOrDefault(x => x.IsCover == true).ImagePath);
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
