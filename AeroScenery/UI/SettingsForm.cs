using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;

namespace AeroScenery.UI
{
    public partial class SettingsForm : Form
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

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

            settings.WorkingDirectory = pathWithTrailingDirectorySeparatorChar(this.workingFolderTextBox.Text);
            settings.AeroSceneryDBDirectory = pathWithTrailingDirectorySeparatorChar(this.aeroSceneryDatabaseFolderTextBox.Text);
            settings.AFS2SDKDirectory = pathWithTrailingDirectorySeparatorChar(this.afsSDKFolderTextBox.Text);
            settings.AFS2UserDirectory = pathWithTrailingDirectorySeparatorChar(this.afsUserFolderTextBox.Text);

            if (settings.AFS2SDKDirectory.Contains("aerofly_fs_2_geoconvert"))
            {
                settings.AFS2SDKDirectory = settings.AFS2SDKDirectory.Replace("aerofly_fs_2_geoconvert.exe", "");
                settings.AFS2SDKDirectory = settings.AFS2SDKDirectory.Replace("aerofly_fs_2_geoconvert", "");
                settings.AFS2SDKDirectory = settings.AFS2SDKDirectory.Replace("\\\\", "");
                settings.AFS2SDKDirectory = pathWithTrailingDirectorySeparatorChar(settings.AFS2SDKDirectory);
            }


            settings.UserAgent = this.userAgentTextBox.Text;

            if (!String.IsNullOrEmpty(this.downloadWaitTextBox.Text))
            {
                settings.DownloadWaitMs = int.Parse(this.downloadWaitTextBox.Text);
            }

            if (!String.IsNullOrEmpty(this.downloadWaitRandomTextBox.Text))
            {
                settings.DownloadWaitRandomMs = int.Parse(this.downloadWaitRandomTextBox.Text);
            }

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


            if (!String.IsNullOrEmpty(this.maxTilesPerStitchedImageTextBox.Text))
            {
                settings.MaximumStitchedImageSize = int.Parse(this.maxTilesPerStitchedImageTextBox.Text);
            }

            // Index 0 = yes, index 1 = no
            if (this.gcWriteImagesWithMaskCombo.SelectedIndex == 0)
            {
                settings.GeoConvertWriteImagesWithMask = true;
            }
            else
            {
                settings.GeoConvertWriteImagesWithMask = false;
            }

            if (this.gcWriteRawFilesComboBox.SelectedIndex == 0)
            {
                settings.GeoConvertWriteRawFiles= true;
            }
            else
            {
                settings.GeoConvertWriteRawFiles = false;
            }

            //if (this.gcDoMultipleSmallerRunsComboBox.SelectedIndex == 0)
            //{
            //    settings.GeoConvertDoMultipleSmallerRuns= true;
            //}
            //else
            //{
            //    settings.GeoConvertDoMultipleSmallerRuns = false;
            //}


            if (!String.IsNullOrEmpty(this.usgsUsernameTextBox.Text))
            {
                settings.USGSUsername = this.usgsUsernameTextBox.Text;
            }

            if (!String.IsNullOrEmpty(this.usgsPasswordTextBox.Text))
            {
                settings.USGSPassword = this.usgsPasswordTextBox.Text;
            }

            settings.ShrinkTMCGridSquareCoords = double.Parse(this.shrinkTMCGridSquaresTextBox.Text, NumberStyles.Any, CultureInfo.InvariantCulture);

            AeroSceneryManager.Instance.SaveSettings();
            this.Hide();
            log.Info("Settings saved");
        }

        private void SettingsForm_Shown(object sender, EventArgs e)
        {
            var settings = AeroSceneryManager.Instance.Settings;

            this.workingFolderTextBox.Text = settings.WorkingDirectory;
            this.aeroSceneryDatabaseFolderTextBox.Text = settings.AeroSceneryDBDirectory;
            this.afsSDKFolderTextBox.Text = settings.AFS2SDKDirectory;
            this.afsUserFolderTextBox.Text = settings.AFS2UserDirectory;

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

            this.maxTilesPerStitchedImageTextBox.Text = settings.MaximumStitchedImageSize.ToString();

            //if (settings.GeoConvertDoMultipleSmallerRuns)
            //{
            //    this.gcDoMultipleSmallerRunsComboBox.SelectedIndex = 0;
            //}
            //else
            //{
            //    this.gcDoMultipleSmallerRunsComboBox.SelectedIndex = 1;
            //}

            if (settings.GeoConvertWriteImagesWithMask.Value)
            {
                this.gcWriteImagesWithMaskCombo.SelectedIndex = 0;
            }
            else
            {
                this.gcWriteImagesWithMaskCombo.SelectedIndex = 1;
            }

            if (settings.GeoConvertWriteRawFiles.Value)
            {
                this.gcWriteRawFilesComboBox.SelectedIndex = 0;
            }
            else
            {
                this.gcWriteRawFilesComboBox.SelectedIndex = 1;
            }

            this.usgsUsernameTextBox.Text = settings.USGSUsername;
            this.usgsPasswordTextBox.Text = settings.USGSPassword;

            this.shrinkTMCGridSquaresTextBox.Text = settings.ShrinkTMCGridSquareCoords.ToString();
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
            var settings = AeroSceneryManager.Instance.Settings;
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.aeroSceneryDatabaseFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
                settings.AeroSceneryDBDirectory = this.aeroSceneryDatabaseFolderTextBox.Text;
            }
        }

        private void sdkButton_Click(object sender, EventArgs e)
        {
            var settings = AeroSceneryManager.Instance.Settings;
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.afsSDKFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
                settings.AFS2SDKDirectory = this.afsSDKFolderTextBox.Text;
            }
        }

        private void afsUserFolderButton_Click(object sender, EventArgs e)
        {
            var settings = AeroSceneryManager.Instance.Settings;
            DialogResult result = this.folderBrowserDialog1.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.afsUserFolderTextBox.Text = folderBrowserDialog1.SelectedPath;
                settings.AFS2UserDirectory = this.afsUserFolderTextBox.Text;
            }
        }

        private string pathWithTrailingDirectorySeparatorChar(string path)
        {
            if (!String.IsNullOrEmpty(path))
            {
                // They're always one character but EndsWith is shorter than
                // array style access to last path character. Change this
                // if performance are a (measured) issue.
                string separator1 = Path.DirectorySeparatorChar.ToString();
                string separator2 = Path.AltDirectorySeparatorChar.ToString();

                // Trailing white spaces are always ignored but folders may have
                // leading spaces. It's unusual but it may happen. If it's an issue
                // then just replace TrimEnd() with Trim(). Tnx Paul Groke to point this out.
                path = path.TrimEnd();

                // Argument is always a directory name then if there is one
                // of allowed separators then I have nothing to do.
                if (path.EndsWith(separator1) || path.EndsWith(separator2))
                    return path;

                // If there is the "alt" separator then I add a trailing one.
                // Note that URI format (file://drive:\path\filename.ext) is
                // not supported in most .NET I/O functions then we don't support it
                // here too. If you have to then simply revert this check:
                // if (path.Contains(separator1))
                //     return path + separator1;
                //
                // return path + separator2;
                if (path.Contains(separator2))
                    return path + separator2;

                // If there is not an "alt" separator I add a "normal" one.
                // It means path may be with normal one or it has not any separator
                // (for example if it's just a directory name). In this case I
                // default to normal as users expect.
                return path + separator1;
            }

            return path;

        }

        private void maxTilesPerStitchedImageTextBox_TextChanged(object sender, EventArgs e)
        {
            var numbersOnly = this.GetNumbers(this.maxTilesPerStitchedImageTextBox.Text);

            if (this.maxTilesPerStitchedImageTextBox.Text != numbersOnly)
            {
                this.maxTilesPerStitchedImageTextBox.Text = numbersOnly;
            }

            int maxTiles = 0;
            if (int.TryParse(this.maxTilesPerStitchedImageTextBox.Text, out maxTiles))
            {
                int resolution = 256 * maxTiles;
                this.maxTilesPerStitchedImageInfoLabel.Text = string.Format("tiles x {0} tiles. ({1}px x {2}px)", maxTiles, resolution, resolution);
            }

        }

        private string GetNumbers(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        private string GetDecimal(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c == '.').ToArray());
        }

        private void downloadWaitTextBox_TextChanged(object sender, EventArgs e)
        {
            var numbersOnly = this.GetNumbers(this.downloadWaitTextBox.Text);

            if (this.downloadWaitTextBox.Text != numbersOnly)
            {
                this.downloadWaitTextBox.Text = numbersOnly;
            }
        }

        private void downloadWaitRandomTextBox_TextChanged(object sender, EventArgs e)
        {
            var numbersOnly = this.GetNumbers(this.downloadWaitRandomTextBox.Text);

            if (this.downloadWaitRandomTextBox.Text != numbersOnly)
            {
                this.downloadWaitRandomTextBox.Text = numbersOnly;
            }
        }

        private void createUSGSAccountLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://ers.cr.usgs.gov/register/");
        }

        private void ShrinkTMCGridSquaresTextBox_TextChanged(object sender, EventArgs e)
        {
            var numbersOnly = this.GetDecimal(this.shrinkTMCGridSquaresTextBox.Text);

            if (this.shrinkTMCGridSquaresTextBox.Text != numbersOnly)
            {
                this.shrinkTMCGridSquaresTextBox.Text = numbersOnly;
            }
        }

        private void AddUserFolderToConfigButton_Click(object sender, EventArgs e)
        {

        }
    }
}
