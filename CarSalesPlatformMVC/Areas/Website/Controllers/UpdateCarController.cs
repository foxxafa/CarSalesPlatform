﻿using Application.Abstractions.Services;
using Application.DTOs.CarDTO.CreateCarDTO;
using Application.Features.Commands.CarCommands.DeleteCarImageByCarId;
using Application.Features.Commands.CarCommands.UpdateCar;
using Application.Features.Queries.CarQueries.GetByIDCar;
using Application.Features.Queries.CarQueries.GetImagesByCarId;
using Application.Results;
using AutoMapper;
using CarSalesPlatformMVC.Areas.Website.Attributes;
using CarSalesPlatformMVC.Areas.Website.Models.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    [Authorize]

    public class UpdateCarController : Controller
    {
        readonly ICarService _carService;
        readonly IMediator _mediator;
        readonly IMapper _mapper;

        public UpdateCarController(ICarService carService, IMediator mediator, IMapper mapper)
        {
            _carService = carService;
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("[controller]/{Id}")]
        public async Task<IActionResult> Index(string Id)
        {
            ViewBag.Brands = (await _carService.GetCarBrandsAsync()).Data.ToList();
            ViewBag.Colors = (await _carService.GetCarColorsAsync()).Data.ToList();
            ViewBag.FuelTypes = (await _carService.GetCarFuelTypesAsync()).Data.ToList();
            ViewBag.Categories = (await _carService.GetCarCategoriesAsync()).Data.ToList();
            ViewBag.GearTypes = (await _carService.GetCarGearTypesAsync()).Data.ToList();

            GetByIDCarQueryRequest request = new GetByIDCarQueryRequest();
            request.Id = Id;
            var response = await _mediator.Send(request);

            CreateCarDTO CarDto = _mapper.Map<CreateCarDTO>(response.Data);

            CarDto.CarId = Guid.Parse(request.Id);

            if (response.IsSuccess)
            {
                var imagesRequest = new GetImagesByCarIdQueryRequest { CarId = Id };
                var imagesResponse = await _mediator.Send(imagesRequest);

                var viewModel = new CarCreateUpdateFormVM
                {
                    Car = CarDto,
                    CarImages = imagesResponse.Data
                };
                return View(viewModel);
            }


            return BadRequest();
        }

        [HttpPost("[controller]/[action]")]
        [ValidateModel]
        public async Task<IActionResult> UpdateCar(CarCreateUpdateFormVM model)
        {
            var userId = HttpContext.Items["UserId"] as Guid?;
            if (userId.HasValue)
                model.Car.UserId = userId.Value;
            else
                return View(new ErrorResult("Kullanıcı kimliği çözümlenemedi"));

            UpdateCarCommandRequest request = new UpdateCarCommandRequest();

            request.Car = model.Car;
            request.Files = model.Files;
            request.CoverIndex = model.CoverIndex;

            Result response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpDelete("[controller]/[action]/{imageId}")]
        public async Task<IActionResult> Delete(string imageId)
        {
            DeleteCarImageByCarIdCommandRequest request = new DeleteCarImageByCarIdCommandRequest();
            request.imageId = imageId;

            Result response = await _mediator.Send(request);
            return Ok(response);
        }
    }


}
