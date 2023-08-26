using Application.Repositories.IRequestStatusRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.RequestStatusCommands.CreateRequestStatus
{
    public class CreateRequestStatusCommandHandler : IRequestHandler<CreateRequestStatusCommandRequest, Result>
    {
        readonly IRequestStatusReadRepositories _requestStatusReadRepositories;
        readonly IRequestStatusWriteRepositories _requestStatusWriteRepositories;

        public CreateRequestStatusCommandHandler(IRequestStatusReadRepositories requestStatusReadRepositories, IRequestStatusWriteRepositories requestStatusWriteRepositories)
        {
            _requestStatusReadRepositories = requestStatusReadRepositories;
            _requestStatusWriteRepositories = requestStatusWriteRepositories;
        }

        public async Task<Result> Handle(CreateRequestStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var existingRequestStatus = await _requestStatusReadRepositories.GetSingleAsync(x => x.Name == request.Name,false);
            if (existingRequestStatus != null)
                return new ErrorResult("Bu durum mevcut!");

            var requestStatus = new RequestStatus { Name = request.Name };
            await _requestStatusWriteRepositories.AddAsync(requestStatus);
            await _requestStatusWriteRepositories.SaveAsync();

            return new SuccessResult("Durum eklendi.");
        }
    }
}

