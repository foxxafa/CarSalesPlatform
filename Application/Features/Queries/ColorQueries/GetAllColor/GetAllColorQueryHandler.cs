using Application.Repositories.IColorRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
namespace Application.Features.Queries.ColorQueries.GetAllColor
{
    public class GetAllColorQueryHandler : IRequestHandler<GetAllColorQueryRequest, DataResult<List<Color>>>
    {
        readonly IColorReadRepositories _colorReadRepositories;

        public GetAllColorQueryHandler(IColorReadRepositories colorReadRepositories)
        {
            _colorReadRepositories = colorReadRepositories;
        }

        public async Task<DataResult<List<Color>>> Handle(GetAllColorQueryRequest request, CancellationToken cancellationToken)
        {
            var colors = await _colorReadRepositories.GetAll(false).ToListAsync(cancellationToken);

            if (colors.Any())
                return new SuccessDataResult<List<Color>>(colors, "Renkler Listelendi");

            return new ErrorDataResult<List<Color>>("Renk yok");
        }
    }

}
