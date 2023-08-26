using Application.Abstractions.Services;
using Application.Repositories.ICarImageRepository;
using Application.Repositories.ICarRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.CarCommands.DeleteCar
{
    public class DeleteCarCommandHandler : IRequestHandler<DeleteCarCommandRequest, Result>
    {
        readonly ICarReadRepositories _carReadRepositories;
        readonly ICarWriteRepositories _carWriteRepositories;
        readonly ICarImageReadRepository _carImageReadRepository;
        readonly ICarImageWriteRepository _carImageWriteRepository;

        public DeleteCarCommandHandler(ICarReadRepositories carReadRepositories,
                                       ICarWriteRepositories carWriteRepositories,
                                       ICarImageReadRepository carImageReadRepository,
                                       ICarImageWriteRepository carImageWriteRepository)
        {
            _carReadRepositories = carReadRepositories;
            _carWriteRepositories = carWriteRepositories;
            _carImageReadRepository = carImageReadRepository;
            _carImageWriteRepository = carImageWriteRepository;
        }

        public async Task<Result> Handle(DeleteCarCommandRequest request, CancellationToken cancellationToken)
        {
            var car = await _carReadRepositories.GetByIdAsync(request.carId, false); 

            if (car == null) return new ErrorResult("Araba Bulunamadı");

            var carImages=  _carImageReadRepository.GetWhere(x => x.CarId==car.Id).ToList();

            if (carImages != null) 
            {
                foreach (var image in carImages)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", image.ImagePath);
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }
            }

            var result = _carWriteRepositories.Remove(car);

            if (result.IsSuccess)
            {
                await _carWriteRepositories.SaveAsync();
                return new SuccessResult("Araba ve ilişkili resimler silindi");
            }
            else
            {
                return new ErrorResult("Hata");
            }
        }
    }
}
