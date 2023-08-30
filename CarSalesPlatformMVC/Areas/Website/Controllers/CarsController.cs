using Application.Abstractions.Services;
using Application.Features.Queries.CarQueries.GetCarsByFilter;
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
            if (!_cache.TryGetValue("Brands", out List<Brand> cachedBrands))
            {
                cachedBrands = (await _carService.GetCarBrandsAsync()).Data.ToList();
                _cache.Set("Brands", cachedBrands);
            }
            ViewBag.Brands = cachedBrands;

            if (!_cache.TryGetValue("Colors", out List<Color> cachedColors))
            {
                cachedColors = (await _carService.GetCarColorsAsync()).Data.ToList();
                _cache.Set("Colors", cachedColors);
            }
            ViewBag.Colors = cachedColors;

            if (!_cache.TryGetValue("FuelTypes", out List<FuelType> cachedFuelTypes))
            {
                cachedFuelTypes = (await _carService.GetCarFuelTypesAsync()).Data.ToList();
                _cache.Set("FuelTypes", cachedFuelTypes);
            }
            ViewBag.FuelTypes = cachedFuelTypes;

            if (!_cache.TryGetValue("Categories", out List<Category> cachedCategories))
            {
                cachedCategories = (await _carService.GetCarCategoriesAsync()).Data.ToList();
                _cache.Set("Categories", cachedCategories);
            }
            ViewBag.Categories = cachedCategories;

            if (!_cache.TryGetValue("GearTypes", out List<GearType> cachedGearTypes))
            {
                cachedGearTypes = (await _carService.GetCarGearTypesAsync()).Data.ToList();
                _cache.Set("GearTypes", cachedGearTypes);
            }
            ViewBag.GearTypes = cachedGearTypes;

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
