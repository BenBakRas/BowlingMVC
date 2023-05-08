using System;

namespace DataAccess.DTO
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public CustomerDTO(int inId, string inFirstName, string inLastName, string inEmail, string inPhone)
        {
            Id = inId;
            FirstName = inFirstName;
            LastName = inLastName;
            Email = inEmail;
            Phone = inPhone;
        }
    }
}