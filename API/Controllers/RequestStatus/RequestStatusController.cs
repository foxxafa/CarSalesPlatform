using Application.Features.Commands.RequestStatusCommands.CreateRequestStatus;
using Application.Features.Commands.RequestStatusCommands.DeleteRequestStatus;
using Application.Features.Commands.RequestStatusCommands.UpdateRequestStatus;
using Application.Features.Queries.RequestStatusQueries.GetAllRequestStatus;
using Application.Features.Queries.RequestStatusQueries.GetByIDRequestStatus;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.RequestStatus
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RequestStatusController : ControllerBase
    {
        readonly IMediator _mediator;

        public RequestStatusController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRequestStatusCommandRequest createRequestStatusCommandRequest)
        {
            var response = await _mediator.Send(createRequestStatusCommandRequest);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteRequestStatusCommandRequest deleteRequestStatusCommandRequest)
        {
            var response = await _mediator.Send(deleteRequestStatusCommandRequest);
            return Ok(response);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRequestStatusCommandRequest updateRequestStatusCommandRequest)
        {
            var response = await _mediator.Send(updateRequestStatusCommandRequest);
            return Ok(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetAll(GetAllRequestStatusQueryRequest getAllRequestStatusQueryRequest)
        {
            var response = await _mediator.Send(getAllRequestStatusQueryRequest);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetByID(GetByIDRequestStatusRequest getByIDRequestStatusRequest)
        {
            var response = await _mediator.Send(getByIDRequestStatusRequest);
            return Ok(response);
        }
    }
}
