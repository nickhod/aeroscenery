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

        // Whether we have finished initially updating the UI with settings
        // We can therefore ignore control events until this is true
        private bool uiSetFromSettings;

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
            this.uiSetFromSettings = false;

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

        public void Initialize()
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.IsBalloon = true;
            toolTip1.InitialDelay = 500;
            toolTip1.SetToolTip(this.generateAFS2LevelsHelpImage, "Aerofly FS2 has image sets at different levels of detail.\nHere you can control which levels the images downloaded should be displayed on.");

            this.UpdateUIFromSettings();
        }

        public void UpdateUIFromSettings()
        {
            var settings = AeroSceneryManager.Instance.Settings;

            // Orthophoto Source
            switch (settings.OrthophotoSource)
            {
                case OrthoPhotoSources.OrthophotoSource.Bing:
                    this.imageSourceComboBox.SelectedIndex = 0;
                    break;
                case OrthoPhotoSources.OrthophotoSource.Google:
                    this.imageSourceComboBox.SelectedIndex = 1;
                    break;
                case OrthoPhotoSources.OrthophotoSource.USGS:
                    this.imageSourceComboBox.SelectedIndex = 2;
                    break;
            }

            // Zoom Level
            this.zoomLevelLabel.Text = settings.ZoomLevel.ToString();
            this.zoomLevelTrackBar.Value = settings.ZoomLevel;

            // AFS Levels To Generate
            // Our minimum is 8
            foreach (int afsLevel in settings.AFSLevelsToGenerate)
            {
                this.afsLevelsCheckBoxList.SetItemChecked(afsLevel - 8, true);
            }

            switch (settings.ActionSet)
            {
                case Common.ActionSet.Custom:
                    this.actionSetComboBox.SelectedIndex = 1;
                    this.SetCustomActions();
                    break;
                case Common.ActionSet.Default:
                    this.actionSetComboBox.SelectedIndex = 0;
                    this.SetDefaultActions();
                    break;
            }

            this.uiSetFromSettings = true;

        }

        private void SetDefaultActions()
        {
            this.downloadImageTileCheckBox.Checked = true;
            this.stitchImageTilesCheckBox.Checked = true;
            this.generateAFSFilesCheckBox.Checked = true;
            this.runGeoConvertCheckBox.Checked = true;
            this.installSceneryIntoAFSCheckBox.Checked = true;

            this.downloadImageTileCheckBox.Enabled = false;
            this.stitchImageTilesCheckBox.Enabled = false;
            this.generateAFSFilesCheckBox.Enabled = false;
            this.runGeoConvertCheckBox.Enabled = false;
            this.installSceneryIntoAFSCheckBox.Enabled = false;
        }

        private void SetCustomActions()
        {
            this.downloadImageTileCheckBox.Checked = true;
            this.stitchImageTilesCheckBox.Checked = true;
            this.generateAFSFilesCheckBox.Checked = true;
            this.runGeoConvertCheckBox.Checked = true;
            this.installSceneryIntoAFSCheckBox.Checked = true;

            this.downloadImageTileCheckBox.Enabled = true;
            this.stitchImageTilesCheckBox.Enabled = true;
            this.generateAFSFilesCheckBox.Enabled = true;
            this.runGeoConvertCheckBox.Enabled = true;
            this.installSceneryIntoAFSCheckBox.Enabled = true;
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
            if (settingsForm.StartPosition == FormStartPosition.CenterParent)
            {
                var x = Location.X + (Width - settingsForm.Width) / 2;
                var y = Location.Y + (Height - settingsForm.Height) / 2;
                settingsForm.Location = new Point(Math.Max(x, 0), Math.Max(y, 0));
            }
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

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void imageSourceComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var settings = AeroSceneryManager.Instance.Settings;

            switch(this.imageSourceComboBox.SelectedIndex)
            {
                // Bing
                case 0:
                    settings.OrthophotoSource = OrthoPhotoSources.OrthophotoSource.Bing;
                    break;
                // Google
                case 1:
                    settings.OrthophotoSource = OrthoPhotoSources.OrthophotoSource.Google;                
                    break;
                // USGS
                case 2:
                    settings.OrthophotoSource = OrthoPhotoSources.OrthophotoSource.USGS;
                    break;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void actionSetComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.actionSetComboBox.SelectedIndex)
            {
                // Default
                case 0:
                    AeroSceneryManager.Instance.Settings.ActionSet = Common.ActionSet.Default;
                    this.SetDefaultActions();
                    break;
                // Custom
                case 1:
                    AeroSceneryManager.Instance.Settings.ActionSet = Common.ActionSet.Custom;
                    this.SetCustomActions();
                    break;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void zoomLevelTrackBar_Scroll(object sender, EventArgs e)
        {
            this.zoomLevelLabel.Text = this.zoomLevelTrackBar.Value.ToString();
            AeroSceneryManager.Instance.Settings.ZoomLevel = this.zoomLevelTrackBar.Value;
            AeroSceneryManager.Instance.SaveSettings();
        }

        private void gridSquareLevelsCheckBoxList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (uiSetFromSettings)
            {
                var settings = AeroSceneryManager.Instance.Settings;

                var checkedLevel = e.Index + 8;

                if (settings.AFSLevelsToGenerate.Contains(checkedLevel))
                {
                    settings.AFSLevelsToGenerate.Remove(checkedLevel);
                }
                else
                {
                    settings.AFSLevelsToGenerate.Add(checkedLevel);
                }
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void downloadImageTileCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (downloadImageTileCheckBox.Checked)
            {
                AeroSceneryManager.Instance.Settings.DownloadImageTiles = true;
            }
            else
            {
                AeroSceneryManager.Instance.Settings.DownloadImageTiles = false;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void stitchImageTilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (stitchImageTilesCheckBox.Checked)
            {
                AeroSceneryManager.Instance.Settings.StitchImageTiles = true;
            }
            else
            {
                AeroSceneryManager.Instance.Settings.StitchImageTiles = false;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void generateAFSFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (generateAFSFilesCheckBox.Checked)
            {
                AeroSceneryManager.Instance.Settings.GenerateAIDAndTMCFiles = true;
            }
            else
            {
                AeroSceneryManager.Instance.Settings.GenerateAIDAndTMCFiles = false;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void runGeoConvertCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (runGeoConvertCheckBox.Checked)
            {
                AeroSceneryManager.Instance.Settings.RunGeoConvert = true;
            }
            else
            {
                AeroSceneryManager.Instance.Settings.RunGeoConvert = false;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void deleteStitchedImagesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (deleteStitchedImagesCheckBox.Checked)
            {
                AeroSceneryManager.Instance.Settings.DeleteStitchedImageTiles = true;
            }
            else
            {
                AeroSceneryManager.Instance.Settings.DeleteStitchedImageTiles = false;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void installSceneryIntoAFSCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (downloadImageTileCheckBox.Checked)
            {
                AeroSceneryManager.Instance.Settings.DownloadImageTiles = true;
            }
            else
            {
                AeroSceneryManager.Instance.Settings.DownloadImageTiles = false;
            }

            AeroSceneryManager.Instance.SaveSettings();
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            var url = "https://github.com/nickhod/aeroscenery";
            System.Diagnostics.Process.Start(url);
        }

        private void getSDKToolStripButton_Click(object sender, EventArgs e)
        {
            var url = "https://www.aerofly.com/community/filebase/index.php?file/2-sdk-tools/";
            System.Diagnostics.Process.Start(url);
        }

        public void UpdateParentTaskLabel(string parentTask)
        {
            this.parentTaskLabel.Text = parentTask;
        }

        public void UpdateChildTaskLabel(string childTask)
        {
            this.childTaskLabel.Text = childTask;
        }

        public void UpdateTaskLabels(string parentTask, string childTask)
        {
            this.parentTaskLabel.Text = parentTask;
            this.childTaskLabel.Text = childTask;
        }

        public void UpdateProgress(int progress)
        {
            this.overallProgressProgressBar.Value = progress;
        }
    }
}
