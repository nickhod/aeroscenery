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
using System.Diagnostics;
using System.Drawing;
using System.IO;
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

        public AFS2GridSquare SelectedAFS2GridSquare;

        private bool mouseDownOnMap;

        private AFS2Grid afs2Grid;
        private List<DownloadThreadProgressControl> downloadThreadProgressControls;

        private AeroScenery.Common.Point mapMouseDownLocation;

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

            this.gridSquareLabel.Text = "";
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

        private void SelectGridSquare(int x, int y)
        {
            double lat = mainMap.FromLocalToLatLng(x, y).Lat;
            double lon = mainMap.FromLocalToLatLng(x, y).Lng;

            // Get the grid square for this lat and lon
            var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, 9);

            gridSquareLabel.Text = gridSquare.Name;

            // Set the stroke with of any previously selected grid square to 1

            if (this.SelectedAFS2GridSquare != null)
            {
                if (this.SelectedAFS2GridSquares.ContainsKey(this.SelectedAFS2GridSquare.Name))
                {
                    var previouslySelectedGridSquare = this.SelectedAFS2GridSquares[this.SelectedAFS2GridSquare.Name];
                    previouslySelectedGridSquare.Item2.Polygons.FirstOrDefault().Stroke = new Pen(Color.Blue, 1);
                }

            }



            if (!this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
            {
                GMapPolygon polygon = new GMapPolygon(gridSquare.Coordinates, gridSquare.Name);
                polygon.Fill = new SolidBrush(Color.FromArgb(40, Color.Blue));
                polygon.Stroke = new Pen(Color.DeepSkyBlue, 2);

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
            else
            {
                // We have to draw it again so that it's on top
                var existingSquareAndOverlay = this.SelectedAFS2GridSquares[gridSquare.Name];
                existingSquareAndOverlay.Item2.Clear();
                existingSquareAndOverlay.Item2.Dispose();

                GMapPolygon polygon = new GMapPolygon(gridSquare.Coordinates, gridSquare.Name);
                polygon.Fill = new SolidBrush(Color.FromArgb(40, Color.Blue));
                polygon.Stroke = new Pen(Color.DeepSkyBlue, 2);

                GMapOverlay polygonOverlay = new GMapOverlay(gridSquare.Name);
                polygonOverlay.Polygons.Add(polygon);
                mainMap.Overlays.Add(polygonOverlay);

                mainMap.Refresh();
                polygonOverlay.IsVisibile = false;
                polygonOverlay.IsVisibile = true;

                this.SelectedAFS2GridSquares[gridSquare.Name] = new Tuple<AFS2GridSquare, GMapOverlay>(gridSquare, polygonOverlay);
            }

            this.SelectedAFS2GridSquare = gridSquare;
        }

        private void DeselectGridSquare(int x, int y)
        {
            double lat = mainMap.FromLocalToLatLng(x, y).Lat;
            double lon = mainMap.FromLocalToLatLng(x, y).Lng;

            // Get the grid square for this lat and lon
            var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, 9);

            // If this grid square is already selected, deselect it
            if (this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
            {
                var squareAndOverlay = this.SelectedAFS2GridSquares[gridSquare.Name];

                mainMap.Overlays.Remove(squareAndOverlay.Item2);
                this.SelectedAFS2GridSquares.Remove(gridSquare.Name);
                this.SelectedAFS2GridSquare = null;
            }

            this.SelectedAFS2GridSquare = null;
            gridSquareLabel.Text = "";

        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void mainMap_MouseDown_1(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.mapMouseDownLocation = new AeroScenery.Common.Point(e.X, e.Y);
            }
        }

        private void mainMap_MouseUp_1(object sender, MouseEventArgs e)
        {
            Debug.WriteLine("mouse up ");

            if (this.mapMouseDownLocation != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    var mouseUpLocation = new Point(e.X, e.Y);

                    var dx = Math.Abs(mouseUpLocation.X - this.mapMouseDownLocation.X);
                    var dy = Math.Abs(mouseUpLocation.Y - this.mapMouseDownLocation.Y);

                    // If there was little movement it was probably meant as a click
                    // rather than a drag
                    if (dx < 10 && dy < 10)
                    {
                        this.SelectGridSquare(e.X, e.Y);
                    }
                }
            }



        }


        private void mainMap_DoubleClick(object sender, EventArgs e)
        {
            var evt = (MouseEventArgs)e;
            this.mapMouseDownLocation = null;


            Debug.WriteLine("double click");
            double lat = mainMap.FromLocalToLatLng(evt.X, evt.Y).Lat;
            double lon = mainMap.FromLocalToLatLng(evt.X, evt.Y).Lng;

            // Get the grid square for this lat and lon
            var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, 9);

            if (this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
            {
                this.DeselectGridSquare(evt.X, evt.Y);
            }


        }

        private void openInGoogleMapsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.SelectedAFS2GridSquare != null)
            {
                var selectedGridSquare = this.SelectedAFS2GridSquare;
                var googleMapsUrl = "https://www.google.com/maps/@{0},{1},60000m/data=!3m1!1e3";
                System.Diagnostics.Process.Start(String.Format(googleMapsUrl, selectedGridSquare.GetCenter().Lat, selectedGridSquare.GetCenter().Lng));
            }
        }

        private void openInBingMApsToolStripMenuItem_Click(object sender, EventArgs e)
        {       
            if (this.SelectedAFS2GridSquare != null)
            {
                var selectedGridSquare = this.SelectedAFS2GridSquare;
                var bingMapsUrl = "https://www.bing.com/maps/default.aspx?cp={0}~{1}&lvl=10&style=h";
                System.Diagnostics.Process.Start(String.Format(bingMapsUrl, selectedGridSquare.GetCenter().Lat, selectedGridSquare.GetCenter().Lng));
            }

        }

        private void openImageFolderToolstripButton_Click(object sender, EventArgs e)
        {
            if (this.SelectedAFS2GridSquare != null)
            {
                var gridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + this.SelectedAFS2GridSquare.Name;

                if (Directory.Exists(gridSquareDirectory))
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = gridSquareDirectory,
                        UseShellExecute = true,
                        Verb = "open"
                    });
                }
                else
                {
                    MessageBox.Show(String.Format("There is no image folder yet for grid square {0}", this.SelectedAFS2GridSquare));
                }
            }
        }

        private void deleteImagesToolStripButton_Click(object sender, EventArgs e)
        {
            if (this.SelectedAFS2GridSquare != null)
            {
                var gridSquareDirectory = AeroSceneryManager.Instance.Settings.WorkingDirectory + this.SelectedAFS2GridSquare.Name;

                if (Directory.Exists(gridSquareDirectory))
                {
                    DialogResult result = MessageBox.Show("Are you sure you want to delete all images for this grid square? (No AFS2 Scenery will be affected).",
                        "AeroScenery",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (result == DialogResult.Yes )
                    {
                        System.IO.DirectoryInfo di = new DirectoryInfo(gridSquareDirectory);

                        Task.Run(() =>
                        {
                            foreach (FileInfo file in di.GetFiles())
                            {
                                file.Delete();
                            }
                            foreach (DirectoryInfo dir in di.GetDirectories())
                            {
                                dir.Delete(true);
                            }
                        });
                    
                    }
                }
                else
                {
                    MessageBox.Show(String.Format("There is no image folder yet for grid square {0}", this.SelectedAFS2GridSquare.Name));
                }
            }
        }
    }
}
