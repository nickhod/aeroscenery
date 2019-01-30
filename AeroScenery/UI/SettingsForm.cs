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
using AeroScenery.ImageProcessing;

namespace AeroScenery.UI
{
    public partial class SettingsForm : Form
    {
        private readonly ILog log = LogManager.GetLogger("AeroScenery");

        private ImageProcessingPreviewForm imageProcessingPreviewForm;

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

            // Image processing
            settings.EnableImageProcessing = this.imageProcessingEnabledCheckBox.Checked;
            settings.BrightnessAdjustment = this.imgProcBrightnessSlider.Value;
            settings.ContrastAdjustment = this.imgProcContrastSlider.Value;
            settings.SaturationAdjustment = this.imgProcSaturationSlider.Value;
            settings.SharpnessAdjustment = this.imgProcSharpnessSlider.Value;
            settings.RedAdjustment = this.imgProcRedSlider.Value;
            settings.GreenAdjustment = this.imgProcGreenSlider.Value;
            settings.BlueAdjustment = this.imgProcBlueSlider.Value;

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

            // Image processing
            this.imageProcessingEnabledCheckBox.Checked = settings.EnableImageProcessing.Value;

            this.imgProcBrightnessSlider.Value = settings.BrightnessAdjustment.Value;
            this.imgProcBrightnessTextBox.Text = settings.BrightnessAdjustment.Value.ToString();

            this.imgProcContrastSlider.Value = settings.ContrastAdjustment.Value;
            this.imgProcContrastTextBox.Text = settings.ContrastAdjustment.Value.ToString();

            this.imgProcSaturationSlider.Value = settings.SaturationAdjustment.Value;
            this.imgProcSaturationTextBox.Text = settings.SaturationAdjustment.Value.ToString();

            this.imgProcSharpnessSlider.Value = settings.SharpnessAdjustment.Value;
            this.imgProcSharpnessTextBox.Text = settings.SharpnessAdjustment.Value.ToString();

            this.imgProcRedSlider.Value = settings.RedAdjustment.Value;
            this.imgProcRedTextBox.Text = settings.RedAdjustment.Value.ToString();
            this.imgProcGreenSlider.Value = settings.GreenAdjustment.Value;
            this.imgProcGreenTextBox.Text = settings.GreenAdjustment.Value.ToString();
            this.imgProcBlueSlider.Value = settings.BlueAdjustment.Value;
            this.imgProcBlueTextBox.Text = settings.BlueAdjustment.Value.ToString();

            // Enable or disable sliders depending on whether image processing is enabled
            if (this.imageProcessingEnabledCheckBox.Checked)
            {
                this.ToggleImageProcessingControlsEnabled(true);
            }
            else
            {
                this.ToggleImageProcessingControlsEnabled(false);
            }

        }

        private void ToggleImageProcessingControlsEnabled(bool enabled)
        {
            this.imgProcBrightnessSlider.Enabled = enabled;
            this.imgProcBrightnessTextBox.Enabled = enabled;

            this.imgProcContrastSlider.Enabled = enabled;
            this.imgProcContrastTextBox.Enabled = enabled;

            this.imgProcSaturationSlider.Enabled = enabled;
            this.imgProcSaturationTextBox.Enabled = enabled;

            this.imgProcSharpnessSlider.Enabled = enabled;
            this.imgProcSharpnessTextBox.Enabled = enabled;

            this.imgProcRedSlider.Enabled = enabled;
            this.imgProcRedTextBox.Enabled = enabled;
            this.imgProcGreenSlider.Enabled = enabled;
            this.imgProcGreenTextBox.Enabled = enabled;
            this.imgProcBlueSlider.Enabled = enabled;
            this.imgProcBlueTextBox.Enabled = enabled;
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
            var numbersOnly = this.GetInteger(this.maxTilesPerStitchedImageTextBox.Text);

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

        private string GetInteger(string input)
        {
            return new string(input.Where(c => char.IsDigit(c)).ToArray());
        }

        private string GetDecimal(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c == '.').ToArray());
        }

        private string GetSignedInteger(string input)
        {
            return new string(input.Where(c => char.IsDigit(c) || c == '-').ToArray());
        }

        private void downloadWaitTextBox_TextChanged(object sender, EventArgs e)
        {
            var numbersOnly = this.GetInteger(this.downloadWaitTextBox.Text);

            if (this.downloadWaitTextBox.Text != numbersOnly)
            {
                this.downloadWaitTextBox.Text = numbersOnly;
            }
        }

        private void downloadWaitRandomTextBox_TextChanged(object sender, EventArgs e)
        {
            var numbersOnly = this.GetInteger(this.downloadWaitRandomTextBox.Text);

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

        private void button1_Click(object sender, EventArgs e)
        {

        }


        private void UpdateImagePreview()
        {
            if (this.imageProcessingPreviewForm != null)
            {
                if (this.imageProcessingPreviewForm.Visible)
                {
                    var imageProcessingSettings = new ImageProcessingSettings();
                    imageProcessingSettings.BrightnessAdjustment = this.imgProcBrightnessSlider.Value;
                    imageProcessingSettings.ContrastAdjustment = this.imgProcContrastSlider.Value;
                    imageProcessingSettings.SaturationAdjustment = this.imgProcSaturationSlider.Value;
                    imageProcessingSettings.SharpnessAdjustment = this.imgProcSharpnessSlider.Value;
                    imageProcessingSettings.RedAdjustment = this.imgProcRedSlider.Value;
                    imageProcessingSettings.GreenAdjustment = this.imgProcGreenSlider.Value;
                    imageProcessingSettings.BlueAdjustment = this.imgProcBlueSlider.Value;

                    this.imageProcessingPreviewForm.UpdateImage(imageProcessingSettings);
                }
            }
        }


        private void imgProcBrightnessSlider_ValueChanged(object sender, EventArgs e)
        {
            if (imgProcBrightnessTextBox.Text != imgProcBrightnessSlider.Value.ToString())
            {
                imgProcBrightnessTextBox.Text = imgProcBrightnessSlider.Value.ToString();
            }
        }


        private void imgProcContrastSlider_ValueChanged(object sender, EventArgs e)
        {
            if (imgProcContrastTextBox.Text != imgProcContrastSlider.Value.ToString())
            {
                imgProcContrastTextBox.Text = imgProcContrastSlider.Value.ToString();
            }

        }

        private void imgProcSaturationSlider_ValueChanged(object sender, EventArgs e)
        {
            if (imgProcSaturationTextBox.Text != imgProcSaturationSlider.Value.ToString())
            {
                imgProcSaturationTextBox.Text = imgProcSaturationSlider.Value.ToString();
            }
        }

        private void imgProcSharpnessSlider_ValueChanged(object sender, EventArgs e)
        {
            if (imgProcSharpnessTextBox.Text != imgProcSharpnessSlider.Value.ToString())
            {
                imgProcSharpnessTextBox.Text = imgProcSharpnessSlider.Value.ToString();
            }
        }

        private void imgProcRedSlider_ValueChanged(object sender, EventArgs e)
        {
            if (imgProcRedTextBox.Text != imgProcRedSlider.Value.ToString())
            {
                imgProcRedTextBox.Text = imgProcRedSlider.Value.ToString();
            }
        }

        private void imgProcGreenSlider_ValueChanged(object sender, EventArgs e)
        {
            if (imgProcGreenTextBox.Text != imgProcGreenSlider.Value.ToString())
            {
                imgProcGreenTextBox.Text = imgProcGreenSlider.Value.ToString();
            }
        }

        private void imgProcBlueSlider_ValueChanged(object sender, EventArgs e)
        {
            if (imgProcBlueTextBox.Text != imgProcBlueSlider.Value.ToString())
            {
                imgProcBlueTextBox.Text = imgProcBlueSlider.Value.ToString();
            }

        }

        private void imgProcBrightnessTextBox_Leave(object sender, EventArgs e)
        {
            this.fixLoneNegativeSign(imgProcBrightnessTextBox);
        }

        private void imgProcContrastTextBox_Leave(object sender, EventArgs e)
        {
            this.fixLoneNegativeSign(imgProcBlueTextBox);
        }

        private void imgProcSaturationTextBox_Leave(object sender, EventArgs e)
        {
            this.fixLoneNegativeSign(imgProcSaturationTextBox);        
        }

        private void imgProcSharpessTextBox_Leave(object sender, EventArgs e)
        {
            this.fixLoneNegativeSign(imgProcSharpnessTextBox);
        }

        private void imgProcRedTextBox_Leave(object sender, EventArgs e)
        {
            this.fixLoneNegativeSign(imgProcRedTextBox);
        }

        private void imgProcGreenTextBox_Leave(object sender, EventArgs e)
        {
            this.fixLoneNegativeSign(imgProcGreenTextBox);
        }

        private void imgProcBlueTextBox_Leave(object sender, EventArgs e)
        {
            this.fixLoneNegativeSign(imgProcBlueTextBox);
        }

        private void fixLoneNegativeSign(TextBox textBox)
        {
            if (textBox.Text == "-")
            {
                textBox.Text = "0";
            }
        }

        private void imgProcTextBoxTextChanged(TextBox textBox, TrackBar slider, int minValue, int maxValue)
        {
            var validatedText = this.GetSignedInteger(textBox.Text);

            if (validatedText != "-")
            {
                int intVal;

                if (int.TryParse(validatedText, out intVal))
                {
                    if (intVal < minValue)
                        intVal = minValue;

                    if (intVal > maxValue)
                        intVal = maxValue;

                    slider.Value = intVal;
                }

                if (textBox.Text != intVal.ToString())
                {
                    textBox.Text = intVal.ToString();
                }
            }
        }

        private void imgProcBrightnessTextBox_TextChanged(object sender, EventArgs e)
        {
            this.imgProcTextBoxTextChanged(this.imgProcBrightnessTextBox, this.imgProcBrightnessSlider, -100, 100);
        }

        private void imgProcContrastTextBox_TextChanged(object sender, EventArgs e)
        {
            this.imgProcTextBoxTextChanged(this.imgProcContrastTextBox, this.imgProcContrastSlider, -100, 100);
        }

        private void imgProcSaturationTextBox_TextChanged(object sender, EventArgs e)
        {
            this.imgProcTextBoxTextChanged(this.imgProcSaturationTextBox, this.imgProcSaturationSlider, -100, 100);
        }

        private void imgProcSharpessTextBox_TextChanged(object sender, EventArgs e)
        {
            this.imgProcTextBoxTextChanged(this.imgProcSharpnessTextBox, this.imgProcSharpnessSlider, 0, 100);
        }

        private void imgProcRedTextBox_TextChanged(object sender, EventArgs e)
        {
            this.imgProcTextBoxTextChanged(this.imgProcRedTextBox, this.imgProcRedSlider, -100, 100);
        }

        private void imgProcGreenTextBox_TextChanged(object sender, EventArgs e)
        {
            this.imgProcTextBoxTextChanged(this.imgProcGreenTextBox, this.imgProcGreenSlider, -100, 100);
        }

        private void imgProcBlueTextBox_TextChanged(object sender, EventArgs e)
        {
            this.imgProcTextBoxTextChanged(this.imgProcBlueTextBox, this.imgProcBlueSlider, -100, 100);
        }

        private void imageProcessingEnabledCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.imageProcessingEnabledCheckBox.Checked)
            {
                this.ToggleImageProcessingControlsEnabled(true);
            }
            else
            {
                this.ToggleImageProcessingControlsEnabled(false);
            }
        }

        private void showPreviewWindowButton_Click(object sender, EventArgs e)
        {
            if (this.imageProcessingPreviewForm == null)
            {
                this.imageProcessingPreviewForm = new ImageProcessingPreviewForm();
            }

            this.imageProcessingPreviewForm.StartPosition = FormStartPosition.Manual;
            this.imageProcessingPreviewForm.Height = this.Height;
            this.imageProcessingPreviewForm.Left = this.Right;
            this.imageProcessingPreviewForm.Top = this.Top;
            this.imageProcessingPreviewForm.Show();
            this.UpdateImagePreview();
        }
    }
}
