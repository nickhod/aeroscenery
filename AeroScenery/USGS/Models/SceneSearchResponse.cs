using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS.Models
{
    public class SceneSearchResponse : USGSInventoryResponse
    {
        [JsonProperty("data")]
        public SearchResponse Data { get; set; }
    }
}
