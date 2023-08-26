using Application.Repositories.ICategoryRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.CategoryQueries.GetByID
{
    public class GetByIDCategoryQueryHandler : IRequestHandler<GetByIDCategoryQueryRequest, DataResult<Category>>
    {
        readonly ICategoryReadRepositories _categoryReadRepositories;

        public GetByIDCategoryQueryHandler(ICategoryReadRepositories categoryReadRepositories)
        {
            _categoryReadRepositories = categoryReadRepositories;
        }

        public async Task<DataResult<Category>> Handle(GetByIDCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var category = await _categoryReadRepositories.GetByIdAsync(request.Id,false);

            if (category == null)
                return new ErrorDataResult<Category>("Kategori Bulunamadı");

            return new SuccessDataResult<Category>(category);
        }
    }

}
