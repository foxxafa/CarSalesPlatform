using Application.Abstractions.Services;
using Application.Features.Commands.CarCommands.CreateCar;
using Application.Results;
using AutoMapper;
using CarSalesPlatformMVC.Areas.Website.Attributes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    [Authorize]
    public class PostCarController : Controller
    {
        readonly IMediator _mediator;
        readonly ICarService _carService;
        readonly IMapper _mapper;
        public PostCarController(IMediator mediator, ICarService carService, IMapper mapper)
        {
            _mediator = mediator;
            _carService = carService;
            _mapper = mapper;
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

        [HttpPost("[controller]/[action]")]
        [ValidateModel]
        public async Task<IActionResult> CreateCar(CreateCarCommandRequest request)
        {
            var userId = HttpContext.Items["UserId"] as Guid?;
            if (userId.HasValue)
                request.Car.UserId = userId.Value;
            else
                return View(new ErrorResult("Kullanıcı kimliği çözümlenemedi"));

            Result response = await _mediator.Send(request);

            if (response.IsSuccess)
                return Ok(response);

            return View(response);
        }


    }
}
