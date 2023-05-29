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

using BowlingMVC.Servicelayer;
using BowlingMVC.Servicelayer.Interfaces;

namespace BowlingMVC.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;

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
        public async Task<IActionResult> Create(Customers customer)
        {
            if (ModelState.IsValid)
            {
                // Call the API to create the customer
                var createdCustomerId = await _customerService.CreateCustomer(customer);

                if (createdCustomerId >= 0)
                {
                    // Redirect to the customer details page or any other appropriate action
                    //BookingController.Create(Booking booking);
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Failed to create the customer.");
                }
            }

            // If the ModelState is not valid or the API call fails, return the create view with the validation errors
            return View(customer);
        }

        [HttpGet]
        public IActionResult Details()
        {
            return View();
        }


    }
}
