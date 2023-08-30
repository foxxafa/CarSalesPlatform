using Application.Abstractions.Services;
using Domain.Entities;
using Microsoft.Extensions.Caching.Memory;

namespace CarSalesPlatformMVC.Areas.Website.Chache
{
    public class CacheInitializer : IHostedService
    {
        private readonly IMemoryCache _cache;
        private readonly IServiceScopeFactory _scopeFactory;

        public static List<string> SuggestionsList; // Ekleme: Suggestions list

        public CacheInitializer(IMemoryCache cache, IServiceScopeFactory scopeFactory)
        {
            _cache = cache;
            _scopeFactory = scopeFactory;
        }

        public void PopulateSuggestionsList()
        {
            // Önbellekte saklanan verileri al
            var brands = _cache.Get<List<Brand>>("Brands") ?? new List<Brand>();
            var colors = _cache.Get<List<Color>>("Colors") ?? new List<Color>();
            var fuelTypes = _cache.Get<List<FuelType>>("FuelTypes") ?? new List<FuelType>();
            var categories = _cache.Get<List<Category>>("Categories") ?? new List<Category>();
            var gearTypes = _cache.Get<List<GearType>>("GearTypes") ?? new List<GearType>();

            // SuggestionsList temizle
            SuggestionsList = new List<string>();

            // SuggestionsList'i doldur
            SuggestionsList.AddRange(brands.Select(b => b.Name));
            SuggestionsList.AddRange(colors.Select(c => c.Name));
            SuggestionsList.AddRange(fuelTypes.Select(f => f.Type));
            SuggestionsList.AddRange(categories.Select(c => c.Name));
            SuggestionsList.AddRange(gearTypes.Select(g => g.Type));
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var carService = scope.ServiceProvider.GetRequiredService<ICarService>();

                var cachedBrands = (await carService.GetCarBrandsAsync()).Data.ToList();
                _cache.Set("Brands", cachedBrands);

                var cachedColors = (await carService.GetCarColorsAsync()).Data.ToList();
                _cache.Set("Colors", cachedColors);

                var cachedFuelTypes = (await carService.GetCarFuelTypesAsync()).Data.ToList();
                _cache.Set("FuelTypes", cachedFuelTypes);

                var cachedCategories = (await carService.GetCarCategoriesAsync()).Data.ToList();
                _cache.Set("Categories", cachedCategories);

                var cachedGearTypes = (await carService.GetCarGearTypesAsync()).Data.ToList();
                _cache.Set("GearTypes", cachedGearTypes);

                PopulateSuggestionsList();
            }
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
