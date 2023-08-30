using Application.Abstractions.Services;
using Application.Features.Commands.CarCommands.CreateCar;
using Application.Results;
using AutoMapper;
using CarSalesPlatformMVC.Areas.Website.Attributes;
using CarSalesPlatformMVC.Areas.Website.Models.ViewModels;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    [Authorize]
    public class PostCarController : Controller
    {
        private IMemoryCache _cache;
        readonly IMediator _mediator;
        readonly ICarService _carService;
        readonly IMapper _mapper;
        public PostCarController(IMediator mediator, ICarService carService, IMapper mapper, IMemoryCache cache)
        {
            _mediator = mediator;
            _carService = carService;
            _mapper = mapper;
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

        [HttpPost("[controller]/[action]")]
        [ValidateModel]
        public async Task<IActionResult> CreateCar(CarCreateUpdateFormVM model)
        {
            var userId = HttpContext.Items["UserId"] as Guid?;
            if (userId.HasValue)
                model.Car.UserId = userId.Value;
            else
                return View(new ErrorResult("Kullanıcı kimliği çözümlenemedi"));

            CreateCarCommandRequest createCarCommandRequest = new CreateCarCommandRequest();

            createCarCommandRequest.Car=model.Car;
            createCarCommandRequest.Files = model.Files;
            createCarCommandRequest.CoverIndex = model.CoverIndex;

            Result response = await _mediator.Send(createCarCommandRequest);

            if (response.IsSuccess)
                return Ok(response);

            return View(response);
        }


    }
}
