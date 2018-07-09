using System;
using System.Collections.Generic;
using System.Globalization;
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
        public DateTime LastModifiedDateTime { get; set; }
        public DateTime LastCachedDateTime { get; set; }

        public string LastModified
        {
            get
            {
                return this.LastModifiedDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }

        public string LastCached
        {
            get
            {
                return this.LastCachedDateTime.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }
    }
}
