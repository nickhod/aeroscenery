using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.CultivationEditor.Models
{
    public class Plant
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int MinHeight { get; set; }
        public int MaxHeight { get; set; }
        public string Group { get; set; }
        public string Species { get; set; }
    }
}
