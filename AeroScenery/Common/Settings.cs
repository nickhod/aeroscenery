using AeroScenery.OrthoPhotoSources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Common
{
    public enum ActionSet
    {
        Default,
        Custom
    }

    public class Settings
    {
        public string AFS2SDKDirectory { get; set; }

        public string AFS2Directory { get; set; }

        public string WorkingDirectory { get; set; }

        public string AeroSceneryDBDirectory { get; set; }

        public OrthophotoSource OrthophotoSource { get; set; }

        public int ZoomLevel { get; set; }

        public bool DownloadImageTiles { get; set; }

        public bool StitchImageTiles { get; set; }

        public bool GenerateAIDAndTMCFiles { get; set; }

        public bool RunGeoConvert { get; set; }

        public bool DeleteStitchedImageTiles { get; set; }

        public bool InstallScenery { get; set; }

        public ActionSet ActionSet { get; set; }

        public List<int> AFSLevelsToGenerate { get; set; }

        public string UserAgent { get; set; }


        public int DownloadWaitMs { get; set; }

        public int DownloadWaitRandomMs { get; set; }

        public int SimultaneousDownloads { get; set; }


    }
}
