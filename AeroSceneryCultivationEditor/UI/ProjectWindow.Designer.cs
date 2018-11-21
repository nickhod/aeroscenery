namespace AeroScenery.CultivationEditor.UI
{
    partial class ProjectWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.editorMap = new GMap.NET.WindowsForms.GMapControl();
            this.SuspendLayout();
            // 
            // editorMap
            // 
            this.editorMap.Bearing = 0F;
            this.editorMap.CanDragMap = true;
            this.editorMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.editorMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.editorMap.GrayScaleMode = false;
            this.editorMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.editorMap.LevelsKeepInMemmory = 5;
            this.editorMap.Location = new System.Drawing.Point(0, 0);
            this.editorMap.MarkersEnabled = true;
            this.editorMap.MaxZoom = 2;
            this.editorMap.MinZoom = 2;
            this.editorMap.MouseWheelZoomEnabled = true;
            this.editorMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.editorMap.Name = "editorMap";
            this.editorMap.NegativeMode = false;
            this.editorMap.PolygonsEnabled = true;
            this.editorMap.RetryLoadTile = 0;
            this.editorMap.RoutesEnabled = true;
            this.editorMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.editorMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.editorMap.ShowTileGridLines = false;
            this.editorMap.Size = new System.Drawing.Size(814, 639);
            this.editorMap.TabIndex = 1;
            this.editorMap.Zoom = 0D;
            this.editorMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.editorMap_MouseDown);
            this.editorMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.editorMap_MouseUp);
            // 
            // ProjectWindow
            // 
            this.AllowEndUserDocking = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 639);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.Controls.Add(this.editorMap);
            this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
            this.Name = "ProjectWindow";
            this.TabText = "New Project";
            this.Text = "ProjectWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl editorMap;
    }
}