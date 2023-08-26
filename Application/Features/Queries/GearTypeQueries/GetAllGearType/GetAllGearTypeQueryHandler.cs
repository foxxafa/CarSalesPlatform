using Application.Repositories.IGearTypeRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.GearTypeQueries.GetAllGearType
{
    public class GetAllGearTypeQueryHandler : IRequestHandler<GetAllGearTypeQueryRequest, DataResult<List<GearType>>>
    {
        readonly IGearTypeReadRepositories _gearTypeReadRepositorie;

        public GetAllGearTypeQueryHandler(IGearTypeReadRepositories gearTypeReadRepositorie)
        {
            _gearTypeReadRepositorie = gearTypeReadRepositorie;
        }

        public async Task<DataResult<List<GearType>>> Handle(GetAllGearTypeQueryRequest request, CancellationToken cancellationToken)
        {
            var gearTypes = await _gearTypeReadRepositorie.GetAll(false).ToListAsync(cancellationToken);

            if (gearTypes.Any())
                return new SuccessDataResult<List<GearType>>(gearTypes,"Vites türleri listelendi");

            return new ErrorDataResult<List<GearType>>("Vites türü yok");
        }
    }

}
