using Application.Repositories.IGearTypeRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.GearTypeQueries.GetByIDGearType
{
    public class GetByIDGearTypeQueryHandler : IRequestHandler<GetByIDGearTypeQueryRequest, DataResult<GearType>>
    {
        readonly IGearTypeReadRepositories _gearTypeReadRepositories;

        public GetByIDGearTypeQueryHandler(IGearTypeReadRepositories gearTypeReadRepositories)
        {
            _gearTypeReadRepositories = gearTypeReadRepositories;
        }

        public async Task<DataResult<GearType>> Handle(GetByIDGearTypeQueryRequest request, CancellationToken cancellationToken)
        {
            var gearType = await _gearTypeReadRepositories.GetByIdAsync(request.Id, false);

            if (gearType == null)
                return new ErrorDataResult<GearType>("Vites türü bulunamadı");

            return new SuccessDataResult<GearType>(gearType);
        }
    }
}
