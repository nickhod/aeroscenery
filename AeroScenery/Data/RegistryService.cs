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
        private int minSettingsVersion = 3;

        public void SaveSettings(Settings settings)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key = key.OpenSubKey("AeroScenery", true);

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

            key.SetValue("SettingsVersion", minSettingsVersion);
        }

        public Settings GetSettings()
        {
            Settings settings = new Settings();

            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", false);
            key = key.OpenSubKey("AeroScenery", false);

            if (key != null)
            {
                settings.DownloadImageTiles = Boolean.Parse(key.GetValueAsString("DownloadImageTiles"));
                settings.StitchImageTiles = Boolean.Parse(key.GetValueAsString("StitchImageTiles"));
                settings.GenerateAIDAndTMCFiles = Boolean.Parse(key.GetValueAsString("GenerateAIDAndTMCFiles"));
                settings.RunGeoConvert = Boolean.Parse(key.GetValueAsString("RunGeoConvert"));
                settings.DeleteStitchedImageTiles = Boolean.Parse(key.GetValueAsString("DeleteStitchedImageTiles"));
                settings.InstallScenery = Boolean.Parse(key.GetValueAsString("InstallScenery"));
                settings.ActionSet = (ActionSet)Enum.Parse(typeof(ActionSet), key.GetValueAsString("ActionSet"));

                settings.OrthophotoSource = (OrthophotoSource)Enum.Parse(typeof(OrthophotoSource), key.GetValueAsString("OrthophotoSource"));
                settings.ZoomLevel = int.Parse(key.GetValueAsString("ZoomLevel"));

                settings.DownloadWaitMs = int.Parse(key.GetValueAsString("DownloadWaitMs"));
                settings.DownloadWaitRandomMs = int.Parse(key.GetValueAsString("DownloadWaitRandomMs"));
                settings.SimultaneousDownloads = int.Parse(key.GetValueAsString("SimultaneousDownloads"));
                settings.UserAgent = key.GetValueAsString("UserAgent");

                settings.AFS2SDKDirectory = key.GetValueAsString("AFS2SDKDirectory");
                settings.AFS2Directory = key.GetValueAsString("AFS2Directory");
                settings.AeroSceneryDBDirectory = key.GetValueAsString("AeroSceneryDBDirectory");
                settings.WorkingDirectory = key.GetValueAsString("WorkingDirectory");

                string afsLevelsCsv = key.GetValueAsString("AFSLevelsToGenerate");
                List<int> afsLevels = afsLevelsCsv.Split(',').Select(int.Parse).ToList();
                settings.AFSLevelsToGenerate = afsLevels;

                // Settings version 2           
                settings.MaximumStitchedImageSize = int.Parse(key.GetValueAsString("MaximumStitchedImageSize"));
                settings.GeoConvertWriteImagesWithMask = Boolean.Parse(key.GetValueAsString("GeoConvertWriteImagesWithMask"));
                settings.GeoConvertWriteRawFiles = Boolean.Parse(key.GetValueAsString("GeoConvertWriteRawFiles"));
                //--

                // Settings verison 3
                settings.GeoConvertDoMultipleSmallerRuns = Boolean.Parse(key.GetValueAsString("GeoConvertDoMultipleSmallerRuns"));
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
                var currentSettingsVersion = int.Parse(key.GetValueAsString("SettingsVersion"));

                // Do we need to upgrade the settings
                if (currentSettingsVersion < this.minSettingsVersion)
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
            settings.AFSLevelsToGenerate.Add(11);
            settings.AFSLevelsToGenerate.Add(12);

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


            RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
            key.CreateSubKey("AeroScenery");

            SaveSettings(settings);

            return settings;
        }

    }
}
