using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BowlingMVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Diagnostics;
using BowlingMVC.Controllers;
using BowlingMVC.Servicelayer;
using BowlingMVC.Servicelayer.Interfaces;

namespace BowlingMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;

        public CustomerController(ICustomerService customerService, IBookingService bookingService)
        {
            _customerService = customerService;
            _bookingService = bookingService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomers();

            return View(customers);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Customers customer, Booking booking)
        {
            if (ModelState.IsValid)
            {

                var createdCustomerId = await _customerService.CreateCustomer(customer);
                if (createdCustomerId >= 0)
                {
                    // Set the customer ID for the booking
                    booking.CustomerId = createdCustomerId;

                    // Call the API to create the booking
                    var createdBookingId = await _bookingService.CreateBooking(booking);

                    if (createdBookingId >= 0)
                    {
                        // Redirect to the customer details page or any other appropriate action
                        return RedirectToAction("Index", "Customer");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to create the booking.");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create the customer.");
                }
            }

            // If the ModelState is not valid or the API call fails, return the create view with the validation errors
            return View(customer); // Pass the customer model to the view to preserve user input
        }



        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }


    }
}
