using BowlingMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BowlingMVC.Controllers
{
    public class BookingController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

       

    }
}
