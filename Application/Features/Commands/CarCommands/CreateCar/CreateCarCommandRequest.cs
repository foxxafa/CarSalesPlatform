using Application.DTOs.CarDTO.CreateCarDTO;
using Application.Results;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.CarCommands.CreateCar
{
    public class CreateCarCommandRequest :IRequest<Result>
    {
        public List<IFormFile> Files { get; set; }
        public CreateCarDTO Car { get; set; }
    }
}
