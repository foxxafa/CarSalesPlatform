using Application.Abstractions.Services;
using Application.Features.Queries.CarQueries.GetImagesByCarId;
using Application.Repositories.ICarImageRepository;
using Application.Repositories.ICarRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Persistence.Services
{
    public class CarImageService : ICarImageService
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        readonly ICarReadRepositories _carReadRepositories;
        readonly ICarImageWriteRepository _carImageWriteRepository;
        readonly IMediator _mediator;

        public CarImageService(IWebHostEnvironment hostingEnvironment, ICarReadRepositories carReadRepositories, ICarImageWriteRepository carImageWriteRepository, IMediator mediator)
        {
            _hostingEnvironment = hostingEnvironment;
            _carReadRepositories = carReadRepositories;
            _carImageWriteRepository = carImageWriteRepository;
            _mediator = mediator;
        }

        public async Task<Result> SetCarCoverImage(string carId, int coverIndex)
        {
            GetImagesByCarIdQueryRequest request = new GetImagesByCarIdQueryRequest();
            request.CarId = carId;
            var carimages = await _mediator.Send(request);

            carimages.Data.Where(x => x.IsCover == true).SingleOrDefault().IsCover = false;

            if (carimages.IsSuccess) 
            {
                var carimage = carimages.Data.ToList()[coverIndex];
                carimage.IsCover = true;
                _carImageWriteRepository.Update(carimage);
                await _carImageWriteRepository.SaveAsync();

                return new SuccessResult("Kapak fotoğrafı ayarlandı");
            }

            return new ErrorResult(carimages.Message);
        }

        public async Task<Result> SaveImagesForCarAsync(string carId, List<IFormFile> images)
        {
            var car = await _carReadRepositories.GetByIdAsync(carId);

            if (car == null)
                throw new Exception("Car not found.");

            var imagesFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

            if (!Directory.Exists(imagesFolder))
                Directory.CreateDirectory(imagesFolder);

            if (images != null)
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
                        CarId = car.Id,
                        Id = guid,
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

                return new SuccessResult("başarılı");
            }

            return new ErrorResult("Resimler gelmedi");
        }
    }
}
