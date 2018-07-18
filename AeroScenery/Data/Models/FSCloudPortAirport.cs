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

        public string Url { get; set; }

        public string LastModified { get; set; }

        public string LastCached { get; set; }

        public DateTime LastModifiedDateTime
        {
            get
            {
                return DateTime.ParseExact(this.LastModified, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            set
            {
                this.LastModified =  value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }

        public DateTime LastCachedDateTime
        {
            get
            {
                return DateTime.ParseExact(this.LastCached, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
            set
            {
                this.LastCached = value.ToString("yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }
    }
}
