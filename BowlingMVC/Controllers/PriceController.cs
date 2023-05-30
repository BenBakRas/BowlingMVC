using BowlingMVC.Models;
using BowlingMVC.Servicelayer;
using BowlingMVC.Servicelayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;

namespace BowlingMVC.Controllers
{
    public class PriceController : Controller
    {
        // Instantiate service
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;

        }
        
        // Index view, showing all prices
        public async Task<IActionResult> Index()
        {
            var prices = await _priceService.GetAllPrices();

            return View(prices);
        }
    }
}