using AeroScenery.Common;
using AeroScenery.Controls;
using AeroScenery.OrthoPhotoSources;
using log4net;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.Data
{
    public static class RegistryExtensions
    {

        public static string GetValueAsString(this RegistryKey key, string name)
        {
            object result = key.GetValue(name);

            if (result != null)
            {
                return result.ToString();
            }

            return null;
        }

        public static bool GetValueAsBoolean(this RegistryKey key, string name, bool defaultValue)
        {
            bool result = defaultValue;
            bool.TryParse(key.GetValue(name).ToString(), out result);

            return result;
        }

        public static int GetValueAsInteger(this RegistryKey key, string name, int defaultValue)
        {
            int result = defaultValue;
            int.TryParse(key.GetValue(name).ToString(), out result);

            return result;
        }

        public static double GetValueAsDouble(this RegistryKey key, string name, double defaultValue)
        {
            double result = defaultValue;
            double.TryParse(key.GetValue(name).ToString(), out result);

            return result;
        }

        public static T GetValueAsEnum<T>(this RegistryKey key, string name, T defaultValue) where T : struct
        {
            T result = defaultValue;
            Enum.TryParse<T>(key.GetValue(name).ToString(), out result);

            return result;
        }
    }


    public class RegistryService
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        private int settingsVersion = 8;

        /// <summary>
        /// Gets whether this installation has legacy registry settings
        /// </summary>
        /// <returns></returns>
        public bool HasRegistrySettings()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", false);
            var aerosceneryKey = key.OpenSubKey("AeroScenery", false);

            if (aerosceneryKey != null)
            {
                return true;
            }

            return false;
        }

        public void DeleteRegistrySubKeyTree()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            var aerosceneryKey = key.OpenSubKey("AeroScenery", true);

            if (aerosceneryKey != null)
            {
                key.DeleteSubKeyTree("AeroScenery");
            }
        }

        public Settings GetSettingsLegacy()
        {
            log.Info("Reading Settings");

            Settings settings = new Settings();

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", false);
            key = key.OpenSubKey("AeroScenery", false);


            if (key != null)
            {
                var cultivationEditorKey = key.OpenSubKey("SceneryEditor", false);

                settings.DownloadImageTiles = key.GetValueAsBoolean("DownloadImageTiles", true);
                settings.StitchImageTiles = key.GetValueAsBoolean("StitchImageTiles", false);
                settings.GenerateAIDAndTMCFiles = key.GetValueAsBoolean("GenerateAIDAndTMCFiles", false);
                settings.RunGeoConvert = key.GetValueAsBoolean("RunGeoConvert", false);
                settings.DeleteStitchedImageTiles = key.GetValueAsBoolean("DeleteStitchedImageTiles", false);
                settings.InstallScenery = key.GetValueAsBoolean("InstallScenery", false);
                settings.ActionSet = key.GetValueAsEnum<ActionSet>("ActionSet", ActionSet.Default);

                settings.OrthophotoSource = key.GetValueAsEnum<OrthophotoSource>("OrthophotoSource", OrthophotoSource.Google);
                settings.ZoomLevel = key.GetValueAsInteger("ZoomLevel", 17);

                // Less than 12 used to be possible
                if (settings.ZoomLevel < 12)
                {
                    settings.ZoomLevel = 12;
                }

                settings.DownloadWaitMs = key.GetValueAsInteger("DownloadWaitMs", 10);
                settings.DownloadWaitRandomMs = key.GetValueAsInteger("DownloadWaitRandomMs", 3);
                settings.SimultaneousDownloads = key.GetValueAsInteger("SimultaneousDownloads", 4);
                settings.UserAgent = key.GetValueAsString("UserAgent");

                settings.AFS2SDKDirectory = key.GetValueAsString("AFS2SDKDirectory");
                settings.AFS2Directory = key.GetValueAsString("AFS2Directory");
                settings.AeroSceneryDBDirectory = key.GetValueAsString("AeroSceneryDBDirectory");
                settings.WorkingDirectory = key.GetValueAsString("WorkingDirectory");

                string afsLevelsCsv = key.GetValueAsString("AFSLevelsToGenerate");

                if (!string.IsNullOrEmpty(afsLevelsCsv))
                {
                    List<int> afsLevels = afsLevelsCsv.Split(',').Select(int.Parse).ToList();
                    settings.AFSLevelsToGenerate = afsLevels;
                }
                else
                {
                    settings.AFSLevelsToGenerate = new List<int>();
                }


                // Settings version 2           
                settings.MaximumStitchedImageSize = key.GetValueAsInteger("MaximumStitchedImageSize", 32);
                settings.GeoConvertWriteImagesWithMask = key.GetValueAsBoolean("GeoConvertWriteImagesWithMask", true);
                settings.GeoConvertWriteRawFiles = key.GetValueAsBoolean("GeoConvertWriteRawFiles", true);
                //--

                // Settings verison 3
                settings.GeoConvertDoMultipleSmallerRuns = key.GetValueAsBoolean("GeoConvertDoMultipleSmallerRuns", false);
                // --

                // Settings verison 4
                settings.USGSUsername = key.GetValueAsString("USGSUsername");
                settings.USGSPassword = key.GetValueAsString("USGSPassword");
                settings.ElevationSettings.DownloadElevationData = key.GetValueAsBoolean("Elevation.DownloadElevationData", true);
                settings.ElevationSettings.RunGeoConvert = key.GetValueAsBoolean("Elevation.RunGeoConvert", false);
                settings.ElevationSettings.GenerateAIDAndTMCFiles = key.GetValueAsBoolean("Elevation.GenerateAIDAndTMCFiles", false);
                //settings.ElevationSettings.ActionSet = (ActionSet)Enum.Parse(typeof(ActionSet), key.GetValueAsString("Elevation.ActionSet"));
                settings.ElevationSettings.ActionSet = key.GetValueAsEnum<ActionSet>("Elevation.ActionSet", ActionSet.Default);
                settings.ElevationSettings.InstallElevationData = key.GetValueAsBoolean("Elevation.InstallElevationData", false);

                string afsElevationLevelsCsv = key.GetValueAsString("Elevation.AFSLevelsToGenerate");
                if (string.IsNullOrEmpty(afsElevationLevelsCsv))
                {
                    settings.ElevationSettings.AFSLevelsToGenerate = new List<int>();
                }
                else
                {
                    List<int> afsElevationLevels = afsElevationLevelsCsv.Split(',').Select(int.Parse).ToList();
                    settings.ElevationSettings.AFSLevelsToGenerate = afsElevationLevels;
                }

                // --

                // Settings version 5
                var mapControlLastZoomLevelStr = key.GetValueAsString("MapControlLastZoomLevel");
                if (!String.IsNullOrEmpty(mapControlLastZoomLevelStr))
                {
                    int mapControlLastZoomLevel;

                    if (int.TryParse(mapControlLastZoomLevelStr, out mapControlLastZoomLevel))
                    {
                        settings.MapControlLastZoomLevel = mapControlLastZoomLevel;
                    }
                }

                var mapControlLastXStr = key.GetValueAsString("MapControlLastX");
                if (!String.IsNullOrEmpty(mapControlLastXStr))
                {
                    double mapControlLastX;

                    if (double.TryParse(mapControlLastXStr, out mapControlLastX))
                    {
                        settings.MapControlLastX = mapControlLastX;
                    }
                }

                var mapControlLastYStr = key.GetValueAsString("MapControlLastY");
                if (!String.IsNullOrEmpty(mapControlLastYStr))
                {
                    double mapControlLastY;

                    if (double.TryParse(mapControlLastYStr, out mapControlLastY))
                    {
                        settings.MapControlLastY = mapControlLastY;
                    }
                }

                settings.MapControlLastMapType = key.GetValueAsString("MapControlLastMapType");



                // Settings verison 7
                settings.ShrinkTMCGridSquareCoords = key.GetValueAsDouble("ShrinkTMCGridSquareCoords", 0.01);
                // --

                // Settings verison 8
                settings.AFS2UserDirectory = key.GetValueAsString("AFS2UserDirectory");
                // --
            }


            return settings;
        }


    }
}
