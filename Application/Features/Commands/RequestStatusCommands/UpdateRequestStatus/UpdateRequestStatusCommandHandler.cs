using Application.Features.Commands.GearTypeCommands.UpdateGearType;
using Application.Repositories.IGearTypeRepositories;
using Application.Repositories.IRequestStatusRepositories;
using Application.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.RequestStatusCommands.UpdateRequestStatus
{
    public class UpdateRequestStatusCommandHandler : IRequestHandler<UpdateRequestStatusCommandRequest, Result>
    {
        readonly IRequestStatusReadRepositories _requestStatusReadRepositories;
        readonly IRequestStatusWriteRepositories _requestStatusWriteRepositories;

        public UpdateRequestStatusCommandHandler(IRequestStatusReadRepositories requestStatusReadRepositories, IRequestStatusWriteRepositories requestStatusWriteRepositories)
        {
            _requestStatusReadRepositories = requestStatusReadRepositories;
            _requestStatusWriteRepositories = requestStatusWriteRepositories;
        }

        public async Task<Result> Handle(UpdateRequestStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var existingRequestStatus = await _requestStatusReadRepositories.GetByIdAsync(request.Id, false);
            if (existingRequestStatus == null)
                return new ErrorResult("İstek durumu bulunamadı.");

            existingRequestStatus.Name = request.Name;
            _requestStatusWriteRepositories.Update(existingRequestStatus);
            await _requestStatusWriteRepositories.SaveAsync();

            return new SuccessResult("İstek durumu güncellendi.");
        }
    }
}
