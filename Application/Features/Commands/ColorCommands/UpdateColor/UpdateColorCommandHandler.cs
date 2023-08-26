using Application.Features.Commands.CategoryCommands.UpdateCategory;
using Application.Repositories.IBrandRepositories;
using Application.Repositories.IColorRepositories;
using Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.ColorCommands.UpdateColor
{
    public class UpdateColorCommandHandler : IRequestHandler<UpdateColorCommandRequest, Result>
    {
        readonly IColorReadRepositories _colorReadRepositories;
        readonly IColorWriteRepositories _colorWriteRepositories;

        public UpdateColorCommandHandler(IColorReadRepositories colorReadRepositories, IColorWriteRepositories colorWriteRepositories)
        {
            _colorReadRepositories = colorReadRepositories;
            _colorWriteRepositories = colorWriteRepositories;
        }

        public async Task<Result> Handle(UpdateColorCommandRequest request, CancellationToken cancellationToken)
        {
            var existingColor = await _colorReadRepositories.GetByIdAsync(request.Id, false);
            if (existingColor == null)
                return new ErrorResult("Renk bulunamadı.");

            existingColor.Name = request.Name;
            _colorWriteRepositories.Update(existingColor);
            await _colorWriteRepositories.SaveAsync();

            return new SuccessResult("Renk güncellendi.");
        }
    }
}
