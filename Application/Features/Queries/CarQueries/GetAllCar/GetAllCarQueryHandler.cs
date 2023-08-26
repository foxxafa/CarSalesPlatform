using Application.Repositories.ICarRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.CarQueries.GetAllCar
{
    public class GetAllCarQueryHandler : IRequestHandler<GetAllCarQueryRequest, DataResult<List<Car>>>
    {
        readonly ICarReadRepositories _carReadRepositories;

        public GetAllCarQueryHandler(ICarReadRepositories carReadRepositories)
        {
            _carReadRepositories = carReadRepositories;
        }

        public async Task<DataResult<List<Car>>> Handle(GetAllCarQueryRequest request, CancellationToken cancellationToken)
        {
            var cars = await _carReadRepositories.GetAll(false).ToListAsync(cancellationToken);
            
            if(cars.Any())
                return new SuccessDataResult<List<Car>>(cars,"Arabalar listelendi");

            return new ErrorDataResult<List<Car>>("Araba getirilemedi");
        }
    }
}
