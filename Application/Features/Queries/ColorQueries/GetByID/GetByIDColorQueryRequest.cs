using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.ColorQueries.GetByID
{
    public class GetByIDColorQueryRequest : IRequest<DataResult<Color>>
    {
        public string Id { get; set; }
    }

}
