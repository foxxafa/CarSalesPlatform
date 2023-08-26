using Application.Results;
using MediatR;

namespace Application.Features.Commands.CategoryCommands.UpdateCategory
{
    public class UpdateCategoryCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
