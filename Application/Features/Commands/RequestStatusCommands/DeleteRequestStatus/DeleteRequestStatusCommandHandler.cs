using Application.Repositories.IRequestStatusRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.RequestStatusCommands.DeleteRequestStatus
{
    public class DeleteRequestStatusCommandHandler : IRequestHandler<DeleteRequestStatusCommandRequest, Result>
    {
        readonly IRequestStatusReadRepositories _requestStatusReadRepositories;
        readonly IRequestStatusWriteRepositories _requestStatusWriteRepositories;

        public DeleteRequestStatusCommandHandler(IRequestStatusReadRepositories requestStatusReadRepositories, IRequestStatusWriteRepositories requestStatusWriteRepositories)
        {
            _requestStatusReadRepositories = requestStatusReadRepositories;
            _requestStatusWriteRepositories = requestStatusWriteRepositories;
        }

        public async Task<Result> Handle(DeleteRequestStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var existingRequestStatus = await _requestStatusReadRepositories.GetByIdAsync(request.Id, false);
            if (existingRequestStatus == null)
                return new ErrorResult("İstek durumu bulunamadı.");

            _requestStatusWriteRepositories.Remove(existingRequestStatus);
            await _requestStatusWriteRepositories.SaveAsync();

            return new SuccessResult("İstek durumı silindi.");
        }
    }
}
