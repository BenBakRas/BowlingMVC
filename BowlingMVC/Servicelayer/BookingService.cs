using BowlingMVC.Models;
using BowlingMVC.Servicelayer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingMVC.Servicelayer
{
    public class BookingService : IBookingService
    {
        // Use API service
        private readonly IApiService _apiService;

        public BookingService (IApiService apiService)
        {
            _apiService = apiService;
        }

        // Get a booking by ID
        public async Task<Booking> GetBookingById(int bookingId)
        {
            var booking = await _apiService.GetAsynced<Booking>($"bookings/{bookingId}");
            return booking;
        }

        // Create booking - post to db/api
        public async Task<int> CreateBooking(Booking booking)
        {
            var createdBooking = await _apiService.PostAsync<int>("bookings", booking);
            return createdBooking;
        }


        // Other price-related methods can be added as needed.
    }
}
