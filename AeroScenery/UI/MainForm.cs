using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.Data;
using AeroScenery.Data.Mappers;
using AeroScenery.Data.Models;
using AeroScenery.FSCloudPort;
using AeroScenery.OrthophotoSources;
using AeroScenery.UI;
using AeroScenery.USGS;
using AeroScenery.USGS.Models;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery
{
    public partial class MainForm : Form
    {
        public event EventHandler StartStopClicked;
        public event EventHandler<string> ResetGridSquare;
        public Dictionary<string, GridSquareViewModel> SelectedAFS2GridSquares;
        public Dictionary<string, GridSquareViewModel> DownloadedAFS2GridSquares;
        public AFS2GridSquare SelectedAFS2GridSquare;

        private bool mouseDownOnMap;
        private AFS2Grid afs2Grid;
        private List<DownloadThreadProgressControl> downloadThreadProgressControls;
        private AeroScenery.Common.Point mapMouseDownLocation;
        private IDataRepository dataRepository;
        private GridSquareMapper gridSquareMapper;
        private GMapOverlay activeGridSquareOverlay;
        private bool actionsRunning;
        private readonly ILog log = LogManager.GetLogger("AeroScenery");
        private GMapControlManager gMapControlManager;
        private FSCloudPortService fsCloudPortService;
        private FSCloudPortMarkerManager fsCloudPortMarkerManager;

        private VersionService versionService;


        // Whether we have finished initially updating the UI with settings
        // We can therefore ignore control events until this is true
        private bool uiSetFromSettings;

        private MainFormSideTab currentMainFormSideTab;
        private int afsGridSquareSelectionSize;

        // Whether the user should be shown a dialog about how changing the selection size
        // removes any current selections.
        private bool shownSelectionSizeChangeInfo;

        public MainForm()
        {
            InitializeComponent();

            this.afs2Grid = new AFS2Grid();
            this.gridSquareMapper = new GridSquareMapper();
            this.gMapControlManager = new GMapControlManager();
            this.fsCloudPortMarkerManager = new FSCloudPortMarkerManager();
            this.fsCloudPortService = new FSCloudPortService();
            this.versionService = new VersionService();

            this.actionsRunning = false;

            mainMap.MapProvider = GMapProviders.GoogleHybridMap;
            //mainMap.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            mainMap.MinZoom = 0;
            mainMap.MaxZoom = 24;
            mainMap.Zoom = 5;
            mainMap.DragButton = MouseButtons.Left;
            mainMap.IgnoreMarkerOnMouseWheel = true;

            SelectedAFS2GridSquares = new Dictionary<string, GridSquareViewModel>();
            DownloadedAFS2GridSquares = new Dictionary<string, GridSquareViewModel>();

            this.downloadThreadProgressControls = new List<DownloadThreadProgressControl>();
            this.uiSetFromSettings = false;

            this.afsGridSquareSelectionSize = 9;
            this.gridSquareSelectionSizeToolstripCombo.SelectedIndex = 0;

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

            this.currentMainFormSideTab = MainFormSideTab.Images;
            // Until if / when we need it
            this.sideTabControl.TabPages.Remove(this.manualElevationTabPage);

            this.gMapControlManager.GMapControl = this.mainMap;
            this.fsCloudPortMarkerManager.GMapControl = this.mainMap;

            this.shownSelectionSizeChangeInfo = true;
        }

        public void Initialize()
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.IsBalloon = true;
            toolTip1.InitialDelay = 500;
            toolTip1.SetToolTip(this.generateAFS2LevelsHelpImage, "Aerofly FS2 has image sets at different levels of detail.\nHere you can control which levels the images downloaded should be displayed on.");

            this.UpdateUIFromSettings();

            this.dataRepository = new SqlLiteDataRepository();
            this.dataRepository.Settings = AeroSceneryManager.Instance.Settings;

            this.LoadDownloadedGridSquares();

            TextBoxAppender.ConfigureTextBoxAppender(this.logTextBox);

            versionToolStripLabel.Text = "v" + AeroSceneryManager.Instance.Version;
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            log.Info(String.Format("AeroScenery v{0} Started", AeroSceneryManager.Instance.Version));
            this.versionService.CheckForNewerVersions();

            await this.fsCloudPortService.UpdateAirportsIfRequiredAsync();
            var airports = await this.fsCloudPortService.GetAirportsAsync();
            this.fsCloudPortMarkerManager.Airports = airports;
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
            this.zoomLevelTrackBar.Value = settings.ZoomLevel;
            this.setZoomLevelLabelText();


            // AFS Levels To Generate
            // Our minimum is 9
            foreach (int afsLevel in settings.AFSLevelsToGenerate)
            {
                this.afsLevelsCheckBoxList.SetItemChecked(afsLevel - 9, true);
            }

            // Action set
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
            var settings = AeroSceneryManager.Instance.Settings;
            // Actions
            this.downloadImageTileCheckBox.Checked = settings.DownloadImageTiles;
            this.stitchImageTilesCheckBox.Checked = settings.StitchImageTiles;
            this.generateAFSFilesCheckBox.Checked = settings.GenerateAIDAndTMCFiles;
            this.runGeoConvertCheckBox.Checked = settings.RunGeoConvert;
            this.deleteStitchedImagesCheckBox.Checked = settings.DeleteStitchedImageTiles;
            this.installSceneryIntoAFSCheckBox.Checked = settings.InstallScenery;

            this.downloadImageTileCheckBox.Enabled = true;
            this.stitchImageTilesCheckBox.Enabled = true;
            this.generateAFSFilesCheckBox.Enabled = true;
            this.runGeoConvertCheckBox.Enabled = true;
            this.installSceneryIntoAFSCheckBox.Enabled = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            mainMap.Manager.CancelTileCaching();
            mainMap.Dispose();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            // Are we currently running actions
            if (this.ActionsRunning)
            {
                this.mainTabControl.SelectedIndex = 0;
                this.ActionsRunning = false;
                this.ResetProgress();
            }
            else
            {
                this.mainTabControl.SelectedIndex = 1;
                this.ActionsRunning = true;
            }


            StartStopClicked(this, e);
        }

        private void ResetProgress()
        {
            this.downloadThreadProgress1.Reset();
            this.downloadThreadProgress2.Reset();
            this.downloadThreadProgress3.Reset();
            this.downloadThreadProgress4.Reset();
            this.currentActionProgressBar.Value = 0;
        }

        public DownloadThreadProgressControl GetDownloadThreadProgressControl(int downloadThread)
        {
            if (downloadThread < this.downloadThreadProgressControls.Count)
            {
                return this.downloadThreadProgressControls[downloadThread];
            }

            return null;
        }

        private void SelectAFSGridSquare(int x, int y)
        {
            double lat = mainMap.FromLocalToLatLng(x, y).Lat;
            double lon = mainMap.FromLocalToLatLng(x, y).Lng;

            // Get the grid square for this lat and lon
            var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, this.afsGridSquareSelectionSize);

            gridSquareLabel.Text = gridSquare.Name;

            // Set the map overlay of any previously selected grid square to visisble
            if (this.SelectedAFS2GridSquare != null)
            {
                if (this.SelectedAFS2GridSquares.ContainsKey(this.SelectedAFS2GridSquare.Name))
                {
                    var previouslySelectedGridSquare = this.SelectedAFS2GridSquares[this.SelectedAFS2GridSquare.Name];
                    previouslySelectedGridSquare.GMapOverlay.IsVisibile = true;
                }

            }

            // Clear the previous active overlay
            if (this.activeGridSquareOverlay != null)
            {
                this.activeGridSquareOverlay.Clear();
                this.activeGridSquareOverlay.Dispose();
                this.activeGridSquareOverlay = null;
            }


            // Is this a grid square that is already selected
            if (!this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
            {
                // Add the selected map overlay but make it invislbe for now
                var selectedGridSquare = this.gMapControlManager.DrawGridSquare(gridSquare, GridSquareDisplayType.Selected);
                selectedGridSquare.IsVisibile = false;

                // Add the AFS2 Grid Squrea and the GMapOverlay to the selected grid squares dictionary
                var gridSquareViewModel = new GridSquareViewModel();
                gridSquareViewModel.GMapOverlay = selectedGridSquare;
                gridSquareViewModel.AFS2GridSquare = gridSquare;

                this.SelectedAFS2GridSquares.Add(gridSquare.Name, gridSquareViewModel);


                // Create the active grid square map overlay, let it be visible
                this.activeGridSquareOverlay = this.gMapControlManager.DrawGridSquare(gridSquare, GridSquareDisplayType.Active);
            }
            else
            {
                // Create the active grid square map overlay, let it be visible
                this.activeGridSquareOverlay = this.gMapControlManager.DrawGridSquare(gridSquare, GridSquareDisplayType.Active);
            }

            this.SelectedAFS2GridSquare = gridSquare;
            this.UpdateStatusStrip();
            this.UpdateToolStrip();

            log.InfoFormat("Grid square {0} selected", gridSquare.Name);
        }

        private void DeselectAFSGridSquare(int x, int y)
        {
            double lat = mainMap.FromLocalToLatLng(x, y).Lat;
            double lon = mainMap.FromLocalToLatLng(x, y).Lng;

            // Get the grid square for this lat and lon
            var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, this.afsGridSquareSelectionSize);

            // If this grid square is already selected, deselect it
            if (this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
            {
                var squareAndOverlay = this.SelectedAFS2GridSquares[gridSquare.Name];

                mainMap.Overlays.Remove(squareAndOverlay.GMapOverlay);
                this.SelectedAFS2GridSquares.Remove(gridSquare.Name);
                this.SelectedAFS2GridSquare = null;
            }

            this.SelectedAFS2GridSquare = null;
            gridSquareLabel.Text = "";

            this.activeGridSquareOverlay.Clear();
            this.activeGridSquareOverlay.Dispose();
            this.activeGridSquareOverlay = null;

            this.UpdateStatusStrip();
        }

        /// <summary>
        /// Clears any currently selected AFSGridSquares
        /// </summary>
        private void ClearAllSelectedAFSGridSquares()
        {
            foreach (var gridSquare in this.SelectedAFS2GridSquares.Values)
            {
                mainMap.Overlays.Remove(gridSquare.GMapOverlay);
            }


            if (this.activeGridSquareOverlay != null)
            {
                this.activeGridSquareOverlay.Clear();
                this.activeGridSquareOverlay.Dispose();
                this.activeGridSquareOverlay = null;
            }

            mainMap.Refresh();

            this.SelectedAFS2GridSquares.Clear();
            this.SelectedAFS2GridSquare = null;

            this.UpdateStatusStrip();
        }

        private void ClearAllSelectedUSGSGridSquares()
        {
            // TODO
        }

        private void SelectUSGSGridSquare(int x, int y)
        {
        }
        private void DeselectUSGSGridSquare(int x, int y)
        {
        }


        private void UpdateStatusStrip()
        {
            if (this.SelectedAFS2GridSquares.Count == 1)
            {
                this.statusStripLabel1.Text = String.Format("1 Grid Square Selected");
            }
            else
            {
                this.statusStripLabel1.Text = String.Format("{0} Grid Squares Selected", this.SelectedAFS2GridSquares.Count);
            }

            if (this.SelectedAFS2GridSquares.Count > 0)
            {
                this.startStopButton.Enabled = true;
            }
            else
            {
                this.startStopButton.Enabled = false;
            }

        }

        private void UpdateToolStrip()
        {
            if (this.SelectedAFS2GridSquare != null)
            {
                if (!this.DownloadedAFS2GridSquares.ContainsKey(this.SelectedAFS2GridSquare.Name))
                {
                    this.toolStripDownloadedLabel.Text = "Not Downloaded";
                    toolStripDownloadedLabel.Image = imageList1.Images[0];
                    resetSquareToolStripButton.Enabled = false;
                }
                else
                {
                    this.toolStripDownloadedLabel.Text = "Downloaded";
                    toolStripDownloadedLabel.Image = imageList1.Images[1];
                    resetSquareToolStripButton.Enabled = true;
                }
            }

        }

        public void LoadDownloadedGridSquares()
        {
            var gridSquares = this.dataRepository.GetAllGridSquares();

            foreach (GridSquare gridSquare in gridSquares)
            {
                var afs2GridSqure = this.gridSquareMapper.ToAFS2GridSquare(gridSquare);
                var polygonOverlay = this.gMapControlManager.DrawGridSquare(afs2GridSqure, GridSquareDisplayType.Downloaded);

                var gridSquareViewModel = new GridSquareViewModel();
                gridSquareViewModel.GMapOverlay = polygonOverlay;
                gridSquareViewModel.AFS2GridSquare = afs2GridSqure;

                this.DownloadedAFS2GridSquares[afs2GridSqure.Name] = gridSquareViewModel;

            }
        }

        public void AddDownloadedGridSquare(AFS2GridSquare afs2GridSqure)
        {
            var polygonOverlay = this.gMapControlManager.DrawGridSquare(afs2GridSqure, GridSquareDisplayType.Downloaded);

            var gridSquareViewModel = new GridSquareViewModel();
            gridSquareViewModel.GMapOverlay = polygonOverlay;
            gridSquareViewModel.AFS2GridSquare = afs2GridSqure;

            this.DownloadedAFS2GridSquares[afs2GridSqure.Name] = gridSquareViewModel;

        }

        private void settingsButton_Click(object sender, EventArgs e)
        {
            var settingsForm = new SettingsForm();
            settingsForm.Show();
            if (settingsForm.StartPosition == FormStartPosition.CenterParent)
            {
                var x = Location.X + (Width - settingsForm.Width) / 2;
                var y = Location.Y + (Height - settingsForm.Height) / 2;
                settingsForm.Location = new System.Drawing.Point(Math.Max(x, 0), Math.Max(y, 0));
            }
        }

        private void MainMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.mapMouseDownLocation = new AeroScenery.Common.Point(e.X, e.Y);
            }
        }

        private void MainMap_MouseUp(object sender, MouseEventArgs e)
        {
            if (this.mapMouseDownLocation != null)
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)
                {
                    var mouseUpLocation = new System.Drawing.Point(e.X, e.Y);

                    var dx = Math.Abs(mouseUpLocation.X - this.mapMouseDownLocation.X);
                    var dy = Math.Abs(mouseUpLocation.Y - this.mapMouseDownLocation.Y);

                    // If there was little movement it was probably meant as a click
                    // rather than a drag
                    if (dx < 10 && dy < 10)
                    {
                        if (!this.mainMap.IsMouseOverMarker)
                        {
                            switch (this.currentMainFormSideTab)
                            {
                                case MainFormSideTab.Images:
                                    this.SelectAFSGridSquare(e.X, e.Y);
                                    break;
                                case MainFormSideTab.Elevation:
                                    this.SelectUSGSGridSquare(e.X, e.Y);
                                    break;
                            }
                        }
                    }
                    else
                    {
                        this.fsCloudPortMarkerManager.UpdateFSCloudPortMarkers();
                    }
                }
            }



        }

        private void mainMap_DoubleClick(object sender, EventArgs e)
        {
            var evt = (MouseEventArgs)e;
            this.mapMouseDownLocation = null;

            double lat = mainMap.FromLocalToLatLng(evt.X, evt.Y).Lat;
            double lon = mainMap.FromLocalToLatLng(evt.X, evt.Y).Lng;

            // Get the grid square for this lat and lon
            var gridSquare = afs2Grid.GetGridSquareAtLatLon(lat, lon, this.afsGridSquareSelectionSize);

            if (this.SelectedAFS2GridSquares.ContainsKey(gridSquare.Name))
            {
                switch (this.currentMainFormSideTab)
                {
                    case MainFormSideTab.Images:
                        this.DeselectAFSGridSquare(evt.X, evt.Y);
                        break;
                    case MainFormSideTab.Elevation:
                        this.DeselectUSGSGridSquare(evt.X, evt.Y);
                        break;
                }
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
                    MessageBox.Show(String.Format("There is no image folder yet for grid square {0}", this.SelectedAFS2GridSquare.Name));
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
            if (this.uiSetFromSettings)
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

        }

        private void setZoomLevelLabelText()
        {
            double metersPerPixel = 0;

            switch (this.zoomLevelTrackBar.Value)
            {
                case 12:
                    metersPerPixel = 38.2185;
                    break;
                case 13:
                    metersPerPixel = 19.1093;
                    break;
                case 14:
                    metersPerPixel = 9.5546;
                    break;
                case 15:
                    metersPerPixel = 4.7773;
                    break;
                case 16:
                    metersPerPixel = 2.3887;
                    break;
                case 17:
                    metersPerPixel = 1.1943;
                    break;
                case 18:
                    metersPerPixel = 0.5972;
                    break;
                case 19:
                    metersPerPixel = 0.2986;
                    break;
                case 20:
                    metersPerPixel = 0.1493;
                    break;
            }

            this.zoomLevelLabel.Text = String.Format("{0} - {1} meters/pixel", this.zoomLevelTrackBar.Value, metersPerPixel.ToString("0.000"));
        }

        private void zoomLevelTrackBar_Scroll(object sender, EventArgs e)
        {
            this.setZoomLevelLabelText();
            AeroSceneryManager.Instance.Settings.ZoomLevel = this.zoomLevelTrackBar.Value;
            AeroSceneryManager.Instance.SaveSettings();
        }

        private void gridSquareLevelsCheckBoxList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (uiSetFromSettings)
            {
                var settings = AeroSceneryManager.Instance.Settings;

                var checkedLevel = e.Index + 9;

                // Don't let anyone select levels that are smaller than the grid square selection size
                if (checkedLevel < this.afsGridSquareSelectionSize)
                {
                    e.NewValue = e.CurrentValue;
                }
                else
                {
                    if (settings.AFSLevelsToGenerate.Contains(checkedLevel))
                    {
                        settings.AFSLevelsToGenerate.Remove(checkedLevel);
                    }
                    else
                    {
                        settings.AFSLevelsToGenerate.Add(checkedLevel);
                    }
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

        public bool ActionsRunning
        {
            get
            {
                return this.actionsRunning;
            }
            set
            {
                this.actionsRunning = value;

                if (this.actionsRunning)
                {
                    this.startStopButton.Text = "Stop";
                }
                else
                {
                    this.startStopButton.Text = "Start";
                }
            }
        }

        private void resetSquareToolStripButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to reset the downloaded status of this grid square? (No files will be deleted).",
                "AeroScenery",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                if (this.SelectedAFS2GridSquare != null)
                {
                    if (this.DownloadedAFS2GridSquares.ContainsKey(this.SelectedAFS2GridSquare.Name))
                    {
                        ResetGridSquare(this, this.SelectedAFS2GridSquare.Name);

                        var downloadedGridSquare = this.DownloadedAFS2GridSquares[this.SelectedAFS2GridSquare.Name];
                        downloadedGridSquare.GMapOverlay.Clear();
                        downloadedGridSquare.GMapOverlay.Dispose();

                        this.DownloadedAFS2GridSquares.Remove(this.SelectedAFS2GridSquare.Name);

                        this.SelectedAFS2GridSquares.Remove(this.SelectedAFS2GridSquare.Name);
                        this.SelectedAFS2GridSquare = null;
                        gridSquareLabel.Text = "";

                        this.activeGridSquareOverlay.Clear();
                        this.activeGridSquareOverlay.Dispose();
                        this.activeGridSquareOverlay = null;

                        this.UpdateStatusStrip();

                    }
                }
            }
        }

        public int CurrentActionProgressPercentage
        {
            get
            {
                return this.currentActionProgressBar.Value;
            }
            set
            {
                this.currentActionProgressBar.Value = value;
            }
        }

        public void ActionsComplete()
        {
            this.mainTabControl.SelectedIndex = 0;
            this.ActionsRunning = false;
            this.ResetProgress();
        }

        private void gridSquareSelectionSizeToolstripCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(this.gridSquareSelectionSizeToolstripCombo.SelectedIndex)
            {
                // 9
                case 0:
                    this.afsGridSquareSelectionSize = 9;
                    this.ClearAllSelectedAFSGridSquares();
                    break;

                // 13
                case 1:
                    this.afsGridSquareSelectionSize = 13;
                    this.ClearAllSelectedAFSGridSquares();
                    //for (int index = 0; index < this.afsLevelsCheckBoxList.Items.Count; ++index)
                    //{
                    //    if ((index + 9) < 13)
                    //    {
                    //        this.afsLevelsCheckBoxList.SetItemChecked(index, false);
                    //    }
                    //}
                    break;

                // 14
                case 2:
                    this.afsGridSquareSelectionSize = 14;
                    this.ClearAllSelectedAFSGridSquares();
                    break;
            }

            if (this.shownSelectionSizeChangeInfo)
            {
                DialogResult result = MessageBox.Show("Changing the grid square selection size removes any current selections.\nAeroScenery can only process one size of grid square per run.",
                    "AeroScenery",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                this.shownSelectionSizeChangeInfo = false;
            }

        }

        private async void usgsTestButton_Click(object sender, EventArgs e)
        {
            USGSInventoryService service = new USGSInventoryService();

            var loginRequest = new LoginRequest();
            loginRequest.Username = AeroSceneryManager.Instance.Settings.USGSUsername;
            loginRequest.Password = AeroSceneryManager.Instance.Settings.USGSPassword;
            loginRequest.CatalogId = CatalogType.EarthExplorer;
            loginRequest.AuthType = "EROS";
            var login = await service.LoginAsync(loginRequest);

            //var datasetSearchRequest = new DatasetSearchRequest();
            //datasetSearchRequest.DatasetName = "ASTER";
            //var datasets = await service.DatasetSearchAsync(datasetSearchRequest);

            var searchRequest = new SceneSearchRequest();
            //searchRequest.DatasetName = "ASTER_GLOBAL_DEM";
            searchRequest.DatasetName = "ASTER_GLOBAL_DEM_DE";
            //searchRequest.DatasetName = "LANDSAT_8";

            var spatialFilter = new SpatialFilter();
            spatialFilter.FilterType = "mbr";
            spatialFilter.LowerLeft = new Coordinate(51.469400, -3.163811);
            spatialFilter.UpperRight = new Coordinate(51.469400, -3.163811);
            //spatialFilter.LowerLeft = new Coordinate(75, -135);
            //spatialFilter.UpperRight = new Coordinate(90, -120);
            searchRequest.SpatialFilter = spatialFilter;

            var searchResult = await service.SceneSearch(searchRequest);



            var downloadOptionsRequest = new DownloadOptionsRequest();
            downloadOptionsRequest.DatasetName = "ASTER_GLOBAL_DEM_DE";
            downloadOptionsRequest.EntityIds = new string[] { "ASTGDEMV2_0N51W004" };

            var asdfdsf = await service.DownloadOptions(downloadOptionsRequest);

            int i = 0;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            USGSScraper scraper = new USGSScraper();
            await scraper.LoginAsync(AeroSceneryManager.Instance.Settings.USGSUsername, AeroSceneryManager.Instance.Settings.USGSPassword);
        }

        private void sideTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (this.sideTabControl.SelectedIndex)
            {
                case 0:
                    this.currentMainFormSideTab = MainFormSideTab.Images;
                    this.ClearAllSelectedUSGSGridSquares();
                    break;
                case 1:
                    this.currentMainFormSideTab = MainFormSideTab.Elevation;
                    this.ClearAllSelectedAFSGridSquares();
                    break;
            }
        }

        private void sceneryEditorToolstripButton_Click(object sender, EventArgs e)
        {
            new SceneryEditorForm().Show();

        }

        private void AutoSelectAFSLevelsButton_Click(object sender, EventArgs e)
        {

        }


        private void MainMap_OnMapZoomChanged()
        {
            this.fsCloudPortMarkerManager.UpdateFSCloudPortMarkers();
        }

        private void hybridToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void MainMap_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
            this.fsCloudPortMarkerManager.AirportMakerClick(item.Tag.ToString());
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
