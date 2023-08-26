using Application.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.AppUserCommands.UpdateUserImageCommand
{
    public class UpdateUserImageCommandRequest : IRequest<Result>
    {
        public string UserId { get; set; }
        public IFormFile Photo { get; set; }
    }
}
