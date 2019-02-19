using AeroScenery.CultivationEditor.UI;
using AeroSceneryCultivationEditor.Common;
using AeroSceneryCultivationEditor.Services;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroSceneryCultivationEditor
{
    public class AeroSceneryCultivationEditorManager
    {
        private CultivationEditorForm cultivationEditorForm;

        private static AeroSceneryCultivationEditorManager aeroSceneryCultivationEditorManager;


        private Common.Settings settings;

        private SettingsService settingsService;

        private readonly ILog log = LogManager.GetLogger("AeroSceneryCultivationEditor");
        private string version;
        private int incrementalVersion;


        public AeroSceneryCultivationEditorManager()
        {

            settingsService = new SettingsService();

            version = "1.0";
            incrementalVersion = 1;
        }

        public Settings Settings
        {
            get
            {
                return this.settings;
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
        }

        public int IncrementalVersion
        {
            get
            {
                return this.incrementalVersion;
            }
        }

        public static AeroSceneryCultivationEditorManager Instance
        {
            get
            {
                if (AeroSceneryCultivationEditorManager.aeroSceneryCultivationEditorManager == null)
                {
                    aeroSceneryCultivationEditorManager = new AeroSceneryCultivationEditorManager();
                }

                return aeroSceneryCultivationEditorManager;
            }
        }

        public void Initialize()
        {
            // Create settings if required and read them
            this.settings = settingsService.GetSettings();
            settingsService.LogSettings(this.settings);
            settingsService.CheckConfiguredDirectories(this.settings);

            //this.dataRepository.Settings = settings;
            //this.dataRepository.UpgradeDatabase();

            this.cultivationEditorForm = new CultivationEditorForm();

            this.cultivationEditorForm.Initialize();
            Application.Run(this.cultivationEditorForm);

        }
    }
}
