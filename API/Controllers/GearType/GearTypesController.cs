using Application.Features.Commands.GearTypeCommands.CreateGearType;
using Application.Features.Commands.GearTypeCommands.DeleteGearType;
using Application.Features.Commands.GearTypeCommands.UpdateGearType;
using Application.Features.Queries.GearTypeQueries.GetAllGearType;
using Application.Features.Queries.GearTypeQueries.GetByIDGearType;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace API.Controllers.GearType

{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GearTypesController : ControllerBase
    {
        readonly IMediator _mediator;

        public GearTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateGearTypeCommandRequest createGearTypeCommandRequest)
        {
            var response = await _mediator.Send(createGearTypeCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteGearTypeCommandRequest deleteGearTypeCommandRequest)
        {
            var response = await _mediator.Send(deleteGearTypeCommandRequest);
            return Ok(response);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateGearTypeCommandRequest updateGearTypeCommandRequest)
        {
            var response = await _mediator.Send(updateGearTypeCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllGearTypeQueryRequest getAllGearTypeQueryRequest)
        {
            var response = await _mediator.Send(getAllGearTypeQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(GetByIDGearTypeQueryRequest getByIDGearTypeQueryRequest)
        {
            var response = await _mediator.Send(getByIDGearTypeQueryRequest);
            return Ok(response);
        }
    }
}
