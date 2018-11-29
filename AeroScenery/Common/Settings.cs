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
        public Settings()
        {
            this.ElevationSettings = new ElevationSettings();
        }

        public string AFS2SDKDirectory { get; set; }

        public string AFS2Directory { get; set; }

        public string WorkingDirectory { get; set; }

        public string AeroSceneryDBDirectory { get; set; }

        public OrthophotoSource? OrthophotoSource { get; set; }

        public int? ZoomLevel { get; set; }

        public bool? DownloadImageTiles { get; set; }

        public bool? StitchImageTiles { get; set; }

        public bool? GenerateAIDAndTMCFiles { get; set; }

        public bool? RunGeoConvert { get; set; }

        public bool? DeleteStitchedImageTiles { get; set; }

        public bool? InstallScenery { get; set; }

        public ActionSet? ActionSet { get; set; }

        public List<int> AFSLevelsToGenerate { get; set; }

        public string UserAgent { get; set; }


        public int? DownloadWaitMs { get; set; }

        public int? DownloadWaitRandomMs { get; set; }

        public int? SimultaneousDownloads { get; set; }

        public int? MaximumStitchedImageSize { get; set; }

        public bool? GeoConvertWriteImagesWithMask { get; set; }

        public bool? GeoConvertWriteRawFiles { get; set; }

        public bool? GeoConvertDoMultipleSmallerRuns { get; set; }

        public string USGSUsername { get; set; }
        public string USGSPassword { get; set; }

        public int? MapControlLastZoomLevel { get; set;}
        public double? MapControlLastX { get; set; }
        public double? MapControlLastY { get; set; }
        public string MapControlLastMapType { get; set; }
        public bool? ShowAirports { get; set; }
        public double? ShrinkTMCGridSquareCoords { get; set; }
        public string AFS2UserDirectory { get; set; }

        // Image procesing
        public bool? EnableImageProcessing { get; set; }
        public int? BrightnessAdjustment { get; set; }
        public int? ContrastAdjustment { get; set; }
        public int? SaturationAdjustment { get; set; }
        public int? SharpnessAdjustment { get; set; }
        public int? RedAdjustment { get; set; }
        public int? GreenAdjustment { get; set; }
        public int? BlueAdjustment { get; set; }

        public ElevationSettings ElevationSettings { get; set; }
    }
}
