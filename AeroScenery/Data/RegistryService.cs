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

        public void SaveSettings(Settings settings)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key = key.OpenSubKey("AeroScenery", true);

            var cultivationEditorKey = key.CreateSubKey("SceneryEditor");

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


            if (settings.CultivationEditorSettings.MapControlLastZoomLevel.HasValue)
            {
                cultivationEditorKey.SetValue("MapControlLastZoomLevel", settings.CultivationEditorSettings.MapControlLastZoomLevel);
            }
            else
            {
                cultivationEditorKey.SetValue("MapControlLastZoomLevel", "");
            }


            if (settings.CultivationEditorSettings.MapControlLastX.HasValue)
            {
                cultivationEditorKey.SetValue("MapControlLastX", settings.CultivationEditorSettings.MapControlLastX);
            }
            else
            {
                cultivationEditorKey.SetValue("MapControlLastX", "");
            }

            if (settings.CultivationEditorSettings.MapControlLastY.HasValue)
            {
                cultivationEditorKey.SetValue("MapControlLastY", settings.CultivationEditorSettings.MapControlLastY);
            }
            else
            {
                cultivationEditorKey.SetValue("MapControlLastY", "");
            }

            cultivationEditorKey.SetValue("MapControlLastMapType", settings.CultivationEditorSettings.MapControlLastMapType);
            // --


            // Settings version 6
            key.SetValue("ShowAirports", settings.ShowAirports);
            cultivationEditorKey.SetValue("ShowAirports", settings.CultivationEditorSettings.ShowAirports);

            // -- 

            // Settings version 7
            key.SetValue("ShrinkTMCGridSquareCoords", settings.ShrinkTMCGridSquareCoords);
            // -- 

            // Settings version 8
            key.SetValue("AFS2UserDirectory", settings.AFS2UserDirectory);
            // -- 

            key.SetValue("SettingsVersion", settingsVersion);
        }

        internal void LogSettings(Settings settings)
        {
            string afsLevelsCsv = "";

            if (settings.AFSLevelsToGenerate.Count > 0)
            {
                afsLevelsCsv = String.Join(",", settings.AFSLevelsToGenerate.Select(x => x.ToString()).ToArray());
            }

            log.Info(String.Format("AFS2SDKDirectory: {0}", settings.AFS2SDKDirectory));
            log.Info(String.Format("AFS2Directory: {0}", settings.AFS2Directory));
            log.Info(String.Format("AFS2UserDirectory: {0}", settings.AFS2UserDirectory));
            log.Info(String.Format("WorkingDirectory: {0}", settings.WorkingDirectory));
            log.Info(String.Format("AeroSceneryDBDirectory: {0}", settings.AeroSceneryDBDirectory));
            log.Info(String.Format("OrthophotoSource: {0}", settings.OrthophotoSource));
            log.Info(String.Format("ZoomLevel: {0}", settings.ZoomLevel));
            log.Info(String.Format("DownloadImageTiles: {0}", settings.DownloadImageTiles));
            log.Info(String.Format("StitchImageTiles: {0}", settings.StitchImageTiles));
            log.Info(String.Format("GenerateAIDAndTMCFiles: {0}", settings.GenerateAIDAndTMCFiles));
            log.Info(String.Format("RunGeoConvert: {0}", settings.RunGeoConvert));
            log.Info(String.Format("DeleteStitchedImageTiles: {0}", settings.DeleteStitchedImageTiles));
            log.Info(String.Format("InstallScenery: {0}", settings.InstallScenery));
            log.Info(String.Format("ActionSet: {0}", settings.ActionSet));
            log.Info(String.Format("AFSLevelsToGenerate: {0}", afsLevelsCsv));
            log.Info(String.Format("UserAgent: {0}", settings.UserAgent));
            log.Info(String.Format("DownloadWaitMs: {0}", settings.DownloadWaitMs));
            log.Info(String.Format("DownloadWaitRandomMs: {0}", settings.DownloadWaitRandomMs));
            log.Info(String.Format("SimultaneousDownloads: {0}", settings.SimultaneousDownloads));
            log.Info(String.Format("MaximumStitchedImageSize: {0}", settings.MaximumStitchedImageSize));
            log.Info(String.Format("GeoConvertWriteImagesWithMask: {0}", settings.GeoConvertWriteImagesWithMask));
            log.Info(String.Format("GeoConvertWriteRawFiles: {0}", settings.GeoConvertWriteRawFiles));
            log.Info(String.Format("GeoConvertDoMultipleSmallerRuns: {0}", settings.GeoConvertDoMultipleSmallerRuns));
            log.Info(String.Format("USGSUsername: {0}", settings.USGSUsername));
            //log.Info(String.Format("USGSPassword: {0}", settings.USGSPassword);
            log.Info(String.Format("MapControlLastMapType: {0}", settings.MapControlLastMapType));
            log.Info(String.Format("ShowAirports: {0}", settings.ShowAirports));

            if (settings.MapControlLastZoomLevel.HasValue)
            {
                log.Info(String.Format("MapControlLastZoomLevel: {0}", settings.MapControlLastZoomLevel));
            }
            else
            {
                log.Info("MapControlLastZoomLevel: null");
            }

            if (settings.MapControlLastX.HasValue)
            {
                log.Info(String.Format("MapControlLastX: {0}", settings.MapControlLastX));
            }
            else
            {
                log.Info("MapControlLastX: null");
            }

            if (settings.MapControlLastX.HasValue)
            {
                log.Info(String.Format("MapControlLastY: {0}", settings.MapControlLastY));
            }
            else
            {
                log.Info("MapControlLastY: null");
            }

            log.Info(String.Format("ShrinkTMCGridSquareCoords: {0}", settings.ShrinkTMCGridSquareCoords));


        }

        public Settings GetSettings()
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

                var sceneryEditorMapControlLastZoomLevelStr = cultivationEditorKey.GetValueAsString("MapControlLastZoomLevel");
                if (!String.IsNullOrEmpty(sceneryEditorMapControlLastZoomLevelStr))
                {
                    int sceneryEditorMapControlLastZoomLevel;

                    if (int.TryParse(sceneryEditorMapControlLastZoomLevelStr, out sceneryEditorMapControlLastZoomLevel))
                    {
                        settings.CultivationEditorSettings.MapControlLastZoomLevel = sceneryEditorMapControlLastZoomLevel;
                    }
                }

                var sceneryEditorMapControlLastXStr = cultivationEditorKey.GetValueAsString("MapControlLastX");
                if (!String.IsNullOrEmpty(sceneryEditorMapControlLastXStr))
                {
                    double sceneryEditorMapControlLastX;

                    if (double.TryParse(sceneryEditorMapControlLastXStr, out sceneryEditorMapControlLastX))
                    {
                        settings.CultivationEditorSettings.MapControlLastX = sceneryEditorMapControlLastX;
                    }
                }

                var sceneryEditorMapControlLastYStr = cultivationEditorKey.GetValueAsString("MapControlLastY");
                if (!String.IsNullOrEmpty(sceneryEditorMapControlLastYStr))
                {
                    double sceneryEditorMapControlLastY;

                    if (double.TryParse(sceneryEditorMapControlLastYStr, out sceneryEditorMapControlLastY))
                    {
                        settings.CultivationEditorSettings.MapControlLastY = sceneryEditorMapControlLastY;
                    }
                }

                settings.CultivationEditorSettings.MapControlLastMapType = cultivationEditorKey.GetValueAsString("MapControlLastMapType");
                // --

                // Settings verison 6
                settings.ShowAirports = key.GetValueAsBoolean("ShowAirports", false);
                settings.CultivationEditorSettings.ShowAirports = cultivationEditorKey.GetValueAsBoolean("ShowAirports", false);
                // --

                // Settings verison 7
                settings.ShrinkTMCGridSquareCoords = key.GetValueAsDouble("ShrinkTMCGridSquareCoords", 0.01);
                // --

                // Settings verison 8
                settings.AFS2UserDirectory = key.GetValueAsString("AFS2UserDirectory");
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
                var cultivationEditorKey = key.CreateSubKey("SceneryEditor");
                var currentSettingsVersionStr = key.GetValueAsString("SettingsVersion");

                if (!string.IsNullOrEmpty(currentSettingsVersionStr))
                {
                    var currentSettingsVersion = int.Parse(currentSettingsVersionStr);

                    // Do we need to upgrade the settings
                    if (currentSettingsVersion < this.settingsVersion)
                    {
                        // Upgrade to settings version 2
                        if (currentSettingsVersion == 1)
                        {
                            key.SetValue("MaximumStitchedImageSize", 32);
                            key.SetValue("GeoConvertWriteImagesWithMask", true);
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

                            cultivationEditorKey.SetValue("MapControlLastZoomLevel", 3);
                            cultivationEditorKey.SetValue("MapControlLastX", "");
                            cultivationEditorKey.SetValue("MapControlLastY", "");
                            cultivationEditorKey.SetValue("MapControlLastMapType", "OpenStreetMap");

                            key.SetValue("SettingsVersion", 5);
                            currentSettingsVersion = 5;
                        }

                        // Upgrade to settings version 6
                        if (currentSettingsVersion == 5)
                        {
                            key.SetValue("ShowAirports", false);
                            cultivationEditorKey.SetValue("ShowAirports", false);
                            key.SetValue("SettingsVersion", 6);
                            currentSettingsVersion = 6;
                        }

                        // Upgrade to settings version 7
                        if (currentSettingsVersion == 6)
                        {
                            key.SetValue("ShrinkTMCGridSquareCoords", 0.01);
                            key.SetValue("SettingsVersion", 7);
                            currentSettingsVersion = 7;
                        }

                        // Upgrade to settings version 8
                        if (currentSettingsVersion == 7)
                        {
                            key.SetValue("AFS2UserDirectory", "");
                            key.SetValue("SettingsVersion", 8);
                            currentSettingsVersion = 8;
                        }
                    }

                    return true;
                }

            }

            return false;
        }

        public Settings CreateDefaultSettings()
        {
            log.Info("Creating Default");

            Settings settings = new Settings();

            settings.ActionSet = ActionSet.Default;
            settings.DownloadImageTiles = true;
            settings.StitchImageTiles = false;
            settings.GenerateAIDAndTMCFiles = false;
            settings.RunGeoConvert = false;
            settings.DeleteStitchedImageTiles = false;
            settings.InstallScenery = false;

            settings.OrthophotoSource = OrthophotoSource.Google;
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
            settings.GeoConvertWriteImagesWithMask = true;
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
            settings.MapControlLastZoomLevel = 3;
            settings.MapControlLastX = null;
            settings.MapControlLastY = null;
            settings.MapControlLastMapType = "GoogleHybridMap";

            settings.CultivationEditorSettings.MapControlLastZoomLevel = null;
            settings.CultivationEditorSettings.MapControlLastX = null;
            settings.CultivationEditorSettings.MapControlLastY = null;
            settings.CultivationEditorSettings.MapControlLastMapType = "OpenStreetMap";
            // --

            // Settings version 6
            settings.ShowAirports = false;
            settings.CultivationEditorSettings.ShowAirports = false;
            // --

            // Settings version 7
            settings.ShrinkTMCGridSquareCoords = 0.01;
            // --

            // Settings version 8
            settings.AFS2UserDirectory = "";
            // --

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key.CreateSubKey("AeroScenery");
            var sceneryEditorKey = key.CreateSubKey("SceneryEditor");

            SaveSettings(settings);

            return settings;
        }

        public void CheckConfiguredDirectories(Settings settings)
        {
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (!Directory.Exists(settings.AeroSceneryDBDirectory))
            {
                string aeroSceneryDBDirectoryPath = myDocumentsPath + @"\AeroScenery\database\";

                if (!Directory.Exists(aeroSceneryDBDirectoryPath))
                {
                    Directory.CreateDirectory(aeroSceneryDBDirectoryPath);
                }

                settings.AeroSceneryDBDirectory = aeroSceneryDBDirectoryPath;

                var messageBox = new CustomMessageBox("The configured AeroScenery database directory does not exist. It will be reset to " + aeroSceneryDBDirectoryPath + ".",
                    "AeroScenery",
                    MessageBoxIcon.Warning);

                messageBox.ShowDialog();
            }

            if (!Directory.Exists(settings.WorkingDirectory))
            {
                string aeroSceneryWorkingDirectoryPath = myDocumentsPath + @"\AeroScenery\working\";

                if (!Directory.Exists(aeroSceneryWorkingDirectoryPath))
                {
                    Directory.CreateDirectory(aeroSceneryWorkingDirectoryPath);
                }

                settings.WorkingDirectory = aeroSceneryWorkingDirectoryPath;


                var messageBox = new CustomMessageBox("The configured AeroScenery working directory does not exist. It will be reset to " + aeroSceneryWorkingDirectoryPath + ".",
                    "AeroScenery",
                    MessageBoxIcon.Warning);

                messageBox.ShowDialog();
            }

            if (!Directory.Exists(settings.AFS2SDKDirectory))
            {
                settings.AFS2SDKDirectory = "";

                var messageBox = new CustomMessageBox("The configured Aerofly FS2 SDK directory does not exist. It will be reset as blank.",
                    "AeroScenery",
                    MessageBoxIcon.Warning);

                messageBox.ShowDialog();
            }

            this.SaveSettings(settings);
        }       


    }
}
