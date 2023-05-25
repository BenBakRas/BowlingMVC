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
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;

        }

        public async Task<IActionResult> Index()
        {
            var apiService = new ApiService();
            var priceService = new PriceService(apiService);

            var prices = await priceService.GetAllPrices();

            return View(prices);
        }
    }
}