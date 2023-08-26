using Application.Repositories.IColorRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.ColorCommands.DeleteColor
{
    public class DeleteColorCommandHandler : IRequestHandler<DeleteColorCommandRequest, Result>
    {
        readonly IColorReadRepositories _colorReadRepositories;
        readonly IColorWriteRepositories _colorWriteRepositories;

        public DeleteColorCommandHandler(IColorReadRepositories colorReadRepositories, IColorWriteRepositories colorWriteRepositories)
        {
            _colorReadRepositories = colorReadRepositories;
            _colorWriteRepositories = colorWriteRepositories;
        }

        public async Task<Result> Handle(DeleteColorCommandRequest request, CancellationToken cancellationToken)
        {
            var existingColor = await _colorReadRepositories.GetByIdAsync(request.Id, false);
            if (existingColor == null)
                return new ErrorResult("Renk bulunamadı.");

            _colorWriteRepositories.Remove(existingColor);
            await _colorWriteRepositories.SaveAsync();

            return new SuccessResult("Renk silindi.");
        }
    }
}
