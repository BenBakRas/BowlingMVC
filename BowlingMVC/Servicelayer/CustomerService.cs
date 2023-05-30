using BowlingMVC.Models;
using BowlingMVC.Servicelayer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingMVC.Servicelayer
{
    public class CustomerService : ICustomerService
    {
        private readonly IApiService _apiService;

        public CustomerService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Customers>> GetAllCustomers()
        {
            var customers = await _apiService.GetAsync<Customers>("customers");
            return customers;
        }

        public async Task<Customers> GetCustomerByPhone(string phone)
        {
            var foundCustomer = await _apiService.GetAsynced<Customers>($"customers/{phone}");
            return foundCustomer;
        }


        public async Task<int> CreateCustomer(Customers customer)
        {
            var createdCustomer = await _apiService.PostAsync<int>("customers", customer);
            return createdCustomer;
        }


        // Other price-related methods can be added as needed.
    }
}
