using AeroScenery.Common;
using AeroScenery.OrthoPhotoSources;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AeroScenery.Data
{
    public static class RegistryExtensions
    { 
        public static string GetValueAsString(this RegistryKey key, string name)
        {
            return key.GetValue(name).ToString();
        }
    }

    public class RegistryService
    {
        private int settingsVersion = 6;

        public void SaveSettings(Settings settings)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key = key.OpenSubKey("AeroScenery", true);

            var sceneryEditorKey = key.CreateSubKey("SceneryEditor");

            key.SetValue("DownloadImageTiles", settings.DownloadImageTiles);
            key.SetValue("StitchImageTiles", settings.StitchImageTiles);
            key.SetValue("GenerateAIDAndTMCFiles", settings.GenerateAIDAndTMCFiles);
            key.SetValue("RunGeoConvert", settings.RunGeoConvert);
            key.SetValue("DeleteStitchedImageTiles", settings.DeleteStitchedImageTiles);
            key.SetValue("InstallScenery", settings.InstallScenery);
            key.SetValue("ActionSet", settings.ActionSet);

            key.SetValue("OrthophotoSource", settings.OrthophotoSource);
            key.SetValue("ZoomLevel", settings.ZoomLevel);

            key.SetValue("DownloadWaitMs", settings.DownloadWaitMs);
            key.SetValue("DownloadWaitRandomMs", settings.DownloadWaitRandomMs);
            key.SetValue("SimultaneousDownloads", settings.SimultaneousDownloads);
            key.SetValue("UserAgent", settings.UserAgent);

            key.SetValue("AFS2SDKDirectory", settings.AFS2SDKDirectory);
            key.SetValue("AFS2Directory", settings.AFS2Directory);
            key.SetValue("AeroSceneryDBDirectory", settings.AeroSceneryDBDirectory);
            key.SetValue("WorkingDirectory", settings.WorkingDirectory);

            string afsLevelsCsv = String.Join(",", settings.AFSLevelsToGenerate.Select(x => x.ToString()).ToArray());
            key.SetValue("AFSLevelsToGenerate", afsLevelsCsv);

            // Settings version 2
            key.SetValue("MaximumStitchedImageSize", settings.MaximumStitchedImageSize);
            key.SetValue("GeoConvertWriteImagesWithMask", settings.GeoConvertWriteImagesWithMask);
            key.SetValue("GeoConvertWriteRawFiles", settings.GeoConvertWriteRawFiles);
            // --

            // Settings verison 3
            key.SetValue("GeoConvertDoMultipleSmallerRuns", settings.GeoConvertDoMultipleSmallerRuns);
            // --

            // Settings verison 4
            key.SetValue("USGSUsername", settings.USGSUsername);
            key.SetValue("USGSPassword", settings.USGSPassword);
            key.SetValue("Elevation.DownloadElevationData", settings.ElevationSettings.DownloadElevationData);
            key.SetValue("Elevation.RunGeoConvert", settings.ElevationSettings.RunGeoConvert);
            key.SetValue("Elevation.GenerateAIDAndTMCFiles", settings.ElevationSettings.GenerateAIDAndTMCFiles);
            key.SetValue("Elevation.ActionSet", settings.ElevationSettings.ActionSet);
            key.SetValue("Elevation.InstallElevationData", settings.ElevationSettings.InstallElevationData);

            string afsElevationLevelsCsv = String.Join(",", settings.ElevationSettings.AFSLevelsToGenerate.Select(x => x.ToString()).ToArray());
            key.SetValue("Elevation.AFSLevelsToGenerate", afsElevationLevelsCsv);
            // --

            // Settings version 5
            if (settings.MapControlLastZoomLevel.HasValue)
            {
                key.SetValue("MapControlLastZoomLevel", settings.MapControlLastZoomLevel);
            }
            else
            {
                key.SetValue("MapControlLastZoomLevel", "");
            }

            if (settings.MapControlLastX.HasValue)
            {
                key.SetValue("MapControlLastX", settings.MapControlLastX);

            }
            else
            {
                key.SetValue("MapControlLastX", "");

            }

            if (settings.MapControlLastY.HasValue)
            {
                key.SetValue("MapControlLastY", settings.MapControlLastY);

            }
            else
            {
                key.SetValue("MapControlLastY", "");
            }

            key.SetValue("MapControlLastMapType", settings.MapControlLastMapType);


            if (settings.SceneryEditorSettings.MapControlLastZoomLevel.HasValue)
            {
                sceneryEditorKey.SetValue("MapControlLastZoomLevel", settings.SceneryEditorSettings.MapControlLastZoomLevel);
            }
            else
            {
                sceneryEditorKey.SetValue("MapControlLastZoomLevel", "");
            }


            if (settings.SceneryEditorSettings.MapControlLastX.HasValue)
            {
                sceneryEditorKey.SetValue("MapControlLastX", settings.SceneryEditorSettings.MapControlLastX);
            }
            else
            {
                sceneryEditorKey.SetValue("MapControlLastX", "");
            }

            if (settings.SceneryEditorSettings.MapControlLastY.HasValue)
            {
                sceneryEditorKey.SetValue("MapControlLastY", settings.SceneryEditorSettings.MapControlLastY);
            }
            else
            {
                sceneryEditorKey.SetValue("MapControlLastY", "");
            }

            sceneryEditorKey.SetValue("MapControlLastMapType", settings.SceneryEditorSettings.MapControlLastMapType);
            // --


            // Settings version 6
            key.SetValue("ShowAirports", settings.ShowAirports);
            sceneryEditorKey.SetValue("ShowAirports", settings.SceneryEditorSettings.ShowAirports);

            // -- 

            key.SetValue("SettingsVersion", settingsVersion);
        }

        public Settings GetSettings()
        {
            Settings settings = new Settings();

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", false);
            key = key.OpenSubKey("AeroScenery", false);


            if (key != null)
            {
                var sceneryEditorKey = key.OpenSubKey("SceneryEditor", false);

                settings.DownloadImageTiles = Boolean.Parse(key.GetValueAsString("DownloadImageTiles"));
                settings.StitchImageTiles = Boolean.Parse(key.GetValueAsString("StitchImageTiles"));
                settings.GenerateAIDAndTMCFiles = Boolean.Parse(key.GetValueAsString("GenerateAIDAndTMCFiles"));
                settings.RunGeoConvert = Boolean.Parse(key.GetValueAsString("RunGeoConvert"));
                settings.DeleteStitchedImageTiles = Boolean.Parse(key.GetValueAsString("DeleteStitchedImageTiles"));
                settings.InstallScenery = Boolean.Parse(key.GetValueAsString("InstallScenery"));
                settings.ActionSet = (ActionSet)Enum.Parse(typeof(ActionSet), key.GetValueAsString("ActionSet"));

                settings.OrthophotoSource = (OrthophotoSource)Enum.Parse(typeof(OrthophotoSource), key.GetValueAsString("OrthophotoSource"));
                settings.ZoomLevel = int.Parse(key.GetValueAsString("ZoomLevel"));

                // Less than 12 used to be possible
                if (settings.ZoomLevel < 12)
                {
                    settings.ZoomLevel = 12;
                }

                settings.DownloadWaitMs = int.Parse(key.GetValueAsString("DownloadWaitMs"));
                settings.DownloadWaitRandomMs = int.Parse(key.GetValueAsString("DownloadWaitRandomMs"));
                settings.SimultaneousDownloads = int.Parse(key.GetValueAsString("SimultaneousDownloads"));
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
                settings.MaximumStitchedImageSize = int.Parse(key.GetValueAsString("MaximumStitchedImageSize"));
                settings.GeoConvertWriteImagesWithMask = Boolean.Parse(key.GetValueAsString("GeoConvertWriteImagesWithMask"));
                settings.GeoConvertWriteRawFiles = Boolean.Parse(key.GetValueAsString("GeoConvertWriteRawFiles"));
                //--

                // Settings verison 3
                settings.GeoConvertDoMultipleSmallerRuns = Boolean.Parse(key.GetValueAsString("GeoConvertDoMultipleSmallerRuns"));
                // --

                // Settings verison 4
                settings.USGSUsername = key.GetValueAsString("USGSUsername");
                settings.USGSPassword = key.GetValueAsString("USGSPassword");
                settings.ElevationSettings.DownloadElevationData = Boolean.Parse(key.GetValueAsString("Elevation.DownloadElevationData"));
                settings.ElevationSettings.RunGeoConvert = Boolean.Parse(key.GetValueAsString("Elevation.RunGeoConvert"));
                settings.ElevationSettings.GenerateAIDAndTMCFiles = Boolean.Parse(key.GetValueAsString("Elevation.GenerateAIDAndTMCFiles"));
                settings.ElevationSettings.ActionSet = (ActionSet)Enum.Parse(typeof(ActionSet), key.GetValueAsString("Elevation.ActionSet"));
                settings.ElevationSettings.InstallElevationData = Boolean.Parse(key.GetValueAsString("Elevation.InstallElevationData"));

                string afsElevationLevelsCsv = key.GetValueAsString("Elevation.AFSLevelsToGenerate");
                List<int> afsElevationLevels = afsElevationLevelsCsv.Split(',').Select(int.Parse).ToList();
                settings.ElevationSettings.AFSLevelsToGenerate = afsElevationLevels;
                // --

                // Settings version 5
                var mapControlLastZoomLevelStr = key.GetValueAsString("MapControlLastZoomLevel");
                if (!String.IsNullOrEmpty(mapControlLastZoomLevelStr))
                {
                    settings.MapControlLastZoomLevel = int.Parse(mapControlLastZoomLevelStr);
                }

                var mapControlLastXStr = key.GetValueAsString("MapControlLastX");
                if (!String.IsNullOrEmpty(mapControlLastXStr))
                {
                    settings.MapControlLastX = double.Parse(mapControlLastXStr);
                }

                var mapControlLastYStr = key.GetValueAsString("MapControlLastY");
                if (!String.IsNullOrEmpty(mapControlLastYStr))
                {
                    settings.MapControlLastY = double.Parse(mapControlLastYStr);
                }

                settings.MapControlLastMapType = key.GetValueAsString("MapControlLastMapType");

                var sceneryEditorMapControlLastZoomLevelStr = sceneryEditorKey.GetValueAsString("MapControlLastZoomLevel");
                if (!String.IsNullOrEmpty(sceneryEditorMapControlLastZoomLevelStr))
                {
                    settings.SceneryEditorSettings.MapControlLastZoomLevel = int.Parse(sceneryEditorMapControlLastZoomLevelStr);
                }

                var sceneryEditorMapControlLastXStr = sceneryEditorKey.GetValueAsString("MapControlLastX");
                if (!String.IsNullOrEmpty(sceneryEditorMapControlLastXStr))
                {
                    settings.SceneryEditorSettings.MapControlLastX = double.Parse(sceneryEditorMapControlLastXStr);
                }

                var sceneryEditorMapControlLastYStr = sceneryEditorKey.GetValueAsString("MapControlLastY");
                if (!String.IsNullOrEmpty(sceneryEditorMapControlLastYStr))
                {
                    settings.SceneryEditorSettings.MapControlLastY = double.Parse(sceneryEditorMapControlLastYStr);
                }

                settings.SceneryEditorSettings.MapControlLastMapType = sceneryEditorKey.GetValueAsString("MapControlLastMapType");
                // --

                // Settings verison 6
                settings.ShowAirports = bool.Parse(key.GetValueAsString("ShowAirports"));
                settings.SceneryEditorSettings.ShowAirports = bool.Parse(sceneryEditorKey.GetValueAsString("ShowAirports"));
                // --
            }


            return settings;
        }

        public bool SettingsInRegistry()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key = key.OpenSubKey("AeroScenery", true);

            if (key != null)
            {
                var sceneryEditorKey = key.CreateSubKey("SceneryEditor");

                var currentSettingsVersion = int.Parse(key.GetValueAsString("SettingsVersion"));

                // Do we need to upgrade the settings
                if (currentSettingsVersion < this.settingsVersion)
                {
                    // Upgrade to settings version 2
                    if (currentSettingsVersion == 1)
                    {
                        key.SetValue("MaximumStitchedImageSize", 32);
                        key.SetValue("GeoConvertWriteImagesWithMask", false);
                        key.SetValue("GeoConvertWriteRawFiles", true);
                        key.SetValue("SettingsVersion", 2);
                        currentSettingsVersion = 2;
                    }

                    // Upgrade to settings verison 3
                    if (currentSettingsVersion == 2)
                    {
                        key.SetValue("GeoConvertDoMultipleSmallerRuns", false);
                        key.SetValue("SettingsVersion", 3);
                        currentSettingsVersion = 3;
                    }

                    // Upgrade to settings verison 4
                    if (currentSettingsVersion == 3)
                    {
                        key.SetValue("USGSUsername", "");
                        key.SetValue("USGSPassword", "");
                        key.SetValue("Elevation.DownloadElevationData", true);
                        key.SetValue("Elevation.RunGeoConvert", false);
                        key.SetValue("Elevation.GenerateAIDAndTMCFiles", false);
                        key.SetValue("Elevation.ActionSet", ActionSet.Default);
                        key.SetValue("Elevation.InstallElevationData", false);
                        key.SetValue("Elevation.AFSLevelsToGenerate", "9,11,12,13,14");
                        key.SetValue("SettingsVersion", 4);
                        currentSettingsVersion = 4;
                    }

                    // Upgrade to settings version 5
                    if (currentSettingsVersion == 4)
                    {
                        key.SetValue("MapControlLastZoomLevel", "");
                        key.SetValue("MapControlLastX", "");
                        key.SetValue("MapControlLastY", "");
                        key.SetValue("MapControlLastMapType", "GoogleHybridMap");

                        sceneryEditorKey.SetValue("MapControlLastZoomLevel", "");
                        sceneryEditorKey.SetValue("MapControlLastX", "");
                        sceneryEditorKey.SetValue("MapControlLastY", "");
                        sceneryEditorKey.SetValue("MapControlLastMapType", "OpenStreetMap");

                        key.SetValue("SettingsVersion", 5);
                        currentSettingsVersion = 5;
                    }

                    // Upgrade to settings version 6
                    if (currentSettingsVersion == 5)
                    {
                        key.SetValue("ShowAirports", false);
                        sceneryEditorKey.SetValue("ShowAirports", false);
                    }
                }

                return true;
            }

            return false;
        }

        public Settings CreateDefaultSettings()
        {
            Settings settings = new Settings();

            settings.ActionSet = ActionSet.Default;
            settings.DownloadImageTiles = true;
            settings.StitchImageTiles = false;
            settings.GenerateAIDAndTMCFiles = false;
            settings.RunGeoConvert = false;
            settings.DeleteStitchedImageTiles = false;
            settings.InstallScenery = false;

            settings.OrthophotoSource = OrthophotoSource.Bing;
            settings.ZoomLevel = 17;

            settings.AFSLevelsToGenerate = new List<int>();
            settings.AFSLevelsToGenerate.Add(9);
            settings.AFSLevelsToGenerate.Add(11);
            settings.AFSLevelsToGenerate.Add(12);
            settings.AFSLevelsToGenerate.Add(13);
            settings.AFSLevelsToGenerate.Add(14);

            settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";
            settings.DownloadWaitMs = 10;
            settings.DownloadWaitRandomMs = 3;
            settings.SimultaneousDownloads = 4;

            settings.AFS2SDKDirectory = "";
            settings.AFS2Directory = "";

            // Create the Aerofly DB & working directory
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string aeroSceneryDBDirectoryPath = myDocumentsPath + @"\AeroScenery\database\";
            string aeroSceneryWorkingDirectoryPath = myDocumentsPath + @"\AeroScenery\working\";

            if (!Directory.Exists(aeroSceneryDBDirectoryPath))
            {
                Directory.CreateDirectory(aeroSceneryDBDirectoryPath);
            }

            if (!Directory.Exists(aeroSceneryWorkingDirectoryPath))
            {
                Directory.CreateDirectory(aeroSceneryWorkingDirectoryPath);
            }

            settings.AeroSceneryDBDirectory = aeroSceneryDBDirectoryPath;
            settings.WorkingDirectory = aeroSceneryWorkingDirectoryPath;

            // Settings version 2           
            settings.MaximumStitchedImageSize = 32;
            settings.GeoConvertWriteImagesWithMask = false;
            settings.GeoConvertWriteRawFiles = true;
            //--

            // Settings version 3           
            settings.GeoConvertDoMultipleSmallerRuns = false;
            //--

            // Settings version 4
            settings.USGSUsername = "";
            settings.USGSPassword = "";
            settings.ElevationSettings.DownloadElevationData = true;
            settings.ElevationSettings.RunGeoConvert = false;
            settings.ElevationSettings.GenerateAIDAndTMCFiles = false;
            settings.ElevationSettings.ActionSet = ActionSet.Default;
            settings.ElevationSettings.InstallElevationData = false;

            settings.ElevationSettings.AFSLevelsToGenerate = new List<int>();
            settings.ElevationSettings.AFSLevelsToGenerate.Add(9);
            settings.ElevationSettings.AFSLevelsToGenerate.Add(11);
            settings.ElevationSettings.AFSLevelsToGenerate.Add(12);
            settings.ElevationSettings.AFSLevelsToGenerate.Add(13);
            settings.ElevationSettings.AFSLevelsToGenerate.Add(14);
            //--

            // Settings version 5
            settings.MapControlLastZoomLevel = null;
            settings.MapControlLastX = null;
            settings.MapControlLastY = null;
            settings.MapControlLastMapType = "GoogleHybridMap";

            settings.SceneryEditorSettings.MapControlLastZoomLevel = null;
            settings.SceneryEditorSettings.MapControlLastX = null;
            settings.SceneryEditorSettings.MapControlLastY = null;
            settings.SceneryEditorSettings.MapControlLastMapType = "OpenStreetMap";
            // --

            // Settings version 6
            settings.ShowAirports = false;
            settings.SceneryEditorSettings.ShowAirports = false;
            // --

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key.CreateSubKey("AeroScenery");
            var sceneryEditorKey = key.CreateSubKey("SceneryEditor");

            SaveSettings(settings);

            return settings;
        }

    }
}
