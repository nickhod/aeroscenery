using AeroScenery.AFS2;
using AeroScenery.OrthophotoSources;
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
        private bool mouseDownOnMap;
        private AFS2Grid afs2Grid;



        public event EventHandler StartClicked;

        public List<AFS2GridSquare> SelectedAFS2GridSquares { get; set; }

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


            SelectedAFS2GridSquares = new List<AFS2GridSquare>();
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

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                double lat = mainMap.FromLocalToLatLng(e.X, e.Y).Lat;
                double lon = mainMap.FromLocalToLatLng(e.X, e.Y).Lng;

                var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, 9);

                GMapPolygon polygon = new GMapPolygon(gridSquare.Coordinates, gridSquare.Name);
                polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Blue));
                polygon.Stroke = new Pen(Color.Blue, 1);

                GMapOverlay polygons = new GMapOverlay("polygons");
                polygons.Polygons.Add(polygon);
                mainMap.Overlays.Add(polygons);

                mainMap.Refresh();
                polygons.IsVisibile = false;
                polygons.IsVisibile = true;

                PointLatLng pointLatLon = new PointLatLng();
                pointLatLon.Lat = lat;
                pointLatLon.Lng = lon;
                var gPoint = mainMap.FromLatLngToLocal(pointLatLon);

                //BingOrthophotoSource asdf = new BingOrthophotoSource();

                //asdf.DoStuff(gPoint);
            }

        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainMap.Manager.CancelTileCaching();
            mainMap.Dispose();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            StartClicked(this, e);
        }
    }
}
