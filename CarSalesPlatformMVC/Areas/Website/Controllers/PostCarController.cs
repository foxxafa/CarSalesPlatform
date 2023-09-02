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

            ViewBag.Brands = _cache.Get<List<Brand>>("Brands");
            ViewBag.Colors = _cache.Get<List<Color>>("Colors");
            ViewBag.FuelTypes = _cache.Get<List<FuelType>>("FuelTypes");
            ViewBag.Categories = _cache.Get<List<Category>>("Categories");
            ViewBag.GearTypes = _cache.Get<List<GearType>>("GearTypes");

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
