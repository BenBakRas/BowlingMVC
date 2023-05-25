using BowlingMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingMVC.Servicelayer.Interfaces
{
    public interface IPriceService
    {
        Task<List<Price>> GetAllPrices();
        Task<Price> GetPriceById(int priceId);
        Task<Price> CreatePrice(Price price);
        // Other price-related methods can be added as needed.
    }
}
