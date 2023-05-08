using DataAccess.DTO;
using DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using BowlingMVC.Views;

namespace BowlingMVC.Controllers
{
    public class BookingsController : Controller
    {
        private readonly ICrudService<BookingDTO> _bookingService;
        private readonly ICrudService<CustomerDTO> _customerService;
        private readonly ICrudService<PriceDTO> _priceService;
        private readonly ICrudService<LaneDTO> _laneService;

        public BookingsController(
            ICrudService<BookingDTO> bookingService,
            ICrudService<CustomerDTO> customerService,
            ICrudService<PriceDTO> priceService,
            ICrudService<LaneDTO> laneService)
        {
            _bookingService = bookingService;
            _customerService = customerService;
            _priceService = priceService;
            _laneService = laneService;
        }

        public IActionResult Index()
        {
            var bookingDTOs = _bookingService.GetAll().ToList();
            var bookingViewModels = new List<BookingViewModel>();

            foreach (var bookingDTO in bookingDTOs)
            {
                var viewModel = new BookingViewModel
                {
                    Booking = bookingDTO,
                    Customer = _customerService.GetById(bookingDTO.Customer.Id),
                    Price = _priceService.GetById(bookingDTO.Price.Id),
                    Lane = _laneService.GetById(bookingDTO.Lane.Id)
                };

                bookingViewModels.Add(viewModel);
            }

            return View(bookingViewModels);
        }

        public IActionResult Create()
        {
            // TODO: Implement the logic for creating a new booking
            // and initializing the necessary view model properties.
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BookingViewModel viewModel)
        {
            // TODO: Implement the logic for saving the new booking
            // using the view model properties.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            // TODO: Implement the logic for retrieving the booking
            // by id and initializing the necessary view model properties.
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, BookingViewModel viewModel)
        {
            // TODO: Implement the logic for updating the booking
            // using the view model properties.
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            // TODO: Implement the logic for deleting the booking by id.
            return RedirectToAction(nameof(Index));
        }
    }
}
    

