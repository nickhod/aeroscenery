using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS.Models
{
    public class InventoryScene
    {
        [JsonProperty("acquisitionDate")]
        public string AcquisitionDate { get; set; }

        [JsonProperty("startTime")]
        public string StartTime { get; set; }

        [JsonProperty("endTime")]
        public string EndTime { get; set; }

        [JsonProperty("lowerLeftCoordinate")]
        public string LowerLeftCoordinate { get; set; }

        [JsonProperty("upperLeftCoordinate")]
        public string UpperLeftCoordinate { get; set; }

        [JsonProperty("upperRightCoordinate")]
        public string UpperRightCoordinate { get; set; }

        [JsonProperty("lowerRightCoordinate")]
        public string LowerRightCoordinate { get; set; }

        [JsonProperty("sceneBounds")]
        public string SceneBounds { get; set; }

        [JsonProperty("browseUrl")]
        public string BrowseUrl { get; set; }

        [JsonProperty("dataAccessUrl")]
        public string DataAccessUrl { get; set; }

        [JsonProperty("downloadUrl")]
        public string DownloadUrl { get; set; }

        [JsonProperty("entityId")]
        public string EntityId { get; set; }

        [JsonProperty("displayId")]
        public string DisplayId { get; set; }

        [JsonProperty("metadataUrl")]
        public string MetadataUrl { get; set; }

        [JsonProperty("fgdcMetadataUrl")]
        public string FgdcMetadataUrl { get; set; }

        [JsonProperty("modifiedDate")]
        public string ModifiedDate { get; set; }

        [JsonProperty("orderUrl")]
        public string OrderUrl { get; set; }

        [JsonProperty("summary")]
        public string Summary { get; set; }

    }
}
