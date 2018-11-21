namespace AeroScenery.CultivationEditor.UI
{
    partial class CultivationEditorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CultivationEditorForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveProjectAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.runToolStripButton = new System.Windows.Forms.ToolStripSplitButton();
            this.runAndStartAeroflyFS2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mapTypeToolStripDropDown = new System.Windows.Forms.ToolStripDropDownButton();
            this.hybridToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.satelliteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bingHybridMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bingSatelliteMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.binStandardMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openStreetMapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.vS2005Theme1 = new WeifenLuo.WinFormsUI.Docking.VS2005Theme();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1270, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.saveProjectToolStripMenuItem,
            this.saveProjectAsToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newProjectToolStripMenuItem
            // 
            this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
            this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.newProjectToolStripMenuItem.Text = "New Project";
            // 
            // openProjectToolStripMenuItem
            // 
            this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
            this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.openProjectToolStripMenuItem.Text = "Open Project";
            this.openProjectToolStripMenuItem.Click += new System.EventHandler(this.openProjectToolStripMenuItem_Click);
            // 
            // saveProjectToolStripMenuItem
            // 
            this.saveProjectToolStripMenuItem.Enabled = false;
            this.saveProjectToolStripMenuItem.Name = "saveProjectToolStripMenuItem";
            this.saveProjectToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveProjectToolStripMenuItem.Text = "Save Project";
            this.saveProjectToolStripMenuItem.Click += new System.EventHandler(this.saveProjectToolStripMenuItem_ClickAsync);
            // 
            // saveProjectAsToolStripMenuItem
            // 
            this.saveProjectAsToolStripMenuItem.Name = "saveProjectAsToolStripMenuItem";
            this.saveProjectAsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.saveProjectAsToolStripMenuItem.Text = "Save Project As";
            this.saveProjectAsToolStripMenuItem.Click += new System.EventHandler(this.saveProjectAsToolStripMenuItem_ClickAsync);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(151, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runToolStripButton,
            this.toolStripButton2,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.mapTypeToolStripDropDown});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 2, 1, 2);
            this.toolStrip1.Size = new System.Drawing.Size(1270, 38);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // runToolStripButton
            // 
            this.runToolStripButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.runAndStartAeroflyFS2ToolStripMenuItem});
            this.runToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("runToolStripButton.Image")));
            this.runToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.runToolStripButton.Name = "runToolStripButton";
            this.runToolStripButton.Size = new System.Drawing.Size(62, 31);
            this.runToolStripButton.Text = "Run";
            this.runToolStripButton.ButtonClick += new System.EventHandler(this.runToolStripButton_ButtonClick);
            // 
            // runAndStartAeroflyFS2ToolStripMenuItem
            // 
            this.runAndStartAeroflyFS2ToolStripMenuItem.Name = "runAndStartAeroflyFS2ToolStripMenuItem";
            this.runAndStartAeroflyFS2ToolStripMenuItem.Size = new System.Drawing.Size(225, 22);
            this.runAndStartAeroflyFS2ToolStripMenuItem.Text = "Run And Start Aerofly FS2";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(118, 31);
            this.toolStripButton2.Text = "Project Settings";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(111, 31);
            this.toolStripButton1.Text = "Load GIS Data";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 34);
            // 
            // mapTypeToolStripDropDown
            // 
            this.mapTypeToolStripDropDown.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hybridToolStripMenuItem,
            this.satelliteToolStripMenuItem,
            this.sToolStripMenuItem,
            this.bingHybridMapToolStripMenuItem,
            this.bingSatelliteMapToolStripMenuItem,
            this.binStandardMapToolStripMenuItem,
            this.openStreetMapToolStripMenuItem});
            this.mapTypeToolStripDropDown.Image = ((System.Drawing.Image)(resources.GetObject("mapTypeToolStripDropDown.Image")));
            this.mapTypeToolStripDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mapTypeToolStripDropDown.Name = "mapTypeToolStripDropDown";
            this.mapTypeToolStripDropDown.Size = new System.Drawing.Size(95, 31);
            this.mapTypeToolStripDropDown.Text = "Map Type";
            // 
            // hybridToolStripMenuItem
            // 
            this.hybridToolStripMenuItem.Name = "hybridToolStripMenuItem";
            this.hybridToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.hybridToolStripMenuItem.Tag = "GoogleHybrid";
            this.hybridToolStripMenuItem.Text = "Google Hybrid Map";
            // 
            // satelliteToolStripMenuItem
            // 
            this.satelliteToolStripMenuItem.Name = "satelliteToolStripMenuItem";
            this.satelliteToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.satelliteToolStripMenuItem.Tag = "GoogleSatellite";
            this.satelliteToolStripMenuItem.Text = "Google Satellite Map";
            // 
            // sToolStripMenuItem
            // 
            this.sToolStripMenuItem.Name = "sToolStripMenuItem";
            this.sToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.sToolStripMenuItem.Tag = "GoogleStandard";
            this.sToolStripMenuItem.Text = "Google Standard Map";
            // 
            // bingHybridMapToolStripMenuItem
            // 
            this.bingHybridMapToolStripMenuItem.Name = "bingHybridMapToolStripMenuItem";
            this.bingHybridMapToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.bingHybridMapToolStripMenuItem.Tag = "BingHybrid";
            this.bingHybridMapToolStripMenuItem.Text = "Bing Hybrid Map";
            // 
            // bingSatelliteMapToolStripMenuItem
            // 
            this.bingSatelliteMapToolStripMenuItem.Name = "bingSatelliteMapToolStripMenuItem";
            this.bingSatelliteMapToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.bingSatelliteMapToolStripMenuItem.Tag = "BingSatellite";
            this.bingSatelliteMapToolStripMenuItem.Text = "Bing Satellite Map";
            // 
            // binStandardMapToolStripMenuItem
            // 
            this.binStandardMapToolStripMenuItem.Name = "binStandardMapToolStripMenuItem";
            this.binStandardMapToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.binStandardMapToolStripMenuItem.Tag = "BingStandard";
            this.binStandardMapToolStripMenuItem.Text = "Bing Standard Map";
            // 
            // openStreetMapToolStripMenuItem
            // 
            this.openStreetMapToolStripMenuItem.Name = "openStreetMapToolStripMenuItem";
            this.openStreetMapToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.openStreetMapToolStripMenuItem.Tag = "OpenStreetMap";
            this.openStreetMapToolStripMenuItem.Text = "Open Street Map";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 755);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1270, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatusLabel1.Text = "toolStripStatusLabel1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dockPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1270, 693);
            this.panel1.TabIndex = 3;
            // 
            // dockPanel1
            // 
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingWindow;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.Size = new System.Drawing.Size(1270, 693);
            this.dockPanel1.TabIndex = 1;
            // 
            // CultivationEditorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1270, 777);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "CultivationEditorForm";
            this.Text = "AeroScenery Cultivation Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CultivationEditorForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CultivationEditorForm_FormClosed);
            this.Resize += new System.EventHandler(this.SceneryEditorForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
        private WeifenLuo.WinFormsUI.Docking.VS2005Theme vS2005Theme1;
        private System.Windows.Forms.ToolStripMenuItem saveProjectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveProjectAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripDropDownButton mapTypeToolStripDropDown;
        private System.Windows.Forms.ToolStripMenuItem hybridToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem satelliteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bingHybridMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bingSatelliteMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem binStandardMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openStreetMapToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton runToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem runAndStartAeroflyFS2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
    }
}