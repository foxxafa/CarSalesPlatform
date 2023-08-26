using Application.Features.Queries.BrandQueries.GetByIDBrand;
using Application.Features.Queries.CarQueries.GetByIDCar;
using Application.Features.Queries.CarQueries.GetImagesByCarId;
using Application.Features.Queries.CategoryQueries.GetByID;
using Application.Features.Queries.ColorQueries.GetByID;
using Application.Features.Queries.FuelTypeQueries.GetByID;
using Application.Features.Queries.GearTypeQueries.GetByIDGearType;
using CarSalesPlatformMVC.Areas.Website.Models.ViewModels;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    public class CarDetailsController : Controller
    {
        readonly IMediator _mediator;
        readonly UserManager<AppUser> _userManager;

        public CarDetailsController(IMediator mediator, UserManager<AppUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        [HttpGet("[controller]/{carId}")]
        public async Task<IActionResult> Index(string carId)
        {
            GetByIDCarQueryRequest request = new GetByIDCarQueryRequest();
            request.Id = carId;
            var response = await _mediator.Send(request);

            if (response != null)
            {
                GetImagesByCarIdQueryRequest getImagesByCarIdQueryRequest = new GetImagesByCarIdQueryRequest();

                GetByIDColorQueryRequest colorQueryRequest = new GetByIDColorQueryRequest();
                GetByIDBrandQueryRequest brandQueryRequest = new GetByIDBrandQueryRequest();
                GetByIDFuelTypeQueryRequest fuelTypeQueryRequest = new GetByIDFuelTypeQueryRequest();
                GetByIDGearTypeQueryRequest gearTypeQueryRequest = new GetByIDGearTypeQueryRequest();
                GetByIDCategoryQueryRequest categoryQueryRequest = new GetByIDCategoryQueryRequest();

                getImagesByCarIdQueryRequest.CarId = carId;

                colorQueryRequest.Id = response.Data.ColorId.ToString();
                brandQueryRequest.Id = response.Data.BrandId.ToString();
                fuelTypeQueryRequest.Id = response.Data.FuelTypeId.ToString();
                gearTypeQueryRequest.Id = response.Data.GearTypeId.ToString();
                categoryQueryRequest.Id = response.Data.CategoryId.ToString();

                var images = await _mediator.Send(getImagesByCarIdQueryRequest);

                var colorQueryResponse = await _mediator.Send(colorQueryRequest);
                var brandQueryResponse = await _mediator.Send(brandQueryRequest);
                var fuelTypeQueryResponse = await _mediator.Send(fuelTypeQueryRequest);
                var gearTypeQueryResponse = await _mediator.Send(gearTypeQueryRequest);
                var categoryQueryResponse = await _mediator.Send(categoryQueryRequest);


                var user = await _userManager.FindByIdAsync(response.Data.UserId.ToString());

                CarDetailsViewModel viewModel = new CarDetailsViewModel
                {
                    NameSurname = user.NameSurname,
                    Phone=user.PhoneNumber,
                    Email=user.Email,

                    ProfileImagePath = user.ProfileImagePath,
                    Images = images.Data.ToArray(),

                    ColorName = colorQueryResponse.Data.Name,
                    BrandName = brandQueryResponse.Data.Name,
                    FuelTypeName = fuelTypeQueryResponse.Data.Type,
                    GearTypeName = gearTypeQueryResponse.Data.Type,
                    CategoryName = categoryQueryResponse.Data.Name,

                    CarHeader=response.Data.CarHeader,
                    Mileage = response.Data.Mileage,
                    Year = response.Data.Year,
                    Price = response.Data.Price,
                    Name = response.Data.Name,
                    Description = response.Data.Description,
                    FuelTankCapacity = response.Data.FuelTankCapacity,

                    Latitude = response.Data.Latitude,
                    Longitude = response.Data.Longitude,

                    //boolean
                    AirConditioner = response.Data.AirConditioner,
                    Door = response.Data.Door,
                    AntiLock = response.Data.AntiLock,
                    Brake = response.Data.Brake,
                    SeatCount = response.Data.SeatCount,
                    Airbag = response.Data.Airbag,
                    Windows = response.Data.Windows,
                    PassengerAirbag = response.Data.PassengerAirbag,
                    Steering = response.Data.Steering,
                    Player = response.Data.Player,
                    Sensor = response.Data.Sensor,
                    Seats = response.Data.Seats,
                    EngineDescription = response.Data.EngineDescription,
                    Locking = response.Data.Locking,
                    Headlamps = response.Data.Headlamps,
                    EngineWarning = response.Data.EngineWarning,

                };

                return View(viewModel);
            }

            return View();
        }
    }
}
