using AeroScenery.Controls;
using AeroScenery.Data.Models;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.UI
{
    public class FSCloudPortMarkerManager
    {
        public GMapControl GMapControl { get; set; }

        private GMapOverlay airportMarkers;

        private Dictionary<string, FSCloudPortAirport> airportLookup;

        private FSCloudPortAirportPopup fsCloudPortPopup;

        private PopperContainer containerForFSCloudPortPopup;

        public bool PopupShown { get; set; }

        public int ClickCount { get; set; }

        public FSCloudPortMarkerManager()
        {
            airportMarkers = new GMapOverlay("FSCloudPortAirportMarkers");
            airportLookup = new Dictionary<string, FSCloudPortAirport>();

            // FSCloudPort popup window
            this.fsCloudPortPopup = new FSCloudPortAirportPopup();
            this.containerForFSCloudPortPopup = new PopperContainer(fsCloudPortPopup);
            this.containerForFSCloudPortPopup.AutoClose = false;

            this.fsCloudPortPopup.CloseClicked += FsCloudPortPopup_CloseClicked;

            this.containerForFSCloudPortPopup.Opened += ContainerForFSCloudPortPopup_Opened;
            this.containerForFSCloudPortPopup.Closed += ContainerForFSCloudPortPopup_Closed;
        }

        private void ContainerForFSCloudPortPopup_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            this.PopupShown = false;
        }

        private void ContainerForFSCloudPortPopup_Opened(object sender, EventArgs e)
        {
            this.ClickCount = 0;
            this.PopupShown = true;
        }

        private void FsCloudPortPopup_CloseClicked(object sender, EventArgs e)
        {
            this.containerForFSCloudPortPopup.Close();
        }

        public void UpdateFSCloudPortMarkers()
        {
            airportMarkers.Markers.Clear();
            this.GMapControl.Overlays.Remove(airportMarkers);

            var mapBounds = this.GMapControl.ViewArea;

            if (this.GMapControl.Zoom >= 7 && this.airportLookup != null && mapBounds != null)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();

                foreach (var airport in this.airportLookup.Values)
                {
                    if (mapBounds.Left < airport.Longitude &&
                        mapBounds.Right > airport.Longitude &&
                        mapBounds.Top > airport.Latitude &&
                        mapBounds.Bottom < airport.Latitude)
                    {
                        var point = new PointLatLng(airport.Latitude, airport.Longitude);
                        var marker = new GMarkerGoogle(point, new Bitmap(Properties.Resources.windsock));
                        marker.Tag = airport.ICAO;
                        airportMarkers.Markers.Add(marker);
                    }

                }

                watch.Stop();
                var elapsedMs = watch.ElapsedMilliseconds;
                Debug.WriteLine("Looped through airports in " + elapsedMs + "ms");
            }


            this.GMapControl.Overlays.Add(airportMarkers);
            //this.GMapControl.Refresh();
        }

        public IList<FSCloudPortAirport> Airports
        {
            set
            {
                foreach (var airport in value)
                {
                    airportLookup.Add(airport.ICAO, airport);
                }
            }
        }

        public void RemoveAllFSCloudPortMarkers()
        {
            airportMarkers.Markers.Clear();
            this.GMapControl.Overlays.Remove(airportMarkers);
        }

        public void ShowAirportPopup(string icao, Form form, Point location)
        {
            FSCloudPortAirport airport = null;

            if (this.airportLookup.TryGetValue(icao, out airport))
            {
                this.fsCloudPortPopup.Airport = airport;
                this.containerForFSCloudPortPopup.Show(form, location);

            }

        }

        public void CloseAirportPopup()
        {
            this.containerForFSCloudPortPopup.Close();
        }



    }
}
