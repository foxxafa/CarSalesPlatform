using Application.Results;
using MediatR;

namespace Application.Features.Commands.BrandCommands.DeleteBrand
{
    public class DeleteBrandCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
    }

}
