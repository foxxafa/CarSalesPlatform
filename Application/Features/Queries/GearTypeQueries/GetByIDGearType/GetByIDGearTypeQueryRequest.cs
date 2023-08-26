using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.GearTypeQueries.GetByIDGearType
{
    public class GetByIDGearTypeQueryRequest : IRequest<DataResult<GearType>>
    {
        public string Id { get; set; }
    }
}
