﻿using Microsoft.AspNetCore.Mvc;

namespace CarSalesPlatformMVC.Areas.Website.Controllers
{
    [Area("Website")]
    public class AboutUsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
