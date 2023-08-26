using Application.Repositories.ICarImageRepository;
using Application.Results;
using MediatR;
using Microsoft.AspNetCore.Hosting;

namespace Application.Features.Commands.CarCommands.DeleteCarImageByCarId
{
    public class DeleteCarImageByCarIdCommandHandler : IRequestHandler<DeleteCarImageByCarIdCommandRequest, Result>
    {
        readonly ICarImageReadRepository _carImageReadRepository;
        readonly ICarImageWriteRepository _carImageWriteRepository;
        private readonly IWebHostEnvironment _env;

        public DeleteCarImageByCarIdCommandHandler(ICarImageReadRepository carImageReadRepository, ICarImageWriteRepository carImageWriteRepository, IWebHostEnvironment env)
        {
            _carImageReadRepository = carImageReadRepository;
            _carImageWriteRepository = carImageWriteRepository;
            _env = env;
        }

        public async Task<Result> Handle(DeleteCarImageByCarIdCommandRequest request, CancellationToken cancellationToken)
        {

            var carImage = await _carImageReadRepository.GetByIdAsync(request.imageId);

            // Eğer carImage null değilse ve imagePath özelliği mevcutsa işlemleri devam ettir
            if (carImage != null && !string.IsNullOrEmpty(carImage.ImagePath))
            {
                // Fiziksel dosya yolunu oluştur
                string filePath = Path.Combine(_env.WebRootPath, "images", carImage.ImagePath);

                // Eğer dosya mevcutsa, sil
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    var response = await _carImageWriteRepository.RemoveAsync(request.imageId);
                    if (response.IsSuccess)
                    {
                        await _carImageWriteRepository.SaveAsync();
                        return new SuccessResult("Silindi");
                    }
                }
                else return new ErrorResult("Dosya bulunamadı");
            }
            return new ErrorResult("NULL");
        }

    }
}
