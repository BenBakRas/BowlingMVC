namespace BowlingMVC.Models
{
    public class Price
    {
        public int PId { get; set; }
        public int NormalPrice { get; set; }
        public int SpeialPrice { get; set; }
        public String Weekday { get; set; }
        public Price() { }
    }
}
