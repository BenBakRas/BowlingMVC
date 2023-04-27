namespace BowlingMVC.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public int HoursToPlay { get; set; }
        public int BookingNumber { get; set; }

        public int NoOfPlayers { get; set;}

        public Booking() { }
    }
}
