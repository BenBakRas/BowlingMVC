using BowlingMVC.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BowlingMVC.Servicelayer.Interfaces
{
    public interface IPriceService
    {
        Task<List<Price>> GetAllPrices();

    }
}
