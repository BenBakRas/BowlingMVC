using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessDTO.DTO;

namespace DataAccessDTO.Interfaces
{
    public interface IBooking : ICrudService<BookingDTO>
    {
        List<Booking> GetBookingsForDate(DateTime date);
    }
}
