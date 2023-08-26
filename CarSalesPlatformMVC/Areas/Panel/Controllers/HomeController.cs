using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Panel.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
