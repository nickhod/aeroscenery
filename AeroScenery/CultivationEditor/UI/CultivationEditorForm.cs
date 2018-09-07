using AeroScenery.AFS2;
using AeroScenery.Common;
using AeroScenery.SceneryEditor.Common;
using AeroScenery.SceneryEditor.GMapCustom;
using AeroScenery.SceneryEditor.Models;
using AeroScenery.SceneryEditor.Project;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AeroScenery.SceneryEditor.UI
{
    public partial class CultivationEditorForm : Form
    {
        private ProjectWindow projectWindow;
        private ToolboxPanel toolboxPanel;
        private PropertiesPanel propertiesPanel;
        private BuildPanel buildPanel;
        private ProjectService projectService;

        private SceneryEditorProject sceneryEditorProject;


        public event EventHandler CultivationEditorFormClosed;

        private SceneryTool currentSceneryTool;

        private GMapOverlay planMarkers;

        public CultivationEditorForm()
        {
            InitializeComponent();

            //https://github.com/alexander-makarov/ExpandCollapsePanel

            this.toolboxPanel  = new ToolboxPanel();
            this.toolboxPanel.Show(this.dockPanel1, DockState.DockLeft);

            this.projectWindow = new ProjectWindow();
            this.projectWindow.Show(this.dockPanel1, DockState.Document);

            this.propertiesPanel = new PropertiesPanel();
            this.propertiesPanel.Show(this.dockPanel1, DockState.DockRight);

            this.buildPanel = new BuildPanel();
            this.buildPanel.Show(this.dockPanel1, DockState.DockBottomAutoHide);

            this.dockPanel1.DockLeftPortion = 225;
            this.dockPanel1.DockRightPortion = 300;

            this.projectService = new ProjectService();

            sceneryEditorProject = new SceneryEditorProject();

        }

        public void Initialize()
        {
            this.currentSceneryTool = SceneryTool.Pointer;

            this.toolboxPanel.SceneryToolSelected += ToolboxPanel_SceneryToolSelected;
            this.projectWindow.EditorMap.MouseUp += EditorMap_MouseUp;
        }

        private void EditorMap_MouseUp(object sender, MouseEventArgs e)
        {
            double lat = this.projectWindow.EditorMap.FromLocalToLatLng(e.X, e.Y).Lat;
            double lng = this.projectWindow.EditorMap.FromLocalToLatLng(e.X, e.Y).Lng;

            switch (currentSceneryTool)
            {
                case SceneryTool.SinglePlant:
                    this.DrawMapPolygon(lat, lng);
                    break;
            }
        }

        private void DrawMapPolygon(double lat, double lng)
        {
            //var area = GeoCoordinatesHelper.RectangleFromCenterPoint(new GeoCoordinate(lat, lng), 0.01, 0.01);

            //var pointList = new List<PointLatLng>();
            //pointList.Add(new PointLatLng(area.NorthLatitude, area.WestLongitude));
            //pointList.Add(new PointLatLng(area.NorthLatitude, area.EastLongitude));
            //pointList.Add(new PointLatLng(area.SouthLatitude, area.EastLongitude));
            //pointList.Add(new PointLatLng(area.SouthLatitude, area.WestLongitude));

            //GMapPolygon polygon = new GMapPolygon(pointList, "Test");

            //polygon.Fill = new SolidBrush(Color.FromArgb(40, Color.Green));
            //polygon.Stroke = new Pen(Color.Green, 2);


            //GMapOverlay polygonOverlay = new GMapOverlay("Test");
            //polygonOverlay.Polygons.Add(polygon);
            //this.projectWindow.EditorMap.Overlays.Add(polygonOverlay);

            //this.projectWindow.EditorMap.Refresh();
            //polygonOverlay.IsVisibile = false;
            //polygonOverlay.IsVisibile = true;

            if (this.planMarkers == null)
            {
                this.planMarkers = new GMapOverlay("Plant Markers");
                this.projectWindow.EditorMap.Overlays.Add(this.planMarkers);
            }

            GMapMarker marker = new PlantMarker(new PointLatLng(lat, lng), this.projectWindow.EditorMap);

            this.planMarkers.Markers.Add(marker);

            this.projectWindow.EditorMap.Refresh();


        }

        private void ToolboxPanel_SceneryToolSelected(SceneryTool sceneryTool, object sender, EventArgs e)
        {
            this.currentSceneryTool = sceneryTool;
        }

        private void CultivationEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.projectWindow.EditorMap.Manager.CancelTileCaching();
            this.projectWindow.Dispose();
        }

        private void SceneryEditorForm_Resize(object sender, EventArgs e)
        {
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private async void saveProjectToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {

            this.sceneryEditorProject.ProjectSettings = new ProjectSettings();

            var filename = AeroSceneryManager.Instance.Settings.WorkingDirectory + "project.aeroproj";

            await projectService.SaveProject(this.sceneryEditorProject, filename);
        }

        private void runToolStripButton_ButtonClick(object sender, EventArgs e)
        {
            var tocFile = new TOCFile();

            var filename = AeroSceneryManager.Instance.Settings.WorkingDirectory + "test.toc";


            File.WriteAllText(filename, tocFile.ToString());
        }

        private void CultivationEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (CultivationEditorFormClosed != null)
            {
                CultivationEditorFormClosed(this, e);
            }
        }

        private async void saveProjectAsToolStripMenuItem_ClickAsync(object sender, EventArgs e)
        {
            SaveFileDialog savefile = new SaveFileDialog();

            savefile.FileName = "Project.aeroproj";
            savefile.Filter = "AeroScenery Scenery Editor Project Files (*.aeroproj)|*.aeroproj";

            if (savefile.ShowDialog() == DialogResult.OK)
            {
                await projectService.SaveProject(this.sceneryEditorProject, savefile.FileName);
            }

        }
    }
}
