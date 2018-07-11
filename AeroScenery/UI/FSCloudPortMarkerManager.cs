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

namespace AeroScenery.UI
{
    public class FSCloudPortMarkerManager
    {
        public GMapControl GMapControl { get; set; }

        private GMapOverlay airportMarkers;

        private Dictionary<string, FSCloudPortAirport> airportLookup;

        public FSCloudPortMarkerManager()
        {
            airportMarkers = new GMapOverlay("FSCloudPortAirportMarkers");
            airportLookup = new Dictionary<string, FSCloudPortAirport>();
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

        public void AirportMakerClick(string icao)
        {
            var airport = this.airportLookup[icao];
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
    }
}
