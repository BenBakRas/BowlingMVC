using BowlingMVC.Models;
using BowlingMVC.Servicelayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BowlingMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking)
        {

            if (ModelState.IsValid)
            {
                // Call the API to create the customer
                var createdBookingId = await _bookingService.CreateBooking(booking);

                if (createdBookingId >= 0)
                {
                    // Redirect to the customer details page or any other appropriate action
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create the customer.");
                }
            }

            // If the ModelState is not valid or the API call fails, return the create view with the validation errors
            return View(booking);

        }


    }
}
