using System.Diagnostics;
using EduHub.Application.Interfaces;
using EduHub.UI.Models;
using Microsoft.AspNetCore.Mvc;

namespace EduHub.UI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICourseService _courseService;

    public HomeController(ILogger<HomeController> logger, ICourseService courseService)
    {
        _logger = logger;
        _courseService = courseService;
    }

    public IActionResult Index()
    {
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

    public async Task<IActionResult> Search(string? search)
    {
        ViewData["CurrentFilter"] = search;


        var res = await _courseService.GetCoursesAsync(search);


        return View(res);
    }
}