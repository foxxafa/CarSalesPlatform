using Application.Results;
using MediatR;

namespace Application.Features.Commands.BrandCommands.UpdateBrand
{
    public class UpdateBrandCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
