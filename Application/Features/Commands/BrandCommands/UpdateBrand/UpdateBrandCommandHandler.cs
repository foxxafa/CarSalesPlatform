using Application.Repositories.IBrandRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.BrandCommands.UpdateBrand
{
    public class UpdateBrandCommandHandler : IRequestHandler<UpdateBrandCommandRequest, Result>
    {
        private readonly IBrandWriteRepositories _brandWriteRepositories;
        private readonly IBrandReadRepositories _brandReadRepositories;

        public UpdateBrandCommandHandler(IBrandWriteRepositories brandWriteRepositories, IBrandReadRepositories brandReadRepositories)
        {
            _brandWriteRepositories = brandWriteRepositories;
            _brandReadRepositories = brandReadRepositories;
        }
        public async Task<Result> Handle(UpdateBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var existingBrand = await _brandReadRepositories.GetByIdAsync(request.Id,false);
            if (existingBrand == null)
                return new ErrorResult("Marka Bulunamadı");

            existingBrand.Name = request.Name;
            _brandWriteRepositories.Update(existingBrand);
            await _brandWriteRepositories.SaveAsync();

            return new SuccessResult("Marka güncellendi");
        }
    }
}
