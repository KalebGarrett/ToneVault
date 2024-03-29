﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToneVault.Models;
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

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var model = new IndexViewModel
        {
            Tones = await _toneService.Get()
        };
        ViewBag.VideoPath = "../vid/guitarist_playing_a_guitar (1080p).mp4";
        return View(model);
    }

    [HttpGet("browse")]
    public async Task<IActionResult> Browse()
    {
        var model = new BrowseViewModel
        {
            Tones = await _toneService.Get()
        };
        ViewBag.ImagePath = "../img/luana-azevedo-OYVaNuVoqVw-unsplash.jpg";
        return View(model);
    }

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _toneService.Delete(id);
        ViewBag.ImagePath = "../img/luana-azevedo-OYVaNuVoqVw-unsplash.jpg";
        return RedirectToAction("Browse");
    }

    [HttpGet("tone/{id}")]
    public async Task<IActionResult> Tone(string id)
    {
        var model = new ToneViewModel
        {
            Tone = await _toneService.Get(id)
        };
        ViewBag.ImagePath = "../img/luana-azevedo-OYVaNuVoqVw-unsplash.jpg";
        return View(model);
    }

    [HttpGet("addtone")]
    public IActionResult AddTone()
    {
        ViewBag.ImagePath = "../img/parker-coffman-GgsG8aNLgjQ-unsplash.jpg";
        return View();
    }

    [HttpPost("addtone")]
    public async Task<IActionResult> AddTone(ToneFormModel model)
    {
        model.Tone.Id = Guid.NewGuid().ToString();
        await _toneService.Create(model.Tone);
        ViewBag.ImagePath = "../img/parker-coffman-GgsG8aNLgjQ-unsplash.jpg";
        return RedirectToAction("AddTone");
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> EditTone(string id)
    {
        var model = new ToneFormModel
        {
            Tone = await _toneService.Get(id)
        };
        ViewBag.ImagePath = "../img/parker-coffman-GgsG8aNLgjQ-unsplash.jpg";
        return View(model);
    }

    [HttpPost("edit/{id}")]
    public async Task<IActionResult> EditTone(ToneFormModel model)
    {
        await _toneService.Update(model.Tone.Id, model.Tone);
        return RedirectToAction("Browse");
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