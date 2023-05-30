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
    //Instantiate services
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IBookingService _bookingService;

        public CustomerController(ICustomerService customerService, IBookingService bookingService)
        {
            _customerService = customerService;
            _bookingService = bookingService;
        }

        //Get index view - "start-view"
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        //Create form-page
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Create post method
        [HttpPost]
        public async Task<IActionResult> Create(Customers customer, Booking booking)
        {
            //Check if all models are validated
            if (ModelState.IsValid)
            {
                // Create new customer - if success assign the customer to the booking
                int createdCustomerId = await _customerService.CreateCustomer(customer);
                if (createdCustomerId >= 0)
                {
                    Customers bookingCustomer = new Customers
                    {
                        Id = createdCustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        Email = customer.Email,
                        Phone = customer.Phone
                    };

                    // Assign the Customer object to the booking.Customer property
                    booking.Customer = bookingCustomer;

                    // Call the API to create the booking
                    int createdBookingId = await _bookingService.CreateBooking(booking);

                    //If booking is successfull
                    if (createdBookingId >= 0)
                    {
                        // Redirect to the customer details page or any other appropriate action
                        return RedirectToAction("Confirm", "Booking", new { id = createdBookingId });
                    }
                    else
                    {
                        // Problem creating booking
                        ModelState.AddModelError("", "Failed to create the booking.");
                    }
                }
                else
                {
                    // Problem creating customer
                    ModelState.AddModelError("", "Failed to create the customer.");
                }
            }

            // If the ModelState is not valid or the API call fails, return the create view with the validation errors
            return View(customer); // Pass the customer model to the view to preserve user input
        }



        


    }
}
