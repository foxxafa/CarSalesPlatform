using Application.Features.Commands.CarCommands.CreateCar;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Car
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        readonly IMediator _mediator;

        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarCommandRequest createCarCommandRequest)
        {
            var response = await _mediator.Send(createCarCommandRequest);
            return Ok(response);
        }

    }
}
