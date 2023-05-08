using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public double NormalPrice { get; set; }
        public double SpecialPrice { get; set; } // Fixed typo
        public string Weekday { get; set; }
        public PriceDTO(int inId, double inNormalPrice, double inSpecialPrice, string inWeekday)
        {
            Id = inId;
            NormalPrice = inNormalPrice;
            SpecialPrice = inSpecialPrice; // Fixed typo
            Weekday = inWeekday;
        }
    }
}
