using Application.Results;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Queries.UserQueries.GetProfileImageQuery
{
    public class GetProfileImageQueryHandler : IRequestHandler<GetProfileImageQueryRequest, DataResult<string>>
    {
        readonly UserManager<AppUser> _userManager;

        public GetProfileImageQueryHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<DataResult<string>> Handle(GetProfileImageQueryRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user==null)
                return new ErrorDataResult<string>(string.Empty, "Kullanıcı bulunamadı");

            var image = user.ProfileImagePath;

            if (image == null)
                return new ErrorDataResult<string>("Resim NULL");

            return new SuccessDataResult<string>(image,"Başarılı");
        }
    }
}
