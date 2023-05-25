using BowlingMVC.Models;

namespace BowlingMVC.Servicelayer.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetAllCustomers();
        Task<Customers> GetCustomerById(int customerId);
        Task<int> CreateCustomer(Customers customer);
        // Other price-related methods can be added as needed.
    }
}
