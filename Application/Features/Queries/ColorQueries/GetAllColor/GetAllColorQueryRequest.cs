using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.ColorQueries.GetAllColor
{
    public class GetAllColorQueryRequest : IRequest<DataResult<List<Color>>>
    {
    }

}
