using Application.Repositories.IBrandRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.BrandCommands.DeleteBrand
{
    public class DeleteBrandCommandHandler : IRequestHandler<DeleteBrandCommandRequest, Result>
    {
        private readonly IBrandWriteRepositories _brandWriteRepositories;
        private readonly IBrandReadRepositories _brandReadRepositories;

        public DeleteBrandCommandHandler(IBrandWriteRepositories brandWriteRepositories, IBrandReadRepositories brandReadRepositories)
        {
            _brandWriteRepositories = brandWriteRepositories;
            _brandReadRepositories = brandReadRepositories;
        }

        public async Task<Result> Handle(DeleteBrandCommandRequest request, CancellationToken cancellationToken)
        {
            var existingBrand = await _brandReadRepositories.GetByIdAsync(request.Id,false);
            if (existingBrand == null)
                return new ErrorResult("Marka Bulunamadı");

            _brandWriteRepositories.Remove(existingBrand);
            await _brandWriteRepositories.SaveAsync();

            return new SuccessResult("Marka Silindi");
        }
    }

}
