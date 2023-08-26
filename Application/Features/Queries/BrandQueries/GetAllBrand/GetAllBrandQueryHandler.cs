using Application.Repositories.IBrandRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.BrandQueries.GetAllBrand
{
    public class GetAllBrandQueryHandler : IRequestHandler<GetAllBrandQueryRequest, DataResult<List<Brand>>>
    {
        readonly IBrandReadRepositories _brandReadRepositories;

        public GetAllBrandQueryHandler(IBrandReadRepositories brandReadRepositories)
        {
            _brandReadRepositories = brandReadRepositories;
        }

        public async Task<DataResult<List<Brand>>> Handle(GetAllBrandQueryRequest request, CancellationToken cancellationToken)
        {
            var brands = await _brandReadRepositories.GetAll(false).ToListAsync(cancellationToken);

            if (brands.Any())
                return new SuccessDataResult<List<Brand>>(brands,"Markalar listelendi");

            return new ErrorDataResult<List<Brand>>("Marka yok");
        }
    }
}
