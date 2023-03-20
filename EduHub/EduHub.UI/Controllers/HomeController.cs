using EduHub.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EduHub.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Log message generated with INFORMATION severity level.");

           _logger.LogWarning("Log message generated with WARNING severity level.");

           _logger.LogError("Log message generated with ERROR severity level.");

           _logger.LogCritical("Log message log generated with CRITICAL severity level.");

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
    }
}