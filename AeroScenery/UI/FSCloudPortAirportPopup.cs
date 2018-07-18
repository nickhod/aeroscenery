using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AeroScenery.Controls;
using AeroScenery.Data.Models;

namespace AeroScenery.UI
{
    public partial class FSCloudPortAirportPopup : UserControl
    {
        public event EventHandler CloseClicked;

        private FSCloudPortAirport airport;


        public FSCloudPortAirportPopup()
        {
            InitializeComponent();
        }

        public FSCloudPortAirport Airport
        {
            get
            {
                return this.airport;
            }
            set
            {
                this.icaoLabel.Text = value.ICAO;
                this.nameLabel.Text = value.Name;
                this.aircraftLabel.Text = string.Format("{0} Static Aircraft", value.StaticAircraft.ToString());
                this.buildingsLabel.Text = string.Format("{0} Buildings", value.Buildings.ToString());
                this.runwaysLabel.Text = string.Format("{0} Runways", value.Runways.ToString());
                this.lastUpdatedLabel.Text = string.Format("{0} UTC", value.LastModified);

                this.airport = value;
            }
        }


        private void DownloadButton_Click(object sender, EventArgs e)
        {
            CloseClicked(this, e);
            System.Diagnostics.Process.Start(string.Format("http://www.fscloudport.com{0}", airport.Url));
        }

        private void CloseLabel_Click(object sender, EventArgs e)
        {
            CloseClicked(this, e);
        }
    }
}
