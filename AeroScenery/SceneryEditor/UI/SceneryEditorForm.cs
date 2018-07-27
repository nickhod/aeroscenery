using AeroScenery.AFS2;
using AeroScenery.SceneryEditor.Models;
using AeroScenery.SceneryEditor.Project;
using GMap.NET.MapProviders;
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
    public partial class SceneryEditorForm : Form
    {
        private ProjectWindow projectWindow;
        private ToolboxPanel toolboxPanel;
        private PropertiesPanel propertiesPanel;
        private BuildPanel buildPanel;

        private SceneryEditorProject sceneryEditorProject;

        public event EventHandler OnFormClosed;

        public SceneryEditorForm()
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

            sceneryEditorProject = new SceneryEditorProject();

        }

        public void Initialize()
        {
        }

        private void SceneryEditorForm_FormClosing(object sender, FormClosingEventArgs e)
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
            var projectService = new ProjectService();

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

        private void SceneryEditorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (OnFormClosed != null)
            {
                OnFormClosed(this, e);
            }
        }
    }
}
