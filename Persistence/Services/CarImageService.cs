using Application.Abstractions.Services;
using Application.Repositories.ICarImageRepository;
using Application.Repositories.ICarRepositories;
using Application.Results;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Persistence.Services
{
    public class CarImageService : ICarImageService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        readonly ICarReadRepositories _carReadRepositories;
        readonly ICarImageWriteRepository _carImageWriteRepository;

        public CarImageService(IWebHostEnvironment hostingEnvironment, ICarReadRepositories carReadRepositories, ICarImageWriteRepository carImageWriteRepository)
        {
            _hostingEnvironment = hostingEnvironment;
            _carReadRepositories = carReadRepositories;
            _carImageWriteRepository = carImageWriteRepository;
        }

        public async Task<Result> SaveImagesForCarAsync(string carId, List<IFormFile> images)
        {
            var car = await _carReadRepositories.GetByIdAsync(carId);

            if (car == null)
                throw new Exception("Car not found.");

            var imagesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            if(images!=null)
            {
                foreach (var image in images)
                {
                    var guid = Guid.NewGuid();
                    var uniqueFileName = guid.ToString() + "_" + image.FileName;
                    var imagePath = Path.Combine(imagesFolder, uniqueFileName);

                    using var fileStream = new FileStream(imagePath, FileMode.Create);
                    await image.CopyToAsync(fileStream);

                    var newCarImage = new CarImage
                    {
                        ImagePath = uniqueFileName,
                        Car = car,
                        Id = guid
                    };

                    // Assuming you have an Add method in your ICarImageWriteRepository
                    var result = await _carImageWriteRepository.AddAsync(newCarImage);

                    if (result.IsSuccess)
                    {
                        await _carImageWriteRepository.SaveAsync();
                    }
                    else
                    {
                        return new ErrorResult("resim eklenemedi");
                    }

                }
            }
            return new SuccessResult("başarılı");
        }
    }
}
