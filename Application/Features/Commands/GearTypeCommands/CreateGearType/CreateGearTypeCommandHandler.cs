using Application.Repositories.IColorRepositories;
using Application.Repositories.IGearTypeRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.GearTypeCommands.CreateGearType
{
    public class CreateGearTypeCommandHandler : IRequestHandler<CreateGearTypeCommandRequest, Result>
    {
        readonly IGearTypeReadRepositories _gearTypeReadRepositories;
        readonly IGearTypeWriteRepositories _gearTypeWriteRepositories;

        public CreateGearTypeCommandHandler(IGearTypeReadRepositories gearTypeReadRepositories, IGearTypeWriteRepositories gearTypeWriteRepositories)
        {
            _gearTypeReadRepositories = gearTypeReadRepositories;
            _gearTypeWriteRepositories = gearTypeWriteRepositories;
        }

        public async Task<Result> Handle(CreateGearTypeCommandRequest request, CancellationToken cancellationToken)
        {
            var existingGearType = await _gearTypeReadRepositories.GetSingleAsync(x => x.Type == request.Type, false);
            if (existingGearType != null)
                return new ErrorResult("Bu vites türü mevcut!");

            var GearType = new GearType { Type = request.Type };
            await _gearTypeWriteRepositories.AddAsync(GearType);
            await _gearTypeWriteRepositories.SaveAsync();

            return new SuccessResult("Vites türü eklendi.");
        }
    }
}
