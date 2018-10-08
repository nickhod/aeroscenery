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
    public partial class DeleteSquareOptionsForm : Form
    {
        public bool DeleteMapImageTiles { get; set; }
        public bool DeleteStitchedImages { get; set; }
        public bool DeleteGeoconvertRawImages { get; set; }
        public bool DeleteTTCFiles { get; set; }

        public DeleteSquareOptionsForm()
        {
            InitializeComponent();
        }

        private void mapImageTilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.DeleteMapImageTiles = this.mapImageTilesCheckBox.Checked;

            if (!this.mapImageTilesCheckBox.Checked)
            {
                this.allFilesCheckBox.Checked = false;
            }
        }

        private void stitchedImagesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.DeleteStitchedImages = this.stitchedImagesCheckBox.Checked;

            if (!this.stitchedImagesCheckBox.Checked)
            {
                this.allFilesCheckBox.Checked = false;
            }
        }

        private void rawImagesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.DeleteGeoconvertRawImages = this.rawImagesCheckBox.Checked;

            if (!this.rawImagesCheckBox.Checked)
            {
                this.allFilesCheckBox.Checked = false;
            }
        }

        private void ttcFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.DeleteTTCFiles = this.ttcFilesCheckBox.Checked;

            if (!this.ttcFilesCheckBox.Checked)
            {
                this.allFilesCheckBox.Checked = false;
            }
        }

        private void allFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (this.allFilesCheckBox.Checked)
            {
                this.mapImageTilesCheckBox.Checked = true;
                this.stitchedImagesCheckBox.Checked = true;
                this.rawImagesCheckBox.Checked = true;
                this.ttcFilesCheckBox.Checked = true;
            }
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
