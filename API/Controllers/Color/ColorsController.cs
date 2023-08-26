using Application.Features.Commands.ColorCommands.CreateColor;
using Application.Features.Commands.ColorCommands.DeleteColor;
using Application.Features.Commands.ColorCommands.UpdateColor;
using Application.Features.Queries.ColorQueries.GetAllColor;
using Application.Features.Queries.ColorQueries.GetByID;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Color
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        readonly IMediator _mediator;

        public ColorsController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateColorCommandRequest createColorCommandRequest)
        {
            var response = await _mediator.Send(createColorCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteColorCommandRequest deleteColorCommandRequest)
        {
            var response = await _mediator.Send(deleteColorCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateColorCommandRequest updateColorCommandRequest)
        {
            var response = await _mediator.Send(updateColorCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllColorQueryRequest getAllColorQueryRequest)
        {
            var response = await _mediator.Send(getAllColorQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(GetByIDColorQueryRequest getByIDColorQueryRequest)
        {
            var response = await _mediator.Send(getByIDColorQueryRequest);
            return Ok(response);
        }
    }
}
