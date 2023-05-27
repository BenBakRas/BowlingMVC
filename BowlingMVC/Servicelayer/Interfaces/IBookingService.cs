using BowlingMVC.Models;

namespace BowlingMVC.Servicelayer.Interfaces
{
    public interface IBookingService
    {
        Task<Booking> GetBookingById(int bookingId);
        Task<int> CreateBooking(Booking booking);

    }
}
