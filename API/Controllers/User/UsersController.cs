using API.Attributes;
using Application.Features.Commands.AppUserCommands.CreateUserCommand;
using Application.Features.Commands.AppUserCommands.DeleteUserCommand;
using Application.Results;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserCommandRequest createUserCommandRequest)
        {
            Result response = await _mediator.Send(createUserCommandRequest);
            return Ok(response);
        }

        [Authorize(Roles = "User,Admin")]
        [ValidateUserOwnership] // Admin doğrulanmadan geçiyor yukarıda doğrulandığından
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteUserCommandRequest deleteUserCommandRequest)
        {

            Result response = await _mediator.Send(deleteUserCommandRequest);
            return Ok(response);
        }


    }
}
