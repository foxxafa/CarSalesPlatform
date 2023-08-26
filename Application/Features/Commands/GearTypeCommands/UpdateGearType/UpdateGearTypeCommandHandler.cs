using Application.Features.Commands.FuelTypeCommands.UpdateFuelType;
using Application.Repositories.IFuelTypeRepositories;
using Application.Repositories.IGearTypeRepositories;
using Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.GearTypeCommands.UpdateGearType
{
    public class UpdateGearTypeCommandHandler : IRequestHandler<UpdateGearTypeCommandRequest, Result>
    {
        readonly IGearTypeReadRepositories _gearTypeReadRepositories;
        readonly IGearTypeWriteRepositories _gearTypeWriteRepositories;

        public UpdateGearTypeCommandHandler(IGearTypeReadRepositories gearTypeReadRepositories, IGearTypeWriteRepositories gearTypeWriteRepositories)
        {
            _gearTypeReadRepositories = gearTypeReadRepositories;
            _gearTypeWriteRepositories = gearTypeWriteRepositories;
        }

        public async Task<Result> Handle(UpdateGearTypeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingGearType = await _gearTypeReadRepositories.GetByIdAsync(request.Id,false);
            if (existingGearType == null)
                return new ErrorResult("Vites türü bulunamadı.");

            existingGearType.Type = request.Type;
            _gearTypeWriteRepositories.Update(existingGearType);
            await _gearTypeWriteRepositories.SaveAsync();

            return new SuccessResult("Vites türü güncellendi.");
        }
    }
}
