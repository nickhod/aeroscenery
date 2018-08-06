using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    public class GeoArea
    {
        public double NorthLatitude { get; set; }
        public double SouthLatitude { get; set; }
        public double EastLongitude{ get; set; }
        public double WestLongitude { get; set; }

        public GeoArea()
        {

        }

        public GeoArea(double northLatitude, double southLatitude, double eastLongitude, double westLongitude)
        {
            this.NorthLatitude = northLatitude;
            this.SouthLatitude = southLatitude;
            this.EastLongitude = eastLongitude;
            this.WestLongitude = westLongitude;
        }
    }
}
