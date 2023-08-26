using Application.Repositories.IBrandRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.BrandCommands.CreateBrand
{
    public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommandRequest, Result>
    {
        readonly IBrandWriteRepositories _brandWriteRepositories;
        readonly IBrandReadRepositories _brandReadRepositories;

        public CreateBrandCommandHandler(IBrandWriteRepositories brandWriteRepositories, IBrandReadRepositories brandReadRepositories)
        {
            _brandWriteRepositories = brandWriteRepositories;
            _brandReadRepositories = brandReadRepositories;
        }

        public async Task<Result> Handle(CreateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var existingBrand = await _brandReadRepositories.GetSingleAsync(x => x.Name == request.Name, false);
            if (existingBrand != null)
                return new ErrorResult("Bu marka mevcut!");

            var brand = new Brand { Name = request.Name };
            await _brandWriteRepositories.AddAsync(brand);
            await _brandWriteRepositories.SaveAsync();

            return new SuccessResult("Marka eklendi");
        }
    }
}
