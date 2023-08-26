using Application.Results;
using MediatR;

namespace Application.Features.Commands.BrandCommands.CreateBrand
{
    public class CreateBrandCommandRequest : IRequest<Result>
    {
        public string Name { get; set; }
    }
}
