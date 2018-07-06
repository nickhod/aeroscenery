using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.USGS.Models
{
    public class LoginResponse : USGSInventoryResponse
    {
        [JsonProperty("data")]
        public string Data { get; set; }
    }
}
