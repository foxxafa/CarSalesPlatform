using Application.Repositories.ICategoryRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.CategoryQueries.GetAllCategory
{
    public class GetAllCategoryQueryHandler : IRequestHandler<GetAllCategoryQueryRequest, DataResult<List<Category>>>
    {
        readonly ICategoryReadRepositories _categoryReadRepositories;

        public GetAllCategoryQueryHandler(ICategoryReadRepositories categoryReadRepositories)
        {
            _categoryReadRepositories = categoryReadRepositories;
        }

        public async Task<DataResult<List<Category>>> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var categories = await _categoryReadRepositories.GetAll(false).ToListAsync(cancellationToken);

            if (categories.Any())
                return new SuccessDataResult<List<Category>>(categories,"Kategoriler Listelendi");

            return new SuccessDataResult<List<Category>>("Kategori yok");
        }
    }
}
