using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    public class SceneryEditorSettings
    {
        public int? MapControlLastZoomLevel { get; set; }
        public double? MapControlLastX { get; set; }
        public double? MapControlLastY { get; set; }
        public string MapControlLastMapType { get; set; }
        public bool ShowAirports { get; set; }

    }
}
