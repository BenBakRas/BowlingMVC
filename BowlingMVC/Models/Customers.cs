namespace BowlingMVC.Models
{
    public class Customers
    {
        //public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Price? price { get; set; }

        public Customers() 
        { 
        }

        public Customers(string firstName, string lastName, string email, string phone, Price price)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

    }
}
