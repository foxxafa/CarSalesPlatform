using Application.Results;
using MediatR;

namespace Application.Features.Commands.CategoryCommands.DeleteCategory
{
    public class DeleteCategoryCommandRequest : IRequest<Result>
    {
        public string Id { get; set; }
    }
}
