using Application.Repositories.IColorRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.ColorQueries.GetByID
{
    public class GetByIDColorQueryHandler : IRequestHandler<GetByIDColorQueryRequest, DataResult<Color>>
    {
        readonly IColorReadRepositories _colorReadRepositories;

        public GetByIDColorQueryHandler(IColorReadRepositories colorReadRepositories)
        {
            _colorReadRepositories = colorReadRepositories;
        }

        public async Task<DataResult<Color>> Handle(GetByIDColorQueryRequest request, CancellationToken cancellationToken)
        {
            var color = await _colorReadRepositories.GetByIdAsync(request.Id,false);

            if (color == null)
                return new ErrorDataResult<Color>("Renk bulunamadı");

            return new SuccessDataResult<Color>(color);
        }
    }

}
