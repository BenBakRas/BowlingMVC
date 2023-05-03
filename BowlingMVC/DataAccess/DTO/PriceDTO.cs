using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessDTO.DTO
{
    public class PriceDTO
    {
        public int Id { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal SpecialPrice { get; set; }
        public int Hour { get; set; }
        public string? Weekday { get; set; }

    }
}
