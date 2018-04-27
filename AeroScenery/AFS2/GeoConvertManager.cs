using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.AFS2
{
    public class GeoConvertManager
    {

        public void RunGeoConvert()
        {
            var geoconvertFullPath = AeroSceneryManager.Instance.Settings.AFS2SDKDirectory + "aerofly_fs_2_geoconvert.exe";
            var options = "config-sample-only.tmc";

            Process.Start(geoconvertFullPath);
        }
    }
}
