using Application.Abstractions.Services;
using CarSalesPlatformMVC.Areas.Website.Chache;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    public class HomeController : Controller
    {
        private IMemoryCache _cache;
        readonly IMediator _mediator;
        readonly ICarService _carService;

        public HomeController(IMediator mediator, ICarService carService, IMemoryCache cache)
        {
            _mediator = mediator;
            _carService = carService;
            _cache = cache;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Brands = _cache.Get<List<Brand>>("Brands");
            ViewBag.Colors = _cache.Get<List<Color>>("Colors");
            ViewBag.FuelTypes = _cache.Get<List<FuelType>>("FuelTypes");
            ViewBag.Categories = _cache.Get<List<Category>>("Categories");
            ViewBag.GearTypes = _cache.Get<List<GearType>>("GearTypes");

            return View();
        }

        [HttpGet("[controller]/[action]")]
        public IEnumerable<string> GetSuggestions(string query)
        {
            var keywords = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (keywords.Length == 0) return Enumerable.Empty<string>();

            return CacheInitializer.SuggestionsList
                       .Where(s => s.StartsWith(keywords.Last(), StringComparison.OrdinalIgnoreCase))
                       .Take(5);
        }
    }
}
