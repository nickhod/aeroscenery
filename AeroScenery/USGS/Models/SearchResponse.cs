using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS.Models
{
    public class SearchResponse : USGSInventoryResponse
    {

        [JsonProperty("numberReturned")]
        public int NumberReturned { get; set; }

        [JsonProperty("totalHits")]
        public int TotalHits { get; set; }

        [JsonProperty("firstRecord")]
        public int FirstRecord { get; set; }

        [JsonProperty("lastRecord")]
        public int LastRecord { get; set; }

        [JsonProperty("nextRecord")]
        public int NextRecord { get; set; }

        [JsonProperty("results")]
        public InventoryScene[] Results { get; set; }

    }
}
