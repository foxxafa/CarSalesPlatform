using Application.Abstractions.Services.Authentications;
using Application.Abstractions.Services;
using Application.Repositories.IBrandRepositories;
using Application.Repositories.ICarRepositories;
using Application.Repositories.ICategoryRepositories;
using Application.Repositories.IColorRepositories;
using Application.Repositories.IFuelTypeRepositories;
using Application.Repositories.IGearTypeRepositories;
using Application.Repositories.IRequestStatusRepositories;
using Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contexts;
using Persistence.Repositories.BrandRepositories;
using Persistence.Repositories.CarRepositories;
using Persistence.Repositories.CategoryRepositories;
using Persistence.Repositories.ColorRepositories;
using Persistence.Repositories.FuelTypeRepositories;
using Persistence.Repositories.GearTypeRepositories;
using Persistence.Repositories.RequestStatusRepository;
using Persistence.Services;
using Persistence.Repositories.CarImageRepository;
using Application.Repositories.ICarImageRepository;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<CarSalesPlatformDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddScoped<ICarReadRepositories, CarReadRepositories>();
            services.AddScoped<ICarWriteRepositories, CarWriteRepositories>();

            services.AddScoped<IColorReadRepositories, ColorReadRepositories>();
            services.AddScoped<IColorWriteRepositories, ColorWriteRepositories>();

            services.AddScoped<IGearTypeReadRepositories, GearTypeReadRepositories>();
            services.AddScoped<IGearTypeWriteRepositories, GearTypeWriteRepositories>();

            services.AddScoped<IBrandReadRepositories, BrandReadRepositories>();
            services.AddScoped<IBrandWriteRepositories, BrandWriteRepositories>();

            services.AddScoped<ICategoryReadRepositories, CategoryReadRepositories>();
            services.AddScoped<ICategoryWriteRepositories, CategoryWriteRepositories>();

            services.AddScoped<IFuelTypeReadRepositories, FuelTypeReadRepositories>();
            services.AddScoped<IFuelTypeWriteRepositories, FuelTypeWriteRepositories>();

            services.AddScoped<IRequestStatusReadRepositories, RequestStatusReadRepository>();
            services.AddScoped<IRequestStatusWriteRepositories, RequestStatusWriteRepository>();

            services.AddScoped<ICarImageReadRepository, CarImageReadRepository>();
            services.AddScoped<ICarImageWriteRepository, CarImageWriteRepository>();

            services.AddIdentity<AppUser, AppRole>(
                options =>
                {
                    options.Password.RequiredLength = 3;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                }).AddEntityFrameworkStores<CarSalesPlatformDbContext>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IInternalAuthentication, AuthService>();
            services.AddScoped<IExternalAuthentication, AuthService>();

            services.AddTransient<ICarService, CarService>();
            services.AddScoped<ICarImageService, CarImageService>();
        }
    }
}
