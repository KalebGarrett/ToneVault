using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToneVault.Web.Models;
using ToneVault.Web.Services;

namespace ToneVault.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ToneService _toneService;

    public HomeController(ILogger<HomeController> logger, ToneService toneService)
    {
        _logger = logger;
        _toneService = toneService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<IActionResult> Browse()
    {
        var model = new BrowseViewModel
        {
            Tones = await _toneService.Get()
        };
        return View(model);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}