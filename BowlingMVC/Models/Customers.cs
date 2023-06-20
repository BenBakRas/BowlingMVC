using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace BowlingMVC.Models
{
    public class Customers
    {
        public int Id { get; set; }

        [MaxLength (20, ErrorMessage = "Fornavnet er for langt!")]
        public string? FirstName { get; set; }

        [MaxLength(20, ErrorMessage = "Efternavnet er for langt!")]
        public string? LastName { get; set; }

        [MaxLength(50, ErrorMessage = "Den email så lidt suspekt ud!")]
        public string? Email { get; set; }

        [MinLength(8, ErrorMessage = "Telefonnummeret så ikke helt rigtigt ud!")]
        public string? Phone { get; set; }

        public Customers() 
        { 
        }

        public Customers(int id, string firstName, string lastName, string email, string phone)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Phone = phone;
        }

    }
}
