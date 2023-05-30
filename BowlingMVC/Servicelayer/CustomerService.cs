using BowlingMVC.Models;
using BowlingMVC.Servicelayer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingMVC.Servicelayer
{
    public class CustomerService : ICustomerService
    {
        // Use API service
        private readonly IApiService _apiService;

        public CustomerService(IApiService apiService)
        {
            _apiService = apiService;
        }

        // Get customer by phone number
        public async Task<Customers> GetCustomerByPhone(string phone)
        {
            var foundCustomer = await _apiService.GetAsynced<Customers>($"customers/{phone}");
            return foundCustomer;
        }

        // Create customer
        public async Task<int> CreateCustomer(Customers customer)
        {
            var createdCustomer = await _apiService.PostAsync<int>("customers", customer);
            return createdCustomer;
        }

    }
}
