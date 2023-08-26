using Application.Repositories.IBrandRepositories;
using Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.CategoryCommands.DeleteCategory
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, Result>
    {
        private readonly IBrandWriteRepositories _brandWriteRepositories;
        private readonly IBrandReadRepositories _brandReadRepositories;

        public DeleteCategoryCommandHandler(IBrandWriteRepositories brandWriteRepositories, IBrandReadRepositories brandReadRepositories)
        {
            _brandWriteRepositories = brandWriteRepositories;
            _brandReadRepositories = brandReadRepositories;
        }

        public async Task<Result> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
        {
            var existingCategory = await _brandReadRepositories.GetByIdAsync(request.Id,false);
            if (existingCategory == null)
                return new ErrorResult("Kategori bulunamadı.");

            _brandWriteRepositories.Remove(existingCategory);
            await _brandWriteRepositories.SaveAsync();

            return new SuccessResult("Kategori silindi.");
        }
    }
}
