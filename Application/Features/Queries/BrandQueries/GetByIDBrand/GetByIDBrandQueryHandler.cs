using Application.Repositories.IBrandRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.BrandQueries.GetByIDBrand
{
    public class GetByIDBrandQueryHandler : IRequestHandler<GetByIDBrandQueryRequest, DataResult<Brand>>
    {
        readonly IBrandReadRepositories _brandReadRepositories;

        public GetByIDBrandQueryHandler(IBrandReadRepositories brandReadRepositories)
        {
            _brandReadRepositories = brandReadRepositories;
        }

        public async Task<DataResult<Brand>> Handle(GetByIDBrandQueryRequest request, CancellationToken cancellationToken)
        {
            var brand = await _brandReadRepositories.GetByIdAsync(request.Id,false);

            if (brand == null)
                return new ErrorDataResult<Brand>("Marka bulunamadı");

            return new SuccessDataResult<Brand>(brand, "Marka getirildi");
        }
    }
}
