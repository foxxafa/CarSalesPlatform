using Application.Repositories.IColorRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.ColorCommands.CreateColor
{
    public class CreateColorCommandHandler : IRequestHandler<CreateColorCommandRequest, Result>
    {
        readonly IColorWriteRepositories _colorWriteRepositories;
        readonly IColorReadRepositories _colorReadRepositories;

        public CreateColorCommandHandler(IColorWriteRepositories colorWriteRepositories, IColorReadRepositories colorReadRepositories)
        {
            _colorWriteRepositories = colorWriteRepositories;
            _colorReadRepositories = colorReadRepositories;
        }

        public async Task<Result> Handle(CreateColorCommandRequest request, CancellationToken cancellationToken)
        {
            var existingColor = await _colorReadRepositories.GetSingleAsync(x => x.Name == request.Name, false);
            if (existingColor != null)
                return new ErrorResult("Bu renk mevcut olduğundan eklenemedi.");

            var color = new Color { Name = request.Name };
            await _colorWriteRepositories.AddAsync(color);
            await _colorWriteRepositories.SaveAsync();

            return new SuccessResult("Renk eklendi.");
        }
    }
}
