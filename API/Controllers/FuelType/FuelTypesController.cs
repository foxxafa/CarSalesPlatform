using Application.Features.Commands.FuelTypeCommands.CreateFuelType;
using Application.Features.Commands.FuelTypeCommands.DeleteFuelType;
using Application.Features.Commands.FuelTypeCommands.UpdateFuelType;
using Application.Features.Queries.FuelTypeQueries.GetAllFuelType;
using Application.Features.Queries.FuelTypeQueries.GetByID;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers.FuelType
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FuelTypesController : ControllerBase
    {
        readonly IMediator _mediator;

        public FuelTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateFuelTypeCommandRequest createFuelTypeCommandRequest)
        {
            var response = await _mediator.Send(createFuelTypeCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteFuelTypeCommandRequest deleteFuelTypeCommandRequest)
        {
            var response = await _mediator.Send(deleteFuelTypeCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateFuelTypeCommandRequest updateFuelTypeCommandRequest)
        {
            var response = await _mediator.Send(updateFuelTypeCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllFuelTypeQueryRequest getAllFuelTypeQueryRequest)
        {
            var response = await _mediator.Send(getAllFuelTypeQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(GetByIDFuelTypeQueryRequest getByIDFuelTypeQueryRequest)
        {
            var response = await _mediator.Send(getByIDFuelTypeQueryRequest);
            return Ok(response);
        }
    }
}
