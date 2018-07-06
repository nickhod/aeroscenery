using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Data.Models
{
    public class FSCloudPortAirport
    {
        public string ICAO { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int Runways { get; set; }
        public int Buildings { get; set; }
        public int StaticAircraft { get; set; }
        public string Name { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime LastCached { get; set; }
    }
}
