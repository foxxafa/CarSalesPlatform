using Application.Abstractions.Services;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.CarCommands.CreateCar
{
    public class CreateCarCommandHandler : IRequestHandler<CreateCarCommandRequest, Result>
    {
        private readonly ICarService _carService;
        private readonly ICarImageService _carImageService;
        private readonly IMapper _mapper;

        public CreateCarCommandHandler(ICarService carService, ICarImageService carImageService, IMapper mapper)
        {
            _carService = carService;
            _carImageService = carImageService;
            _mapper = mapper;
        }

        public async Task<Result> Handle(CreateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var carEntity = _mapper.Map<Car>(request.Car);

            // Car nesnesinin bir Id'si yoksa, yeni bir Id atayalım.
            if (carEntity.Id == Guid.Empty)
            {
                carEntity.Id = Guid.NewGuid();
            }

            carEntity.CarIsActive = true;
            var result = await _carService.CreateCarAsync(carEntity); // carEntity kullanıyoruz, çünkü Id'si var.

            if (result.IsSuccess)
            {
                await _carImageService.SaveImagesForCarAsync(carEntity.Id.ToString(), request.Files,request.CoverIndex);
                return new SuccessResult("Resimler kaydedildi");
            }
            else
            {
                return new ErrorResult("Hata");
            }
        }
    }

}
