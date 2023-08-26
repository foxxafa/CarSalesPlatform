using Application.Repositories.ICarRepositories;
using Application.Results;
using MediatR;

namespace Application.Features.Commands.CarCommands.UpdateCarStatus
{

    public class UpdateCarStatusCommandHandler : IRequestHandler<UpdateCarStatusCommandRequest, DataResult<bool>>
    {
        private readonly ICarWriteRepositories _carWriteRepositories;
        private readonly ICarReadRepositories _carReadRepositories;

        public UpdateCarStatusCommandHandler(ICarWriteRepositories carWriteRepositories, ICarReadRepositories carReadRepositories)
        {
            _carWriteRepositories = carWriteRepositories;
            _carReadRepositories = carReadRepositories;
        }

        public async Task<DataResult<bool>> Handle(UpdateCarStatusCommandRequest request, CancellationToken cancellationToken)
        {
            var car = await _carReadRepositories.GetSingleAsync(c => c.Id == request.CarId);

            if (car == null)
                return new ErrorDataResult<bool>("Araba bulunamadı");

            car.CarIsActive = !car.CarIsActive;

            _carWriteRepositories.Update(car);
            await _carWriteRepositories.SaveAsync();

            return new SuccessDataResult<bool>(car.CarIsActive,"Araba durumu değiştirildi.");
        }
    }

}
