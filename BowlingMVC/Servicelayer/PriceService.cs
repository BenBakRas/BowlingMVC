using BowlingMVC.Models;
using BowlingMVC.Servicelayer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingMVC.Servicelayer
{
    public class PriceService : IPriceService
    {
        // Use API service
        private readonly IApiService _apiService;

        public PriceService(IApiService apiService)
        {
            _apiService = apiService;
        }

        // Get all prices from database
        public async Task<List<Price>> GetAllPrices()
        {
            var prices = await _apiService.GetAsync<Price>("prices");
            return prices;
        }

    }
}
