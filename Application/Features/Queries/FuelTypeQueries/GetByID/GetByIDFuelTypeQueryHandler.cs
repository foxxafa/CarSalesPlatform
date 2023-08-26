using Application.Features.Queries.ColorQueries.GetByID;
using Application.Repositories.IColorRepositories;
using Application.Repositories.IFuelTypeRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.FuelTypeQueries.GetByID
{
    public class GetByIDFuelTypeQueryHandler : IRequestHandler<GetByIDFuelTypeQueryRequest, DataResult<FuelType>>
    {
        readonly IFuelTypeReadRepositories _fuelTypeReadRepositories;

        public GetByIDFuelTypeQueryHandler(IFuelTypeReadRepositories fuelTypeReadRepositories)
        {
            _fuelTypeReadRepositories = fuelTypeReadRepositories;
        }

        public async Task<DataResult<FuelType>> Handle(GetByIDFuelTypeQueryRequest request, CancellationToken cancellationToken)
        {
            var fuelType = await _fuelTypeReadRepositories.GetByIdAsync(request.Id, false);

            if (fuelType == null)
                return new ErrorDataResult<FuelType>("Yakıt türü bulunamadı");

            return new SuccessDataResult<FuelType>(fuelType);
        }
    }
}
