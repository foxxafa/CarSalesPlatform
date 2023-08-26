using Application.Repositories.ICategoryRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.CategoryCommands.CreateCategory
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Result>
    {
        readonly ICategoryReadRepositories _categoryReadRepositories;
        readonly ICategoryWriteRepositories _categoryWriteRepositories;

        public CreateCategoryCommandHandler(ICategoryReadRepositories categoryReadRepositories, ICategoryWriteRepositories categoryWriteRepositories)
        {
            _categoryReadRepositories = categoryReadRepositories;
            _categoryWriteRepositories = categoryWriteRepositories;
        }

        public async Task<Result> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var existingCategory = await _categoryReadRepositories.GetSingleAsync(x => x.Name == request.Name,false);
            if (existingCategory != null)
                return new ErrorResult("Bu kategori mevcut olduğundan dolayı eklenemedi.");

            var category = new Category { Name = request.Name };
            await _categoryWriteRepositories.AddAsync(category);
            await _categoryWriteRepositories.SaveAsync();

            return new SuccessResult("Kategori eklendi.");
        }
    }
}
