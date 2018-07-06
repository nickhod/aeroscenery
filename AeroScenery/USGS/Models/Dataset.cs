using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS.Models
{
    public class Dataset
    {
        [JsonProperty("datasetName")]
        public string DatasetName { get; set; }

        [JsonProperty("datasetFullName")]
        public string DatasetFullName { get; set; }

        [JsonProperty("supportDownload")]
        public bool SupportDownload { get; set; }

        [JsonProperty("supportBulkDownload")]
        public bool SupportBulkDownload { get; set; }

        [JsonProperty("bulkDownloadOrderLimit")]
        public int BulkDownloadOrderLimit { get; set; }

        [JsonProperty("supportOrder")]
        public bool SupportOrder { get; set; }

        [JsonProperty("orderLimit")]
        public int OrderLimit { get; set; }

        [JsonProperty("totalScenes")]
        public int TotalScenes { get; set; }
    }
}
