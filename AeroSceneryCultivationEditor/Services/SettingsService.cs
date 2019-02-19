using AeroSceneryCultivationEditor.Common;
using AeroSceneryCultivationEditor.Controls;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AeroSceneryCultivationEditor.Services
{
    public class SettingsService
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery Cultivatoin Editor");

        private string settingsFilePath;

        public SettingsService()
        {
        }

        public Settings GetSettings()
        {
            Settings settings = null;

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            this.settingsFilePath = String.Format("{0}{1}AeroScenery{2}cultivation_editor_settings.xml", myDocumentsPath,
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
                    log.Error("Error parsing cultivation_editor_settings.xml");
                    log.Error(ex.Message);
                    if (ex.InnerException != null)
                    {
                        log.Error(ex.InnerException.Message);

                    }

                    var messageBox = new CustomMessageBox("There was an error reading the AeroScenery Cultivation Editor cultivation_editor_settings.xml file.\n" +
                        "If this persists, you can delete the file and let AeroScenery Cultivation Editor recreate it.",
                        "AeroScenery Cultivation Editor ",
                        MessageBoxIcon.Error);

                    messageBox.ShowDialog();
                }

            }
            else
            {
                settings = new Settings();
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
                log.Error("Error saving cultivation_editor_settings.xml");
                log.Error(ex.Message);
                if (ex.InnerException != null)
                {
                    log.Error(ex.InnerException.Message);

                }

                var messageBox = new CustomMessageBox("There was an error reading the AeroScenery Cultivation Editor cultivation_editor_settings.xml file.\n" +
                    "If this persists, you can delete the file and let AeroScenery Cultivation Editor recreate it.",
                    "AeroScenery Cultivation Editor ",
                    MessageBoxIcon.Error);

                messageBox.ShowDialog();
            }
        }

        public void LogSettings(Settings settings)
        {
            log.Info(String.Format("MapControlLastZoomLevel: {0}", settings.MapControlLastZoomLevel));
            log.Info(String.Format("MapControlLastX: {0}", settings.MapControlLastX));
            log.Info(String.Format("MapControlLastY: {0}", settings.MapControlLastY));
        }

        private void SetDefaultSettingsWhereNull(Settings settings)
        {
            log.Info("Setting default settings");

            if (settings.MapControlLastZoomLevel == null)
                settings.MapControlLastZoomLevel = 3;

            if (settings.MapControlLastX == null)
                settings.MapControlLastX = 0;

            if (settings.MapControlLastY == null)
                settings.MapControlLastY = 0;

            if (settings.MapControlLastMapType == null)
                settings.MapControlLastMapType = "OpenStreetMap";
        }

        public void CheckConfiguredDirectories(Settings settings)
        {
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            //if (!Directory.Exists(settings.AeroSceneryDBDirectory))
            //{
            //    string aeroSceneryDBDirectoryPath = myDocumentsPath + @"\AeroScenery\database\";

            //    if (!Directory.Exists(aeroSceneryDBDirectoryPath))
            //    {
            //        Directory.CreateDirectory(aeroSceneryDBDirectoryPath);
            //    }

            //    settings.AeroSceneryDBDirectory = aeroSceneryDBDirectoryPath;

            //    var messageBox = new CustomMessageBox("The configured AeroScenery database directory does not exist. It will be reset to " + aeroSceneryDBDirectoryPath + ".",
            //        "AeroScenery",
            //        MessageBoxIcon.Warning);

            //    messageBox.ShowDialog();
            //}

            this.SaveSettings(settings);
        }
    }
}
