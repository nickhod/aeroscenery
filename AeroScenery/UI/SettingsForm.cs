using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.UI
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            var settings = AeroSceneryManager.Instance.Settings;
            
            settings.WorkingDirectory = this.workingFolderTextBox.Text;
            settings.AeroSceneryDBDirectory = this.aeroSceneryDatabaseFolderTextBox.Text;
            settings.AFS2SDKDirectory = this.afsSDKFolderTextBox.Text;
            settings.AFS2Directory = this.afsFolderTextBox.Text;

            settings.UserAgent = this.userAgentTextBox.Text;
            settings.DownloadWaitMs = int.Parse(this.downloadWaitTextBox.Text);
            settings.DownloadWaitRandomMs = int.Parse(this.downloadWaitRandomTextBox.Text);

            switch (this.simultaneousDownloadsComboBox.SelectedIndex)
            {
                case 0:
                    settings.SimultaneousDownloads = 4;
                    break;
                case 1:
                    settings.SimultaneousDownloads = 6;
                    break;
                case 2:
                    settings.SimultaneousDownloads = 8;
                    break;
            }

            AeroSceneryManager.Instance.SaveSettings();
            this.Hide();
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            var settings = AeroSceneryManager.Instance.Settings;

            this.workingFolderTextBox.Text = settings.WorkingDirectory;
            this.aeroSceneryDatabaseFolderTextBox.Text = settings.AeroSceneryDBDirectory;
            this.afsSDKFolderTextBox.Text = settings.AFS2SDKDirectory;
            this.afsFolderTextBox.Text = settings.AFS2Directory;

            this.userAgentTextBox.Text = settings.UserAgent;
            this.downloadWaitTextBox.Text = settings.DownloadWaitMs.ToString();
            this.downloadWaitRandomTextBox.Text = settings.DownloadWaitRandomMs.ToString();

            switch (settings.SimultaneousDownloads)
            {
                case 4:
                    this.simultaneousDownloadsComboBox.SelectedIndex = 0;
                    break;
                case 6:
                    this.simultaneousDownloadsComboBox.SelectedIndex = 1;
                    break;
                case 8:
                    this.simultaneousDownloadsComboBox.SelectedIndex = 2;
                    break;
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void workingFolderButton_Click(object sender, EventArgs e)
        {
            var settings = AeroSceneryManager.Instance.Settings;
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.workingFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
                settings.WorkingDirectory = this.workingFolderTextBox.Text;
            }
        }

        private void aerosceneryDatabaseFolderButton_Click(object sender, EventArgs e)
        {

        }

        private void sdkButton_Click(object sender, EventArgs e)
        {

        }

        private void afsFolderButton_Click(object sender, EventArgs e)
        {

        }


    }
}
