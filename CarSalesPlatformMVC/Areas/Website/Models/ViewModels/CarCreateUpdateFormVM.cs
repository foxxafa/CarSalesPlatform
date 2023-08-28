using Application.DTOs.CarDTO.CreateCarDTO;
using Application.Results;
using Domain.Entities;
using MediatR;

namespace CarSalesPlatformMVC.Areas.Website.Models.ViewModels
{
    public class CarCreateUpdateFormVM : IRequest<Result>
    {
        public CreateCarDTO? Car { get; set; }
        public List<IFormFile>? Files { get; set; }
        public IEnumerable<CarImage>? CarImages { get; set; }
        public int CoverIndex { get; set; }
    }
}
