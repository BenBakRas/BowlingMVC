using BowlingMVC.Models;
using BowlingMVC.Servicelayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BowlingMVC.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly ICustomerService _customerService;

        public BookingController(IBookingService bookingService, ICustomerService customerService)
        {
            _bookingService = bookingService;
            _customerService = customerService;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Booking booking, Customers customer)
        {
            if (ModelState.IsValid)
            {
                // First we try to find the existing customer
                Customers foundCustomer = await _customerService.GetCustomerByPhone(customer.Phone);

                // If customer is found
                if (foundCustomer != null)
                {
                    // Then we assign the customer to our booking.
                    Customers bookingCustomer = new Customers
                    {
                        Id = foundCustomer.Id,
                        FirstName = foundCustomer.FirstName,
                        LastName = foundCustomer.LastName,
                        Email = foundCustomer.Email,
                        Phone = foundCustomer.Phone
                    };

                    // Assign the Customer object to the booking.Customer property
                    booking.Customer = bookingCustomer;

                    // Call the API to create the booking
                    var createdBookingId = await _bookingService.CreateBooking(booking);

                    if (createdBookingId >= 0)
                    {
                        // Redirect to confirm page
                        return RedirectToAction("Confirm", new { id = createdBookingId });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create the booking.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Not able to find the customer.");
                }
            }

            // If the ModelState is not valid or the API call fails, return the create view with the validation errors
            return View(booking);
        }

        public async Task<IActionResult> Confirm(int id)
        {
            // Retrieve the booking details from the API using the booking ID
            var booking = await _bookingService.GetBookingById(id);

            if (booking == null)
            {
                // If the booking is not found, you can handle the error accordingly, such as displaying an error message or redirecting to an error page.
                return RedirectToAction("Index", "Error");
            }

            // Pass the booking details to the confirm view
            return View(booking);
        }

    }
}