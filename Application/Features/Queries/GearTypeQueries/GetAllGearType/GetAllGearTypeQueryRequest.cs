using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.GearTypeQueries.GetAllGearType
{
    public class GetAllGearTypeQueryRequest : IRequest<DataResult<List<GearType>>>
    {
    }

}
