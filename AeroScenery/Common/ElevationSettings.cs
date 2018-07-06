using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    public class ElevationSettings
    {
        public bool DownloadElevationData { get; set; }
        public bool RunGeoConvert { get; set; }

        public bool GenerateAIDAndTMCFiles { get; set; }

        public ActionSet ActionSet { get; set; }

        public bool InstallElevationData { get; set; }

        public List<int> AFSLevelsToGenerate { get; set; }
    }
}
