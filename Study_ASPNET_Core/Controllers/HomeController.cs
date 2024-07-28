using Microsoft.AspNetCore.Mvc;
using Study_ASPNET_Core.Models;
using System.Diagnostics;
using Microsoft.Extensions.Localization;
using Study_ASPNET_Core.Language;
using Study_ASPNET_Core.Services.FileIOService;

namespace Study_ASPNET_Core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IStringLocalizer<Lan> _localizer;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment, IStringLocalizer<Lan> localizer)
        {
            _logger = logger;
            _environment = environment;
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
        public IActionResult Upload()
        {
            var fileIOService = new FileIOService(_environment);
            var uploadedFiles = fileIOService.UploadFiles(Request.Form.Files);

            return Ok(new { message = "Files uploaded successfully", files = uploadedFiles });
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture)
        {
            Lan.ChangeLanguage(HttpContext, culture);
            return RedirectToAction("Index");
        }
    }
}
