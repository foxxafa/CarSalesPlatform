using Application.Abstractions.Services;
using Application.Features.Queries.BrandQueries.GetAllBrand;
using Application.Features.Queries.CategoryQueries.GetAllCategory;
using Application.Features.Queries.ColorQueries.GetAllColor;
using Application.Features.Queries.FuelTypeQueries.GetAllFuelType;
using Application.Features.Queries.GearTypeQueries.GetAllGearType;
using Application.Repositories.ICarRepositories;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace Persistence.Services
{
    public class CarService : ICarService
    {
        private readonly IMediator _mediator;
        readonly ICarWriteRepositories _carWriteRepositories;

        public CarService(IMediator mediator, ICarWriteRepositories carWriteRepositories)
        {
            _mediator = mediator;
            _carWriteRepositories = carWriteRepositories;
        }

        public async Task<Result> CreateCarAsync(Car car)
        {

            var result = await _carWriteRepositories.AddAsync(car);

            if (result.IsSuccess)
            {
                await _carWriteRepositories.SaveAsync();
                return new SuccessResult("Araba başarıyla oluşturuldu.");
            }

            return new ErrorResult("Araba oluşturma başarısız.");
        }


        public Task<Result> DeleteCarAsync(Guid carId)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<Brand>>> GetCarBrandsAsync()
        {
            var query = new GetAllBrandQueryRequest();
            var result = await _mediator.Send(query);

            return new DataResult<List<Brand>>(result.Data, result.IsSuccess, result.Message);
        }

        public async Task<DataResult<List<Category>>> GetCarCategoriesAsync()
        {
            var query = new GetAllCategoryQueryRequest();
            var result = await _mediator.Send(query);

            return new DataResult<List<Category>>(result.Data, result.IsSuccess, result.Message);
        }

        public async Task<DataResult<List<Color>>> GetCarColorsAsync()
        {
            var query = new GetAllColorQueryRequest();
            var result = await _mediator.Send(query);

            return new DataResult<List<Color>>(result.Data, result.IsSuccess, result.Message);
        }

        public async Task<DataResult<List<FuelType>>> GetCarFuelTypesAsync()
        {
            var query = new GetAllFuelTypeQueryRequest();
            var result = await _mediator.Send(query);
            
            return new DataResult<List<FuelType>>(result.Data,result.IsSuccess,result.Message);
        }

        public async Task<DataResult<List<GearType>>> GetCarGearTypesAsync()
        {
            var query = new GetAllGearTypeQueryRequest();
            var result = await _mediator.Send(query);

            return new DataResult<List<GearType>>(result.Data,result.IsSuccess,result.Message);
        }

        public Task<Result> UpdateCarAsync(Car car)
        {
            throw new NotImplementedException();
        }
    }

}
