using Application.Results;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUserCommands.UpdateUserImageCommand
{
    public class UpdateUserImageCommandHandler : IRequestHandler<UpdateUserImageCommandRequest, Result>
    {
        readonly UserManager<AppUser> _userManager;
        readonly IWebHostEnvironment _hostEnvironment;

        public UpdateUserImageCommandHandler(UserManager<AppUser> userManager, IWebHostEnvironment hostEnvironment)
        {
            _userManager = userManager;
            _hostEnvironment = hostEnvironment;
        }

        public async Task<Result> Handle(UpdateUserImageCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
                return new ErrorResult("Kullanıcı Bulunamadı");

            var supportedTypes = new[] { "jpg", "jpeg", "png" };
            var fileExt = Path.GetExtension(request.Photo.FileName).Substring(1);

            if (!supportedTypes.Contains(fileExt))
                return new ErrorResult("Geçersiz dosya formatı");

            // Sunucu dizinine göre göreceli yolu oluşturun
            var relativePath = "images/UserProfileImage";
            var uploadPath = Path.Combine(_hostEnvironment.WebRootPath, relativePath);
            if (!Directory.Exists(uploadPath))
                Directory.CreateDirectory(uploadPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Photo.FileName);
            var filePath = Path.Combine(uploadPath, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.Photo.CopyToAsync(fileStream);
            }

            // Burada yalnızca göreceli yolu ve dosya adını kaydediyoruz
            user.ProfileImagePath = $"{relativePath}/{fileName}";

            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
                return new SuccessResult("Başarılı");
            else
                return new ErrorResult("Fotoğraf güncellenemedi");
        }

    }
}
