using Application.Repositories.IFuelTypeRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
namespace Application.Features.Queries.FuelTypeQueries.GetAllFuelType
{
    public class GetAllFuelTypeQueryHandler : IRequestHandler<GetAllFuelTypeQueryRequest, DataResult<List<FuelType>>>
    {
        readonly IFuelTypeReadRepositories _fuelTypeReadRepositories;

        public GetAllFuelTypeQueryHandler(IFuelTypeReadRepositories fuelTypeReadRepositories)
        {
            _fuelTypeReadRepositories = fuelTypeReadRepositories;
        }

        public async Task<DataResult<List<FuelType>>> Handle(GetAllFuelTypeQueryRequest request, CancellationToken cancellationToken)
        {
            var fuelTypes = await _fuelTypeReadRepositories.GetAll(false).ToListAsync(cancellationToken);

            if (fuelTypes.Any())
                return new SuccessDataResult<List<FuelType>>(fuelTypes,"Yakıt türleri listelendi");

            return new ErrorDataResult<List<FuelType>>("Yakıt türü yok");
        }
    }
}
