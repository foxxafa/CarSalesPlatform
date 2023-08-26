using Application.Repositories.ICarImageRepository;
using Application.Results;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Queries.CarQueries.GetImagesByCarId
{
    public class GetImagesByCarIdQueryHandler : IRequestHandler<GetImagesByCarIdQueryRequest, DataResult<IEnumerable<CarImage>>>
    {
        private readonly ICarImageReadRepository _carImageReadRepository;

        public GetImagesByCarIdQueryHandler(ICarImageReadRepository carImageReadRepository)
        {
            _carImageReadRepository = carImageReadRepository;
        }

        public async Task<DataResult<IEnumerable<CarImage>>> Handle(GetImagesByCarIdQueryRequest request, CancellationToken cancellationToken)
        {
            var carImages = await _carImageReadRepository.GetWhere(img => 
                                                                   img.CarId.ToString() == request.CarId).ToListAsync();

            if(carImages.Any())
                return new SuccessDataResult<IEnumerable<CarImage>>(carImages);

            return new ErrorDataResult<IEnumerable<CarImage>>("Resim bulunamadı");

        }
    }
}
