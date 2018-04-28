using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    public class Settings
    {
        public string AFS2SDKDirectory { get; set; }

        public string AFS2Directory { get; set; }

        public string WorkingDirectory { get; set; }

        public OrthophotoSource OrthophotoSource { get; set; }

        public int ZoomLevel { get; set; }

        public Settings()
        {
            // TODO - Temp
            this.AFS2SDKDirectory = @"E:\Games\aerofly_fs_2_sdk_tools_20171123\aerofly_fs_2_sdk_tools\aerofly_fs_2_geoconvert\";
            this.AFS2SDKDirectory = @"C:\Temp\AeroScenery\";

            this.WorkingDirectory = @"E:\Games\Temp\";
            this.WorkingDirectory = @"C:\Temp\AeroScenery\";
        }
    }
}
