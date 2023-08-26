using Application.Repositories.IFuelTypeRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.FuelTypeCommands.DeleteFuelType
{
    public class DeleteFuelTypeCommandHandler : IRequestHandler<DeleteFuelTypeCommandRequest, Result>
    {
        readonly IFuelTypeReadRepositories _fuelTypeReadRepositories;
        readonly IFuelTypeWriteRepositories _fuelTypeWriteRepositories;

        public DeleteFuelTypeCommandHandler(IFuelTypeReadRepositories fuelTypeReadRepositories, IFuelTypeWriteRepositories fuelTypeWriteRepositories)
        {
            _fuelTypeReadRepositories = fuelTypeReadRepositories;
            _fuelTypeWriteRepositories = fuelTypeWriteRepositories;
        }

        public async Task<Result> Handle(DeleteFuelTypeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingFuelType = await _fuelTypeReadRepositories.GetByIdAsync(request.Id, false);
            if (existingFuelType == null)
                return new ErrorResult ("Yakıt türü bulunamadı.");

            _fuelTypeWriteRepositories.Remove(existingFuelType);
            await _fuelTypeWriteRepositories.SaveAsync();

            return new SuccessResult("Yakıt türü silindi.");
        }
    }
}
