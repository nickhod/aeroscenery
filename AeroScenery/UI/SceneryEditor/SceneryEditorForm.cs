using GMap.NET.MapProviders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.UI
{
    public partial class SceneryEditorForm : Form
    {
        public SceneryEditorForm()
        {
            InitializeComponent();

            editorMap.MapProvider = GMapProviders.OpenStreetMap;
            //mainMap.Position = new PointLatLng(54.6961334816182, 25.2985095977783);
            editorMap.MinZoom = 0;
            editorMap.MaxZoom = 24;
            editorMap.Zoom = 5;
            editorMap.DragButton = MouseButtons.Left;

            //editorMap.MouseMove += new MouseEventHandler(MainMap_MouseMove);
            //editorMap.MouseDown += new MouseEventHandler(MainMap_MouseDown);
            //editorMap.MouseUp += new MouseEventHandler(MainMap_MouseUp);

            //https://github.com/alexander-makarov/ExpandCollapsePanel
        }

        private void SceneryEditorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            editorMap.Manager.CancelTileCaching();
            editorMap.Dispose();
        }
    }
}
