using AeroScenery.Common;
using AeroScenery.Controls;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.Data
{
    public class SettingsService
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        private RegistryService registryService;

        public Settings GetSettings()
        {
            return new Settings();
        }

        public void SaveSettings(Settings settings)
        {

        }

        public void LogSettings(Settings settings)
        {
            throw new NotImplementedException();
        }

        //public Settings CreateDefaultSettings()
        //{
        //    log.Info("Creating Default");

        //    Settings settings = new Settings();

        //    settings.ActionSet = ActionSet.Default;
        //    settings.DownloadImageTiles = true;
        //    settings.StitchImageTiles = false;
        //    settings.GenerateAIDAndTMCFiles = false;
        //    settings.RunGeoConvert = false;
        //    settings.DeleteStitchedImageTiles = false;
        //    settings.InstallScenery = false;

        //    settings.OrthophotoSource = OrthophotoSource.Google;
        //    settings.ZoomLevel = 17;

        //    settings.AFSLevelsToGenerate = new List<int>();
        //    settings.AFSLevelsToGenerate.Add(9);
        //    settings.AFSLevelsToGenerate.Add(11);
        //    settings.AFSLevelsToGenerate.Add(12);
        //    settings.AFSLevelsToGenerate.Add(13);
        //    settings.AFSLevelsToGenerate.Add(14);

        //    settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";
        //    settings.DownloadWaitMs = 10;
        //    settings.DownloadWaitRandomMs = 3;
        //    settings.SimultaneousDownloads = 4;

        //    settings.AFS2SDKDirectory = "";
        //    settings.AFS2Directory = "";

        //    // Create the Aerofly DB & working directory
        //    string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        //    string aeroSceneryDBDirectoryPath = myDocumentsPath + @"\AeroScenery\database\";
        //    string aeroSceneryWorkingDirectoryPath = myDocumentsPath + @"\AeroScenery\working\";

        //    if (!Directory.Exists(aeroSceneryDBDirectoryPath))
        //    {
        //        Directory.CreateDirectory(aeroSceneryDBDirectoryPath);
        //    }

        //    if (!Directory.Exists(aeroSceneryWorkingDirectoryPath))
        //    {
        //        Directory.CreateDirectory(aeroSceneryWorkingDirectoryPath);
        //    }

        //    settings.AeroSceneryDBDirectory = aeroSceneryDBDirectoryPath;
        //    settings.WorkingDirectory = aeroSceneryWorkingDirectoryPath;

        //    // Settings version 2           
        //    settings.MaximumStitchedImageSize = 32;
        //    settings.GeoConvertWriteImagesWithMask = true;
        //    settings.GeoConvertWriteRawFiles = true;
        //    //--

        //    // Settings version 3           
        //    settings.GeoConvertDoMultipleSmallerRuns = false;
        //    //--

        //    // Settings version 4
        //    settings.USGSUsername = "";
        //    settings.USGSPassword = "";
        //    settings.ElevationSettings.DownloadElevationData = true;
        //    settings.ElevationSettings.RunGeoConvert = false;
        //    settings.ElevationSettings.GenerateAIDAndTMCFiles = false;
        //    settings.ElevationSettings.ActionSet = ActionSet.Default;
        //    settings.ElevationSettings.InstallElevationData = false;

        //    settings.ElevationSettings.AFSLevelsToGenerate = new List<int>();
        //    settings.ElevationSettings.AFSLevelsToGenerate.Add(9);
        //    settings.ElevationSettings.AFSLevelsToGenerate.Add(11);
        //    settings.ElevationSettings.AFSLevelsToGenerate.Add(12);
        //    settings.ElevationSettings.AFSLevelsToGenerate.Add(13);
        //    settings.ElevationSettings.AFSLevelsToGenerate.Add(14);
        //    //--

        //    // Settings version 5
        //    settings.MapControlLastZoomLevel = 3;
        //    settings.MapControlLastX = null;
        //    settings.MapControlLastY = null;
        //    settings.MapControlLastMapType = "GoogleHybridMap";


        //    // Settings version 6
        //    settings.ShowAirports = false;
        //    // --

        //    // Settings version 7
        //    settings.ShrinkTMCGridSquareCoords = 0.01;
        //    // --

        //    // Settings version 8
        //    settings.AFS2UserDirectory = "";
        //    // --

        //    RegistryKey key = Registry.CurrentUser.OpenSubKey("Software", true);
        //    key.CreateSubKey("AeroScenery");
        //    var sceneryEditorKey = key.CreateSubKey("SceneryEditor");

        //    SaveSettings(settings);

        //    return settings;
        //}

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

            // If the SDK directory isn't blank but doesn't exist show a warning
            if (!string.IsNullOrEmpty(settings.AFS2SDKDirectory) && !Directory.Exists(settings.AFS2SDKDirectory))
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
