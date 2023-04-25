namespace BowlingMVC.Models
{
    public class Price
    {
        public int PId { get; set; }
        public double NormalPrice { get; set; }
        public double SpeialPrice { get; set; }
        public String Weekday { get; set; }
        public Price() { }
    }
}
