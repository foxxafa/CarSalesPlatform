using Application.Repositories.ICarRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Application.Features.Queries.CarQueries.GetByIDCar
{
    public class GetByIDCarQueryHandler : IRequestHandler<GetByIDCarQueryRequest, DataResult<Car>>
    {
        readonly ICarReadRepositories _carReadRepositories;

        public GetByIDCarQueryHandler(ICarReadRepositories carReadRepositories)
        {
            _carReadRepositories = carReadRepositories;
        }

        public async Task<DataResult<Car>> Handle(GetByIDCarQueryRequest request, CancellationToken cancellationToken)
        {
            var car = await _carReadRepositories.GetByIdAsync(request.Id,false);

            if (car == null)
                return new ErrorDataResult<Car>("Araba bulunamadı");

            return new SuccessDataResult<Car>(car,"Araba getirildi");
        }
    }
}
