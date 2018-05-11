using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Data.Models
{
    public class GridSquare
    {
        public long GridSquareId { get; set; }
        public string Name { get; set; }
        public double NorthLatitude { get; set; }
        public double EastLongitude { get; set; }
        public double WestLongitude { get; set; }
        public double SouthLatitude { get; set; }
    }
}
