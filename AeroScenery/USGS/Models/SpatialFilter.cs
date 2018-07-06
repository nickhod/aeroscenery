using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS.Models
{
    public class SpatialFilter
    {
        [JsonProperty("filterType")]
        public string FilterType { get; set; }

        [JsonProperty("lowerLeft")]
        public Coordinate LowerLeft { get; set; }

        [JsonProperty("upperRight")]
        public Coordinate UpperRight { get; set; }
    }
}
