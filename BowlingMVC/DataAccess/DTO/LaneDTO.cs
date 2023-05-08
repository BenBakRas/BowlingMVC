using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DTO
{
    public class LaneDTO
    {
        public int Id { get; set; }

        public int LaneNumber { get; set; }

        public LaneDTO(int inId, int inLaneNumber)
        {
            Id = inId;
            LaneNumber = inLaneNumber;
        }
    }
}