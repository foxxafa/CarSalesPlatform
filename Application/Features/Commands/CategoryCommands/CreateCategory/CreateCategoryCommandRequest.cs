using Application.Results;
using MediatR;

namespace Application.Features.Commands.CategoryCommands.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<Result>
    {
        public string Name { get; set; }
    }
}
