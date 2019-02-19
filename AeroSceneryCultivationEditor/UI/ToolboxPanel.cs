using AeroScenery.CultivationEditor.Common;
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
    public partial class ToolboxPanel : WeifenLuo.WinFormsUI.Docking.DockContent
    {
        public delegate void SceneryToolEventHandler(SceneryTool sceneryTool, object sender, EventArgs e);
        public event SceneryToolEventHandler SceneryToolSelected;

        public ToolboxPanel()
        {
            InitializeComponent();
            this.pointerToolBoxButton.BackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
            SceneryToolSelected?.Invoke(SceneryTool.Pointer, this, new EventArgs());
        }

        private void ToolBoxButton_Click(object sender, EventArgs e)
        {
            var buttonTag = ((Button)sender).Tag;
            this.ResetToolButtonBackColors();

            switch (buttonTag)
            {
                case "Pointer":
                    SceneryToolSelected?.Invoke(SceneryTool.Pointer, this, e);
                    this.pointerToolBoxButton.BackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
                    break;
                //case "Plant":
                //    SceneryToolSelected?.Invoke(SceneryTool.Plant, this, e);
                //    this.singlePlantToolBoxButton.BackColor = Color.FromKnownColor(KnownColor.ActiveBorder);
                //    break;
            }
        }

        private void ResetToolButtonBackColors()
        {
            this.pointerToolBoxButton.BackColor = Color.FromKnownColor(KnownColor.Control);
            this.singlePlantToolBoxButton.BackColor = Color.FromKnownColor(KnownColor.Control);
        }
    }
}
