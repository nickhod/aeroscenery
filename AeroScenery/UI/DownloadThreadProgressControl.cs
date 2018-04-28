using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.UI
{
    public partial class DownloadThreadProgressControl : UserControl
    {
        public DownloadThreadProgressControl()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        public void SetProgressPercentage(int percentage)
        {
            this.progressBar.Value = percentage;
            this.percentageLabel.Text = percentage.ToString() + "%";
        }

        public void SetDownloadThreadNumber(int number)
        {
            this.downloadThreadNumberLabel.Text = number.ToString();
        }

        public void SetImageTileCount(int imageTilesDone, int totalImageTiles)
        {
            this.imageTileCountLabel.Text = String.Format("{0} of {1} Image Tiles Downloaded", imageTilesDone, totalImageTiles);
        }
    }
}
