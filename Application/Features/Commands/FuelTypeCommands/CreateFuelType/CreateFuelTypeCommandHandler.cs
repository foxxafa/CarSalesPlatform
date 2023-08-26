using Application.Repositories.IColorRepositories;
using Application.Repositories.IFuelTypeRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.FuelTypeCommands.CreateFuelType
{
    public class CreateFuelTypeCommandHandler : IRequestHandler<CreateFuelTypeCommandRequest, Result>
    {
        readonly IFuelTypeReadRepositories _fuelTypeReadRepositories;
        readonly IFuelTypeWriteRepositories _fuelTypeWriteRepositories;

        public CreateFuelTypeCommandHandler(IFuelTypeReadRepositories fuelTypeReadRepositories, IFuelTypeWriteRepositories fuelTypeWriteRepositories)
        {
            _fuelTypeReadRepositories = fuelTypeReadRepositories;
            _fuelTypeWriteRepositories = fuelTypeWriteRepositories;
        }

        public async Task<Result> Handle(CreateFuelTypeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingFuelType = await _fuelTypeReadRepositories.GetSingleAsync(x => x.Type == request.Type, false);
            if (existingFuelType != null)
                return new ErrorResult("Yakıt türü mevcut olduğundan eklenemedi.");

            var fuelType = new FuelType { Type = request.Type };
            await _fuelTypeWriteRepositories.AddAsync(fuelType);
            await _fuelTypeWriteRepositories.SaveAsync();

            return new SuccessResult("Yakıt türü eklendi.");
        }
    }
}
