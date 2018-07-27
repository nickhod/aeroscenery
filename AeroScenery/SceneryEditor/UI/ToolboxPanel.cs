using AeroScenery.SceneryEditor.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AeroScenery.SceneryEditor.UI
{
    public partial class ToolboxPanel : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public delegate void SceneryToolEventHandler(SceneryTool sceneryTool, object sender, EventArgs e);
        public event SceneryToolEventHandler SceneryToolSelected;

        public ToolboxPanel()
        {
            InitializeComponent();
        }

        private void ToolBoxButton_Click(object sender, EventArgs e)
        {
            var buttonTag = ((Button)sender).Tag;

            switch (buttonTag)
            {
                case "Pointer":
                    SceneryToolSelected?.Invoke(SceneryTool.Pointer, this, e);
                    break;
                case "SinglePlant":
                    SceneryToolSelected?.Invoke(SceneryTool.SinglePlant, this, e);
                    break;
            }
        }
    }
}
