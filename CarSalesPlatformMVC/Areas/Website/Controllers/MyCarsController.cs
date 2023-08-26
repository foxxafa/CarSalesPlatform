using Application.Features.Commands.CarCommands.DeleteCar;
using Application.Features.Commands.CarCommands.UpdateCarStatus;
using Application.Features.Queries.CarQueries.GetCarsByUserId;
using Application.Features.Queries.CarQueries.GetImagesByCarId;
using Application.Results;
using CarSalesPlatformMVC.Areas.Website.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    [Authorize]
    public class MyCarsController : Controller
    {
        readonly IMediator _mediator;

        public MyCarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index(int page = 1)
        {
            return View(page);
        }

        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> LoadCar(int page)
        {
            var userId = HttpContext.Items["UserId"] as Guid?;
            if (!userId.HasValue)
                return View(new ErrorResult("Kullanıcı kimliği çözümlenemedi"));

            var query = new GetCarsByUserIdQueryRequest { UserId = userId.Value, Page = page };
            var response = await _mediator.Send(query);

            PaginationVM paginationVM = new PaginationVM()
            {
                CurrentPage = response.Data.Item1.CurrentPage,
                TotalPages = response.Data.Item1.TotalPages
            };

            GetImagesByCarIdQueryRequest request = new GetImagesByCarIdQueryRequest();

            List<string> imagePathList = new List<string>();
            foreach (var car in response.Data.Item2)
            {
                request.CarId = car.Id.ToString();
                var imageResponse = await _mediator.Send(request);
                imagePathList.Add(imageResponse.Data.FirstOrDefault().ImagePath);
            }

            var viewModel = new MyCarsPageVM
            {
                Pagination = paginationVM,
                Car = response.Data.Item2,
                TotalCars = response.Data.Item3,
                CarsImage = imagePathList
            };

            return Json(viewModel);
        }


        [HttpPost("[controller]/[action]")]
        public async Task<IActionResult> ChangeStatus(UpdateCarStatusCommandRequest updateCarStatusCommandRequest)
        {
            var result = await _mediator.Send(updateCarStatusCommandRequest);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("[controller]/[action]/{carId}")]
        public async Task<IActionResult> DeleteCar(string carId)
        {
            DeleteCarCommandRequest request = new DeleteCarCommandRequest();
            request.carId = carId;
            Result response = await _mediator.Send(request);

            if(response.IsSuccess)
            {
                return Ok(response);
            }
            return BadRequest($"Could not delete car {carId}");
        }
    }
}
