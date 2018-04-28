using AeroScenery.AFS2;
using AeroScenery.OrthophotoSources;
using AeroScenery.UI;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery
{
    public partial class MainForm : Form
    {
        public event EventHandler StartClicked;
        public Dictionary<string, Tuple<AFS2GridSquare, GMapOverlay>> SelectedAFS2GridSquares;

        private bool mouseDownOnMap;
        private AFS2Grid afs2Grid;
        private List<DownloadThreadProgressControl> downloadThreadProgressControls;


        public MainForm()
        {
            InitializeComponent();

            this.afs2Grid = new AFS2Grid();

            mainMap.MapProvider = GMapProviders.GoogleHybridMap;
            //mainMap.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            mainMap.MinZoom = 0;
            mainMap.MaxZoom = 24;
            mainMap.Zoom = 5;
            mainMap.DragButton = MouseButtons.Left;

            mainMap.MouseMove += new MouseEventHandler(MainMap_MouseMove);
            mainMap.MouseDown += new MouseEventHandler(MainMap_MouseDown);
            mainMap.MouseUp += new MouseEventHandler(MainMap_MouseUp);
            mainMap.MouseClick += new MouseEventHandler(MainMap_MouseClick);
            mainMap.MouseDoubleClick += new MouseEventHandler(MainMap_MouseDoubleClick);

            SelectedAFS2GridSquares = new Dictionary<string, Tuple<AFS2GridSquare, GMapOverlay>>();

            this.downloadThreadProgressControls = new List<DownloadThreadProgressControl>();

            // TODO - Make this dynamic
            this.downloadThreadProgressControls.Add(this.downloadThreadProgress1);
            this.downloadThreadProgressControls.Add(this.downloadThreadProgress2);
            this.downloadThreadProgressControls.Add(this.downloadThreadProgress3);
            this.downloadThreadProgressControls.Add(this.downloadThreadProgress4);

            this.downloadThreadProgress1.SetDownloadThreadNumber(1);
            this.downloadThreadProgress2.SetDownloadThreadNumber(2);
            this.downloadThreadProgress3.SetDownloadThreadNumber(3);
            this.downloadThreadProgress4.SetDownloadThreadNumber(4);
        }

        void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownOnMap = false;
            }
        }



        void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDownOnMap = true;
            }
        }


        private void MainMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mouseDownOnMap)
            {

            }
        }

        // add demo circle
        private void MainMap_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }

        private void MainMap_MouseClick(object sender, MouseEventArgs e)
        {
            // Should be left eventually, but we need to filter our dragging
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.ToggleTileSelected(e.X, e.Y);
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainMap.Manager.CancelTileCaching();
            mainMap.Dispose();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            this.mainTabControl.SelectedIndex = 1;

            StartClicked(this, e);
        }

        public DownloadThreadProgressControl GetDownloadThreadProgressControl(int downloadThread)
        {
            if (downloadThread < this.downloadThreadProgressControls.Count)
            {
                return this.downloadThreadProgressControls[downloadThread];
            }

            return null;
        }

        private void ToggleTileSelected(int x, int y)
        {
            double lat = mainMap.FromLocalToLatLng(x, y).Lat;
            double lon = mainMap.FromLocalToLatLng(x, y).Lng;

            // Get the grid square for this lat and lon
            var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, 9);

            // If this grid square is already selected, deselect it
            if(this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
            {
                if (this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
                {
                    var squareAndOverlay = this.SelectedAFS2GridSquares[gridSquare.Name];

                    mainMap.Overlays.Remove(squareAndOverlay.Item2);
                    this.SelectedAFS2GridSquares.Remove(gridSquare.Name);
                }


            }
            // This grid square is not selected, highlight it
            else
            {

                GMapPolygon polygon = new GMapPolygon(gridSquare.Coordinates, gridSquare.Name);
                polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Blue));
                polygon.Stroke = new Pen(Color.Blue, 1);

                GMapOverlay polygonOverlay = new GMapOverlay(gridSquare.Name);
                polygonOverlay.Polygons.Add(polygon);
                mainMap.Overlays.Add(polygonOverlay);

                mainMap.Refresh();
                polygonOverlay.IsVisibile = false;
                polygonOverlay.IsVisibile = true;

                // Add the AFS2 Grid Squrea and the GMapOverlay to the selected grid squares dictionary
                var squareAndOverlay = new Tuple<AFS2GridSquare, GMapOverlay>(gridSquare, polygonOverlay);

                this.SelectedAFS2GridSquares.Add(gridSquare.Name, squareAndOverlay);
            }


            //PointLatLng pointLatLon = new PointLatLng();
            //pointLatLon.Lat = lat;
            //pointLatLon.Lng = lon;

            //var gPoint = mainMap.FromLatLngToLocal(pointLatLon);

            //BingOrthophotoSource asdf = new BingOrthophotoSource();

            //asdf.DoStuff(gPoint);
        }
    }
}
