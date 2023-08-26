using Application.Repositories.IFuelTypeRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.FuelTypeCommands.UpdateFuelType
{
    public class UpdateFuelTypeCommandHandler : IRequestHandler<UpdateFuelTypeCommandRequest, Result>
    {
        readonly IFuelTypeReadRepositories _fuelTypeReadRepositories;
        readonly IFuelTypeWriteRepositories _fuelTypeWriteRepositories;

        public UpdateFuelTypeCommandHandler(IFuelTypeReadRepositories fuelTypeReadRepositories, IFuelTypeWriteRepositories fuelTypeWriteRepositories)
        {
            _fuelTypeReadRepositories = fuelTypeReadRepositories;
            _fuelTypeWriteRepositories = fuelTypeWriteRepositories;
        }

        public async Task<Result> Handle(UpdateFuelTypeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingFuelType = await _fuelTypeReadRepositories.GetByIdAsync(request.Id, false);
            if (existingFuelType == null)
                return new ErrorResult("Yakıt türü bulunamadı.");

            existingFuelType.Type = request.Type;
            _fuelTypeWriteRepositories.Update(existingFuelType);
            await _fuelTypeWriteRepositories.SaveAsync();

            return new SuccessResult("Yakıt türü güncellendi.");
        }
    }
}
