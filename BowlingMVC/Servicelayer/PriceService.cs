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

        public async Task<Price> GetPriceById(int priceId)
        {
            var price = await _apiService.GetAsync<Price>($"prices/{priceId}");
            return null;
        }

        public async Task<Price> CreatePrice(Price price)
        {
            var createdPrice = await _apiService.PostAsync<Price>("prices", price);
            return createdPrice;
        }

        // Other price-related methods can be added as needed.
    }
}
