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

namespace AeroScenery.CultivationEditor.UI
{
    public partial class ProjectWindow : WeifenLuo.WinFormsUI.Docking.DockContent
    {


        public ProjectWindow()
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
        }

        public GMapControl EditorMap
        {
            get
            {
                return this.editorMap;
            }
        }

        private void editorMap_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void editorMap_MouseUp(object sender, MouseEventArgs e)
        {

        }
    }
}
