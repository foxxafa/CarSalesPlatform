using Application.Features.Commands.CarCommands.UpdateCar;
using FluentValidation;

namespace Application.Validators
{
    public class UpdateCarValidator : AbstractValidator<UpdateCarCommandRequest>
    {
        public UpdateCarValidator()
        {
            RuleFor(x => x.Car.BrandId).NotNull().WithMessage("Lütfen Marka Seçin");
            RuleFor(x => x.Car.ColorId).NotNull().WithMessage("Lütfen Renk Seçin");
            RuleFor(x => x.Car.CategoryId).NotNull().WithMessage("Lütfen Kategori Seçin");
            RuleFor(x => x.Car.FuelTypeId).NotNull().WithMessage("Lütfen Yakıt Türü Seçin");
            RuleFor(x => x.Car.GearTypeId).NotNull().WithMessage("Lütfen Vites Türü Seçin");

            RuleFor(x => x.Car.Name).NotNull().WithMessage("Araba için başlık girin");
            RuleFor(x => x.Car.Name).MinimumLength(50).WithMessage("Daha kısa giriniz");

            RuleFor(x => x.Car.Description).NotNull().WithMessage("Lütfen arabaya açıklama girin");

            RuleFor(x => x.Car.FuelTankCapacity).NotNull().WithMessage("Lütfen kapasite bilgisi girin");
            RuleFor(x => x.Car.FuelTankCapacity).GreaterThan(0);

            RuleFor(x => x.Car.Year).GreaterThan(1900);
            RuleFor(x => x.Car.Year).NotNull().WithMessage("Lütfen yıl bigisi girin");

            RuleFor(x => x.Car.Mileage).GreaterThan(0).WithMessage("KM bilgisi negatif olamaz");

            RuleFor(x => x.Car.EngineDescription).NotNull().WithMessage("Lütfen motor açıklaması girin");

            RuleFor(x => x.Car.SeatCount).NotNull().WithMessage("Koltuk sayısını girin");
            RuleFor(x => x.Car.SeatCount).GreaterThan(0);

            RuleFor(x => x.Car.Longitude).NotNull().WithMessage("Konum seçilmelidir");

            RuleFor(x=> x.Car.CarHeader).NotNull().WithMessage("Başlık kısmı boş bırakılamaz");
            RuleFor(x=> x.Car.CarHeader).MaximumLength(50).WithMessage("Daha kısa bir başlık oluşturun");
        }
    }
}
