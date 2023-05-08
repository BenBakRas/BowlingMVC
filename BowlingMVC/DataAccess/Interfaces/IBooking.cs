using ShModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DTO;

namespace DataAccess.Interfaces
{
    public interface IBooking : ICrudService<BookingDTO>
    {
        List<Booking> GetBookingsForDate(DateTime date);
    }
}
