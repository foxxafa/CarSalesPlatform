using Application.Abstractions.Services;
using Application.Repositories.ICarRepositories;
using Application.Results;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Commands.CarCommands.UpdateCar
{
    public class UpdateCarCommandHandler : IRequestHandler<UpdateCarCommandRequest, Result>
    {
        readonly ICarReadRepositories _carReadRepositories;
        readonly ICarWriteRepositories _carWriteRepositories;
        readonly IMapper _mapper;
        readonly ICarImageService _imageService;

        public UpdateCarCommandHandler(ICarReadRepositories carReadRepositories, ICarWriteRepositories carWriteRepositories, IMapper mapper, ICarImageService imageService)
        {
            _carReadRepositories = carReadRepositories;
            _carWriteRepositories = carWriteRepositories;
            _mapper = mapper;
            _imageService = imageService;
        }

        public async Task<Result> Handle(UpdateCarCommandRequest request, CancellationToken cancellationToken)
        {
            var car = await _carReadRepositories.GetSingleAsync(p=> p.Id==request.Car.CarId);

            if (car==null)
            {
                return new ErrorResult("Araba bulunamadı");
            }
            
            _mapper.Map(request.Car, car);

            _carWriteRepositories.Update(car);
            await _carWriteRepositories.SaveAsync();

            await _imageService.SaveImagesForCarAsync(car.Id.ToString(), request.Files,request.CoverIndex);

            return new SuccessResult("Araba güncellendi");
        }
    }
}
