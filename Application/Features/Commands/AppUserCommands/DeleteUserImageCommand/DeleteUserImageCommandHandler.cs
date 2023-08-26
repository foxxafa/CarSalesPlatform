using Application.Results;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUserCommands.DeleteUserImageCommand
{
    public class RemoveUserImageCommandHandler : IRequestHandler<DeleteUserImageCommandRequest, Result>
    {
        readonly UserManager<AppUser> _userManager;
        readonly IWebHostEnvironment _hostEnvironment;

        public RemoveUserImageCommandHandler(UserManager<AppUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Result> Handle(DeleteUserImageCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                return new ErrorResult("Kullanıcı Bulunamadı");

            var fullPath = Path.Combine(_hostEnvironment.WebRootPath, "images", "UserProfileImage", Path.GetFileName(user.ProfileImagePath));

            if (File.Exists(fullPath))
                File.Delete(fullPath);


            user.ProfileImagePath = null;

            var result = await _userManager.UpdateAsync(user);

            return result.Succeeded ? new SuccessResult("Başarılı") : new ErrorResult("Fotoğraf kaldırılamadı");
        }
    }
}
