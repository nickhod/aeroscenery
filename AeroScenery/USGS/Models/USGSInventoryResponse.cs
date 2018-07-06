using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace AeroScenery.USGS.Models
{
    public abstract class USGSInventoryResponse
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("api_version")]
        public string ApiVersion { get; set; }

        [JsonProperty("access_level")]
        public string AccessLevel { get; set; }

        [JsonProperty("catalog_id")]
        public string CatalogId { get; set; }

        [JsonProperty("executionTime")]
        public decimal ExecutionTime { get; set; }
    }
}
