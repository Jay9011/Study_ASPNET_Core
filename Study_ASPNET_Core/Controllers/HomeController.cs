using Microsoft.AspNetCore.Mvc;
using Study_ASPNET_Core.Models;
using System.Diagnostics;
using Microsoft.Extensions.Localization;
using Study_ASPNET_Core.Language;

namespace Study_ASPNET_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IStringLocalizer<Lan> _localizer;

        public HomeController(ILogger<HomeController> logger, IStringLocalizer<Lan> localizer)
        {
            _logger = logger;
            _localizer = localizer;
        }

        public IActionResult Index()
        {
            ViewData["Message"] = _localizer["1"];
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture)
        {
            Lan.ChangeLanguage(HttpContext, culture);
            return RedirectToAction("Index");
        }
    }
}
