using Application.Results;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services
{
    public interface ICarImageService
    {
        Task<Result> SaveImagesForCarAsync(string carId, List<IFormFile> images,int CoverIndex);
    }
}
