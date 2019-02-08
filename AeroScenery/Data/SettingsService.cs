using AeroScenery.Common;
using AeroScenery.Controls;
using AeroScenery.OrthoPhotoSources;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AeroScenery.Data
{
    public class SettingsService
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        private RegistryService registryService;

        private string settingsFilePath;

        public SettingsService()
        {
            this.registryService = new RegistryService();
        }

        public Settings GetSettings()
        {
            Settings settings = null;

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            this.settingsFilePath = String.Format("{0}{1}AeroScenery{2}settings.xml", myDocumentsPath, 
                Path.DirectorySeparatorChar, Path.DirectorySeparatorChar);

            if (File.Exists(this.settingsFilePath))
            {
                // We have a settings.xml file so let's try to read it
                try
                {
                    using (var streamReader = new StreamReader(this.settingsFilePath))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                        settings = (Settings)serializer.Deserialize(streamReader);
                    }

                }
                catch (Exception ex)
                {
                    log.Error("Error parsing settings.xml");
                    log.Error(ex.Message);
                    if (ex.InnerException != null)
                    {
                        log.Error(ex.InnerException.Message);

                    }

                    var messageBox = new CustomMessageBox("There was an error reading the AeroScenery settings.xml file.\n" +
                        "If this persists, you can delete the file and let AeroScenery recreate it.",
                        "AeroScenery",
                        MessageBoxIcon.Error);

                    messageBox.ShowDialog();
                }

            }
            else
            {
                // There was no settings.xml. 
                // Do we need to migrate old registry based settings?
                if (registryService.HasRegistrySettings())
                {
                    settings = registryService.GetSettingsLegacy();

                    // Save before we delete the registry key
                    this.SetDefaultSettingsWhereNull(settings);
                    this.SaveSettings(settings);

                    registryService.DeleteRegistrySubKeyTree();
                }
                else
                {
                    settings = new Settings();
                }


            }

            // Any settings that are null will be set to their default value
            this.SetDefaultSettingsWhereNull(settings);
            this.SaveSettings(settings);

            return settings;
        }

        public void SaveSettings(Settings settings)
        {
            try
            {
                using (var streamWriter = new StreamWriter(this.settingsFilePath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Settings));
                    serializer.Serialize(streamWriter, settings);
                }

            }
            catch (Exception ex)
            {
                log.Error("Error saving settings.xml");
                log.Error(ex.Message);
                if (ex.InnerException != null)
                {
                    log.Error(ex.InnerException.Message);

                }

                var messageBox = new CustomMessageBox("There was an error reading the AeroScenery settings.xml file.\n" +
                    "If this persists, you can delete the file and let AeroScenery recreate it.",
                    "AeroScenery",
                    MessageBoxIcon.Error);

                messageBox.ShowDialog();
            }
        }

        public void LogSettings(Settings settings)
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

            log.Info(String.Format("MapControlLastZoomLevel: {0}", settings.MapControlLastZoomLevel));
            log.Info(String.Format("MapControlLastX: {0}", settings.MapControlLastX));
            log.Info(String.Format("MapControlLastY: {0}", settings.MapControlLastY));

            log.Info(String.Format("ShrinkTMCGridSquareCoords: {0}", settings.ShrinkTMCGridSquareCoords));

            log.Info(String.Format("EnableImageProcessing: {0}", settings.EnableImageProcessing));
            log.Info(String.Format("BrightnessAdjustment: {0}", settings.BrightnessAdjustment));
            log.Info(String.Format("ContrastAdjustment: {0}", settings.ContrastAdjustment));
            log.Info(String.Format("SaturationAdjustment: {0}", settings.SaturationAdjustment));
            log.Info(String.Format("SharpnessAdjustment: {0}", settings.SharpnessAdjustment));
            log.Info(String.Format("RedAdjustment: {0}", settings.RedAdjustment));
            log.Info(String.Format("GreenAdjustment: {0}", settings.GreenAdjustment));
            log.Info(String.Format("BlueAdjustment: {0}", settings.BlueAdjustment));

        }

        private void SetDefaultSettingsWhereNull(Settings settings)
        {
            log.Info("Setting default settings");

            if (settings.ActionSet == null)
                settings.ActionSet = ActionSet.Default;

            if (settings.DownloadImageTiles == null)
                settings.DownloadImageTiles = true;


            if (settings.StitchImageTiles == null)
                settings.StitchImageTiles = false;

            if (settings.GenerateAIDAndTMCFiles == null)
                settings.GenerateAIDAndTMCFiles = false;

            if (settings.RunGeoConvert == null)
                settings.RunGeoConvert = false;

            if (settings.DeleteStitchedImageTiles == null)
                settings.DeleteStitchedImageTiles = false;

            if (settings.InstallScenery == null)
                settings.InstallScenery = false;

            if (settings.OrthophotoSource == null)
                settings.OrthophotoSource = OrthophotoSource.Google;

            if (settings.ZoomLevel == null)
                settings.ZoomLevel = 17;

            if (settings.AFSLevelsToGenerate == null)
            {
                settings.AFSLevelsToGenerate = new List<int>();
                settings.AFSLevelsToGenerate.Add(9);
                settings.AFSLevelsToGenerate.Add(11);
                settings.AFSLevelsToGenerate.Add(12);
                settings.AFSLevelsToGenerate.Add(13);
                settings.AFSLevelsToGenerate.Add(14);
            }

            if (settings.UserAgent == null)
                settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36";

            if (settings.DownloadWaitMs == null)
                settings.DownloadWaitMs = 10;

            if (settings.DownloadWaitRandomMs == null)
                settings.DownloadWaitRandomMs = 3;

            if (settings.SimultaneousDownloads == null)
                settings.SimultaneousDownloads = 4;

            if (settings.AFS2SDKDirectory == null)
                settings.AFS2SDKDirectory = "";

            if (settings.AFS2Directory == null)
                settings.AFS2Directory = "";

            if (settings.AeroSceneryDBDirectory == null)
            {
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string aeroSceneryDBDirectoryPath = myDocumentsPath + @"\AeroScenery\database\";


                if (!Directory.Exists(aeroSceneryDBDirectoryPath))
                {
                    Directory.CreateDirectory(aeroSceneryDBDirectoryPath);
                }

                settings.AeroSceneryDBDirectory = aeroSceneryDBDirectoryPath;

            }

            if (settings.WorkingDirectory == null)
            {
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                // Create the Aerofly DB & working directory
                string aeroSceneryWorkingDirectoryPath = myDocumentsPath + @"\AeroScenery\working\";

                if (!Directory.Exists(aeroSceneryWorkingDirectoryPath))
                {
                    Directory.CreateDirectory(aeroSceneryWorkingDirectoryPath);
                }
                settings.WorkingDirectory = aeroSceneryWorkingDirectoryPath;
            }

            if (settings.MaximumStitchedImageSize == null)
                settings.MaximumStitchedImageSize = 32;

            if (settings.GeoConvertWriteImagesWithMask == null)
                settings.GeoConvertWriteImagesWithMask = true;

            if (settings.GeoConvertWriteRawFiles == null)
                settings.GeoConvertWriteRawFiles = true;

            if (settings.GeoConvertDoMultipleSmallerRuns == null)
                settings.GeoConvertDoMultipleSmallerRuns = false;

            if (settings.GeoConvertUseWrapper == null)
                settings.GeoConvertUseWrapper = false;

            if (settings.USGSUsername == null)
                settings.USGSUsername = "";

            if (settings.USGSPassword == null)
                settings.USGSPassword = "";

            if (settings.ElevationSettings.DownloadElevationData == null)
                settings.ElevationSettings.DownloadElevationData = true;

            if (settings.ElevationSettings.RunGeoConvert == null)
                settings.ElevationSettings.RunGeoConvert = false;

            if (settings.ElevationSettings.GenerateAIDAndTMCFiles == null)
                settings.ElevationSettings.GenerateAIDAndTMCFiles = false;

            if (settings.ElevationSettings.ActionSet == null)
                settings.ElevationSettings.ActionSet = ActionSet.Default;

            if (settings.ElevationSettings.InstallElevationData == null)
                settings.ElevationSettings.InstallElevationData = false;

            if (settings.ElevationSettings.AFSLevelsToGenerate == null)
            {
                settings.ElevationSettings.AFSLevelsToGenerate = new List<int>();
                settings.ElevationSettings.AFSLevelsToGenerate.Add(9);
                settings.ElevationSettings.AFSLevelsToGenerate.Add(11);
                settings.ElevationSettings.AFSLevelsToGenerate.Add(12);
                settings.ElevationSettings.AFSLevelsToGenerate.Add(13);
                settings.ElevationSettings.AFSLevelsToGenerate.Add(14);
            }

            if (settings.MapControlLastZoomLevel == null)
                settings.MapControlLastZoomLevel = 3;

            if (settings.MapControlLastX == null)
                settings.MapControlLastX = 0;

            if (settings.MapControlLastY == null)
                settings.MapControlLastY = 0;

            if (settings.MapControlLastMapType == null)
                settings.MapControlLastMapType = "GoogleHybridMap";


            if (settings.ShowAirports == null)
                settings.ShowAirports = false;

            if (settings.ShrinkTMCGridSquareCoords == null)
                settings.ShrinkTMCGridSquareCoords = 0.01;

            if (settings.AFS2UserDirectory == null)
                settings.AFS2UserDirectory = "";

            if (settings.EnableImageProcessing == null)
                settings.EnableImageProcessing = false;

            if (settings.BrightnessAdjustment == null)
                settings.BrightnessAdjustment = 0;

            if (settings.ContrastAdjustment == null)
                settings.ContrastAdjustment = 0;

            if (settings.SaturationAdjustment == null)
                settings.SaturationAdjustment = 0;

            if (settings.SharpnessAdjustment == null)
                settings.SharpnessAdjustment = 0;

            if (settings.RedAdjustment == null)
                settings.RedAdjustment = 0;

            if (settings.GreenAdjustment == null)
                settings.GreenAdjustment = 0;

            if (settings.BlueAdjustment == null)
                settings.BlueAdjustment = 0;

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
