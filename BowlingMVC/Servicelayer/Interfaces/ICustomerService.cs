using BowlingMVC.Models;

namespace BowlingMVC.Servicelayer.Interfaces
{
    public interface ICustomerService
    {
        Task<List<Customers>> GetAllCustomers();
        Task<Customers> GetCustomerByPhone(string phone);
        Task<int> CreateCustomer(Customers customer);
        
    }
}
