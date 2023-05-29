namespace BowlingMVC.Models
{
    public class Price
    {
        public int Id { get; set; }
        public double NormalPrice { get; set; }
        public string Weekday { get; set; } = string.Empty;
        public Price() 
        { 
        
        }
    }
}
