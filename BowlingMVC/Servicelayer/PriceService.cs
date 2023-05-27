using BowlingMVC.Models;
using BowlingMVC.Servicelayer.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingMVC.Servicelayer
{
    public class PriceService : IPriceService
    {
        private readonly IApiService _apiService;

        public PriceService(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<Price>> GetAllPrices()
        {
            var prices = await _apiService.GetAsync<Price>("prices");
            return prices;
        }

        // Other price-related methods can be added as needed.
    }
}
