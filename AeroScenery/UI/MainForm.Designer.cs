namespace AeroScenery
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.mainMap = new GMap.NET.WindowsForms.GMapControl();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.statusStripLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.settingsButton = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.getSDKToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mainTabControl = new System.Windows.Forms.TabControl();
            this.mapTabPage = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.gridSquareLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripDownloadedLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.resetSquareToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.openImageFolderToolstripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteImagesToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.openInGoogleMapsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openInBingMApsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressTabPage = new System.Windows.Forms.TabPage();
            this.childTaskLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.downloadThreadProgress4 = new AeroScenery.UI.DownloadThreadProgressControl();
            this.downloadThreadProgress3 = new AeroScenery.UI.DownloadThreadProgressControl();
            this.downloadThreadProgress2 = new AeroScenery.UI.DownloadThreadProgressControl();
            this.downloadThreadProgress1 = new AeroScenery.UI.DownloadThreadProgressControl();
            this.label6 = new System.Windows.Forms.Label();
            this.currentActionProgressBar = new System.Windows.Forms.ProgressBar();
            this.parentTaskLabel = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.logTextBox = new System.Windows.Forms.TextBox();
            this.tabControl2 = new System.Windows.Forms.TabControl();
            this.imagesTabPage = new System.Windows.Forms.TabPage();
            this.generateAFS2LevelsHelpImage = new System.Windows.Forms.Label();
            this.zoomLevelLabel = new System.Windows.Forms.Label();
            this.zoomLevelTrackBar = new System.Windows.Forms.TrackBar();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.actionSetComboBox = new System.Windows.Forms.ComboBox();
            this.installSceneryIntoAFSCheckBox = new System.Windows.Forms.CheckBox();
            this.deleteStitchedImagesCheckBox = new System.Windows.Forms.CheckBox();
            this.runGeoConvertCheckBox = new System.Windows.Forms.CheckBox();
            this.generateAFSFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.stitchImageTilesCheckBox = new System.Windows.Forms.CheckBox();
            this.downloadImageTileCheckBox = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.afsLevelsCheckBoxList = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imageSourceComboBox = new System.Windows.Forms.ComboBox();
            this.terrainTabPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.startStopButton = new System.Windows.Forms.Button();
            this.shutdownCheckbox = new System.Windows.Forms.CheckBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.mapTabPage.SuspendLayout();
            this.panel1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.progressTabPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabControl2.SuspendLayout();
            this.imagesTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomLevelTrackBar)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.terrainTabPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMap
            // 
            this.mainMap.BackColor = System.Drawing.SystemColors.Control;
            this.mainMap.Bearing = 0F;
            this.mainMap.CanDragMap = true;
            this.mainMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainMap.EmptyTileColor = System.Drawing.Color.Navy;
            this.mainMap.GrayScaleMode = false;
            this.mainMap.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.mainMap.LevelsKeepInMemmory = 5;
            this.mainMap.Location = new System.Drawing.Point(3, 3);
            this.mainMap.MarkersEnabled = true;
            this.mainMap.MaxZoom = 2;
            this.mainMap.MinZoom = 2;
            this.mainMap.MouseWheelZoomEnabled = true;
            this.mainMap.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.mainMap.Name = "mainMap";
            this.mainMap.NegativeMode = false;
            this.mainMap.PolygonsEnabled = true;
            this.mainMap.RetryLoadTile = 0;
            this.mainMap.RoutesEnabled = true;
            this.mainMap.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.mainMap.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.mainMap.ShowTileGridLines = false;
            this.mainMap.Size = new System.Drawing.Size(1034, 724);
            this.mainMap.TabIndex = 0;
            this.mainMap.Zoom = 0D;
            this.mainMap.DoubleClick += new System.EventHandler(this.mainMap_DoubleClick);
            this.mainMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mainMap_MouseDown_1);
            this.mainMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mainMap_MouseUp_1);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusStripLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 821);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1461, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip";
            // 
            // statusStripLabel1
            // 
            this.statusStripLabel1.Name = "statusStripLabel1";
            this.statusStripLabel1.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsButton,
            this.helpToolStripButton,
            this.getSDKToolStripButton,
            this.toolStripSeparator4,
            this.toolStripLabel2,
            this.toolStripComboBox1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(12, 5, 0, 5);
            this.toolStrip1.Size = new System.Drawing.Size(1461, 42);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // settingsButton
            // 
            this.settingsButton.Image = ((System.Drawing.Image)(resources.GetObject("settingsButton.Image")));
            this.settingsButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.settingsButton.Name = "settingsButton";
            this.settingsButton.Size = new System.Drawing.Size(69, 29);
            this.settingsButton.Text = "Settings";
            this.settingsButton.Click += new System.EventHandler(this.settingsButton_Click);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(52, 29);
            this.helpToolStripButton.Text = "Help";
            this.helpToolStripButton.Click += new System.EventHandler(this.helpToolStripButton_Click);
            // 
            // getSDKToolStripButton
            // 
            this.getSDKToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("getSDKToolStripButton.Image")));
            this.getSDKToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.getSDKToolStripButton.Name = "getSDKToolStripButton";
            this.getSDKToolStripButton.Size = new System.Drawing.Size(133, 29);
            this.getSDKToolStripButton.Text = "Get AeroFly FS2 SDK";
            this.getSDKToolStripButton.Click += new System.EventHandler(this.getSDKToolStripButton_Click);
            // 
            // mainTabControl
            // 
            this.mainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mainTabControl.Controls.Add(this.mapTabPage);
            this.mainTabControl.Controls.Add(this.progressTabPage);
            this.mainTabControl.Controls.Add(this.tabPage5);
            this.mainTabControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainTabControl.Location = new System.Drawing.Point(401, 45);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.SelectedIndex = 0;
            this.mainTabControl.Size = new System.Drawing.Size(1048, 760);
            this.mainTabControl.TabIndex = 6;
            // 
            // mapTabPage
            // 
            this.mapTabPage.Controls.Add(this.panel1);
            this.mapTabPage.Controls.Add(this.mainMap);
            this.mapTabPage.Location = new System.Drawing.Point(4, 26);
            this.mapTabPage.Name = "mapTabPage";
            this.mapTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.mapTabPage.Size = new System.Drawing.Size(1040, 730);
            this.mapTabPage.TabIndex = 0;
            this.mapTabPage.Text = "Map";
            this.mapTabPage.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toolStrip2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 30);
            this.panel1.TabIndex = 1;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.gridSquareLabel,
            this.toolStripDownloadedLabel,
            this.toolStripSeparator3,
            this.resetSquareToolStripButton,
            this.toolStripSeparator1,
            this.openImageFolderToolstripButton,
            this.deleteImagesToolStripButton,
            this.toolStripSeparator2,
            this.toolStripDropDownButton1});
            this.toolStrip2.Location = new System.Drawing.Point(0, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1034, 25);
            this.toolStrip2.TabIndex = 1;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripLabel1.Margin = new System.Windows.Forms.Padding(0, 1, 5, 2);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(82, 22);
            this.toolStripLabel1.Text = "Grid Square:";
            // 
            // gridSquareLabel
            // 
            this.gridSquareLabel.AutoSize = false;
            this.gridSquareLabel.Margin = new System.Windows.Forms.Padding(0, 1, 8, 2);
            this.gridSquareLabel.Name = "gridSquareLabel";
            this.gridSquareLabel.Size = new System.Drawing.Size(125, 22);
            this.gridSquareLabel.Text = "map_09_xxxx_xxxx";
            // 
            // toolStripDownloadedLabel
            // 
            this.toolStripDownloadedLabel.Image = global::AeroScenery.Properties.Resources.arrow_down;
            this.toolStripDownloadedLabel.Margin = new System.Windows.Forms.Padding(0, 1, 10, 2);
            this.toolStripDownloadedLabel.Name = "toolStripDownloadedLabel";
            this.toolStripDownloadedLabel.Size = new System.Drawing.Size(124, 22);
            this.toolStripDownloadedLabel.Text = "Not Downloaded";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // resetSquareToolStripButton
            // 
            this.resetSquareToolStripButton.Enabled = false;
            this.resetSquareToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.resetSquareToolStripButton.Name = "resetSquareToolStripButton";
            this.resetSquareToolStripButton.Size = new System.Drawing.Size(89, 22);
            this.resetSquareToolStripButton.Text = "Reset Square";
            this.resetSquareToolStripButton.ToolTipText = "Reset Square";
            this.resetSquareToolStripButton.Click += new System.EventHandler(this.resetSquareToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // openImageFolderToolstripButton
            // 
            this.openImageFolderToolstripButton.Image = ((System.Drawing.Image)(resources.GetObject("openImageFolderToolstripButton.Image")));
            this.openImageFolderToolstripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openImageFolderToolstripButton.Name = "openImageFolderToolstripButton";
            this.openImageFolderToolstripButton.Size = new System.Drawing.Size(141, 22);
            this.openImageFolderToolstripButton.Text = "Open Image Folder";
            this.openImageFolderToolstripButton.Click += new System.EventHandler(this.openImageFolderToolstripButton_Click);
            // 
            // deleteImagesToolStripButton
            // 
            this.deleteImagesToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("deleteImagesToolStripButton.Image")));
            this.deleteImagesToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.deleteImagesToolStripButton.Name = "deleteImagesToolStripButton";
            this.deleteImagesToolStripButton.Size = new System.Drawing.Size(111, 22);
            this.deleteImagesToolStripButton.Text = "Delete Images";
            this.deleteImagesToolStripButton.Click += new System.EventHandler(this.deleteImagesToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openInGoogleMapsToolStripMenuItem,
            this.openInBingMApsToolStripMenuItem});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(188, 22);
            this.toolStripDropDownButton1.Text = "Open Grid Square In Map";
            // 
            // openInGoogleMapsToolStripMenuItem
            // 
            this.openInGoogleMapsToolStripMenuItem.Name = "openInGoogleMapsToolStripMenuItem";
            this.openInGoogleMapsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openInGoogleMapsToolStripMenuItem.Text = "Open In Google Maps";
            this.openInGoogleMapsToolStripMenuItem.Click += new System.EventHandler(this.openInGoogleMapsToolStripMenuItem_Click);
            // 
            // openInBingMApsToolStripMenuItem
            // 
            this.openInBingMApsToolStripMenuItem.Name = "openInBingMApsToolStripMenuItem";
            this.openInBingMApsToolStripMenuItem.Size = new System.Drawing.Size(206, 22);
            this.openInBingMApsToolStripMenuItem.Text = "Open In Bing Maps";
            this.openInBingMApsToolStripMenuItem.Click += new System.EventHandler(this.openInBingMApsToolStripMenuItem_Click);
            // 
            // progressTabPage
            // 
            this.progressTabPage.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.progressTabPage.Controls.Add(this.childTaskLabel);
            this.progressTabPage.Controls.Add(this.label7);
            this.progressTabPage.Controls.Add(this.groupBox1);
            this.progressTabPage.Controls.Add(this.label6);
            this.progressTabPage.Controls.Add(this.currentActionProgressBar);
            this.progressTabPage.Controls.Add(this.parentTaskLabel);
            this.progressTabPage.Location = new System.Drawing.Point(4, 26);
            this.progressTabPage.Name = "progressTabPage";
            this.progressTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.progressTabPage.Size = new System.Drawing.Size(1040, 730);
            this.progressTabPage.TabIndex = 1;
            this.progressTabPage.Text = "Progress";
            // 
            // childTaskLabel
            // 
            this.childTaskLabel.AutoSize = true;
            this.childTaskLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.childTaskLabel.Location = new System.Drawing.Point(88, 55);
            this.childTaskLabel.Name = "childTaskLabel";
            this.childTaskLabel.Size = new System.Drawing.Size(0, 17);
            this.childTaskLabel.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(13, 55);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 17);
            this.label7.TabIndex = 5;
            this.label7.Text = "Currently:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.downloadThreadProgress4);
            this.groupBox1.Controls.Add(this.downloadThreadProgress3);
            this.groupBox1.Controls.Add(this.downloadThreadProgress2);
            this.groupBox1.Controls.Add(this.downloadThreadProgress1);
            this.groupBox1.Location = new System.Drawing.Point(9, 154);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1025, 501);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Downloaders";
            // 
            // downloadThreadProgress4
            // 
            this.downloadThreadProgress4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadThreadProgress4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadThreadProgress4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.downloadThreadProgress4.Location = new System.Drawing.Point(7, 222);
            this.downloadThreadProgress4.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.downloadThreadProgress4.Name = "downloadThreadProgress4";
            this.downloadThreadProgress4.Size = new System.Drawing.Size(1012, 57);
            this.downloadThreadProgress4.TabIndex = 3;
            // 
            // downloadThreadProgress3
            // 
            this.downloadThreadProgress3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadThreadProgress3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadThreadProgress3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.downloadThreadProgress3.Location = new System.Drawing.Point(7, 155);
            this.downloadThreadProgress3.Margin = new System.Windows.Forms.Padding(3, 7, 3, 7);
            this.downloadThreadProgress3.Name = "downloadThreadProgress3";
            this.downloadThreadProgress3.Size = new System.Drawing.Size(1012, 59);
            this.downloadThreadProgress3.TabIndex = 2;
            // 
            // downloadThreadProgress2
            // 
            this.downloadThreadProgress2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadThreadProgress2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadThreadProgress2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.downloadThreadProgress2.Location = new System.Drawing.Point(7, 93);
            this.downloadThreadProgress2.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.downloadThreadProgress2.Name = "downloadThreadProgress2";
            this.downloadThreadProgress2.Size = new System.Drawing.Size(1012, 53);
            this.downloadThreadProgress2.TabIndex = 1;
            // 
            // downloadThreadProgress1
            // 
            this.downloadThreadProgress1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.downloadThreadProgress1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.downloadThreadProgress1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.downloadThreadProgress1.Location = new System.Drawing.Point(7, 31);
            this.downloadThreadProgress1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.downloadThreadProgress1.Name = "downloadThreadProgress1";
            this.downloadThreadProgress1.Size = new System.Drawing.Size(1012, 48);
            this.downloadThreadProgress1.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(147, 17);
            this.label6.TabIndex = 3;
            this.label6.Text = "Current Action Progress";
            // 
            // currentActionProgressBar
            // 
            this.currentActionProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentActionProgressBar.Location = new System.Drawing.Point(16, 120);
            this.currentActionProgressBar.Name = "currentActionProgressBar";
            this.currentActionProgressBar.Size = new System.Drawing.Size(1004, 18);
            this.currentActionProgressBar.TabIndex = 2;
            // 
            // parentTaskLabel
            // 
            this.parentTaskLabel.AutoSize = true;
            this.parentTaskLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.parentTaskLabel.Location = new System.Drawing.Point(12, 17);
            this.parentTaskLabel.Name = "parentTaskLabel";
            this.parentTaskLabel.Size = new System.Drawing.Size(276, 21);
            this.parentTaskLabel.TabIndex = 1;
            this.parentTaskLabel.Text = "Working On AFS2 Grid Square - of -";
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.logTextBox);
            this.tabPage5.Location = new System.Drawing.Point(4, 26);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(1040, 730);
            this.tabPage5.TabIndex = 2;
            this.tabPage5.Text = "Log";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // logTextBox
            // 
            this.logTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTextBox.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logTextBox.Location = new System.Drawing.Point(3, 3);
            this.logTextBox.Multiline = true;
            this.logTextBox.Name = "logTextBox";
            this.logTextBox.Size = new System.Drawing.Size(1034, 724);
            this.logTextBox.TabIndex = 0;
            // 
            // tabControl2
            // 
            this.tabControl2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tabControl2.Controls.Add(this.imagesTabPage);
            this.tabControl2.Controls.Add(this.terrainTabPage);
            this.tabControl2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl2.Location = new System.Drawing.Point(12, 45);
            this.tabControl2.Name = "tabControl2";
            this.tabControl2.SelectedIndex = 0;
            this.tabControl2.Size = new System.Drawing.Size(379, 655);
            this.tabControl2.TabIndex = 7;
            // 
            // imagesTabPage
            // 
            this.imagesTabPage.Controls.Add(this.generateAFS2LevelsHelpImage);
            this.imagesTabPage.Controls.Add(this.zoomLevelLabel);
            this.imagesTabPage.Controls.Add(this.zoomLevelTrackBar);
            this.imagesTabPage.Controls.Add(this.groupBox2);
            this.imagesTabPage.Controls.Add(this.label4);
            this.imagesTabPage.Controls.Add(this.afsLevelsCheckBoxList);
            this.imagesTabPage.Controls.Add(this.label3);
            this.imagesTabPage.Controls.Add(this.label2);
            this.imagesTabPage.Controls.Add(this.imageSourceComboBox);
            this.imagesTabPage.Location = new System.Drawing.Point(4, 26);
            this.imagesTabPage.Name = "imagesTabPage";
            this.imagesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.imagesTabPage.Size = new System.Drawing.Size(371, 625);
            this.imagesTabPage.TabIndex = 0;
            this.imagesTabPage.Text = "Images";
            this.imagesTabPage.UseVisualStyleBackColor = true;
            // 
            // generateAFS2LevelsHelpImage
            // 
            this.generateAFS2LevelsHelpImage.AutoSize = true;
            this.generateAFS2LevelsHelpImage.Image = ((System.Drawing.Image)(resources.GetObject("generateAFS2LevelsHelpImage.Image")));
            this.generateAFS2LevelsHelpImage.Location = new System.Drawing.Point(151, 135);
            this.generateAFS2LevelsHelpImage.Name = "generateAFS2LevelsHelpImage";
            this.generateAFS2LevelsHelpImage.Size = new System.Drawing.Size(16, 17);
            this.generateAFS2LevelsHelpImage.TabIndex = 10;
            this.generateAFS2LevelsHelpImage.Text = "  ";
            // 
            // zoomLevelLabel
            // 
            this.zoomLevelLabel.AutoSize = true;
            this.zoomLevelLabel.Location = new System.Drawing.Point(109, 65);
            this.zoomLevelLabel.Name = "zoomLevelLabel";
            this.zoomLevelLabel.Size = new System.Drawing.Size(22, 17);
            this.zoomLevelLabel.TabIndex = 9;
            this.zoomLevelLabel.Text = "16";
            // 
            // zoomLevelTrackBar
            // 
            this.zoomLevelTrackBar.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.zoomLevelTrackBar.LargeChange = 1;
            this.zoomLevelTrackBar.Location = new System.Drawing.Point(16, 88);
            this.zoomLevelTrackBar.Maximum = 20;
            this.zoomLevelTrackBar.Minimum = 8;
            this.zoomLevelTrackBar.Name = "zoomLevelTrackBar";
            this.zoomLevelTrackBar.Size = new System.Drawing.Size(337, 45);
            this.zoomLevelTrackBar.TabIndex = 8;
            this.zoomLevelTrackBar.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.zoomLevelTrackBar.Value = 8;
            this.zoomLevelTrackBar.Scroll += new System.EventHandler(this.zoomLevelTrackBar_Scroll);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.actionSetComboBox);
            this.groupBox2.Controls.Add(this.installSceneryIntoAFSCheckBox);
            this.groupBox2.Controls.Add(this.deleteStitchedImagesCheckBox);
            this.groupBox2.Controls.Add(this.runGeoConvertCheckBox);
            this.groupBox2.Controls.Add(this.generateAFSFilesCheckBox);
            this.groupBox2.Controls.Add(this.stitchImageTilesCheckBox);
            this.groupBox2.Controls.Add(this.downloadImageTileCheckBox);
            this.groupBox2.Location = new System.Drawing.Point(16, 334);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(339, 241);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Actions";
            // 
            // actionSetComboBox
            // 
            this.actionSetComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.actionSetComboBox.FormattingEnabled = true;
            this.actionSetComboBox.Items.AddRange(new object[] {
            "Run Default Actions",
            "Choose Actions To Run"});
            this.actionSetComboBox.Location = new System.Drawing.Point(19, 30);
            this.actionSetComboBox.Name = "actionSetComboBox";
            this.actionSetComboBox.Size = new System.Drawing.Size(299, 25);
            this.actionSetComboBox.TabIndex = 6;
            this.actionSetComboBox.SelectedIndexChanged += new System.EventHandler(this.actionSetComboBox_SelectedIndexChanged);
            // 
            // installSceneryIntoAFSCheckBox
            // 
            this.installSceneryIntoAFSCheckBox.AutoSize = true;
            this.installSceneryIntoAFSCheckBox.Enabled = false;
            this.installSceneryIntoAFSCheckBox.Location = new System.Drawing.Point(19, 203);
            this.installSceneryIntoAFSCheckBox.Name = "installSceneryIntoAFSCheckBox";
            this.installSceneryIntoAFSCheckBox.Size = new System.Drawing.Size(209, 21);
            this.installSceneryIntoAFSCheckBox.TabIndex = 5;
            this.installSceneryIntoAFSCheckBox.Text = "Ask To Install Scenery Into AFS2";
            this.installSceneryIntoAFSCheckBox.UseVisualStyleBackColor = true;
            this.installSceneryIntoAFSCheckBox.CheckedChanged += new System.EventHandler(this.installSceneryIntoAFSCheckBox_CheckedChanged);
            // 
            // deleteStitchedImagesCheckBox
            // 
            this.deleteStitchedImagesCheckBox.AutoSize = true;
            this.deleteStitchedImagesCheckBox.Enabled = false;
            this.deleteStitchedImagesCheckBox.Location = new System.Drawing.Point(19, 176);
            this.deleteStitchedImagesCheckBox.Name = "deleteStitchedImagesCheckBox";
            this.deleteStitchedImagesCheckBox.Size = new System.Drawing.Size(160, 21);
            this.deleteStitchedImagesCheckBox.TabIndex = 4;
            this.deleteStitchedImagesCheckBox.Text = "Delete Stitched Images";
            this.deleteStitchedImagesCheckBox.UseVisualStyleBackColor = true;
            this.deleteStitchedImagesCheckBox.CheckedChanged += new System.EventHandler(this.deleteStitchedImagesCheckBox_CheckedChanged);
            // 
            // runGeoConvertCheckBox
            // 
            this.runGeoConvertCheckBox.AutoSize = true;
            this.runGeoConvertCheckBox.Enabled = false;
            this.runGeoConvertCheckBox.Location = new System.Drawing.Point(19, 149);
            this.runGeoConvertCheckBox.Name = "runGeoConvertCheckBox";
            this.runGeoConvertCheckBox.Size = new System.Drawing.Size(122, 21);
            this.runGeoConvertCheckBox.TabIndex = 3;
            this.runGeoConvertCheckBox.Text = "Run GeoConvert";
            this.runGeoConvertCheckBox.UseVisualStyleBackColor = true;
            this.runGeoConvertCheckBox.CheckedChanged += new System.EventHandler(this.runGeoConvertCheckBox_CheckedChanged);
            // 
            // generateAFSFilesCheckBox
            // 
            this.generateAFSFilesCheckBox.AutoSize = true;
            this.generateAFSFilesCheckBox.Enabled = false;
            this.generateAFSFilesCheckBox.Location = new System.Drawing.Point(19, 122);
            this.generateAFSFilesCheckBox.Name = "generateAFSFilesCheckBox";
            this.generateAFSFilesCheckBox.Size = new System.Drawing.Size(173, 21);
            this.generateAFSFilesCheckBox.TabIndex = 2;
            this.generateAFSFilesCheckBox.Text = "Generate AID / TMC Files";
            this.generateAFSFilesCheckBox.UseVisualStyleBackColor = true;
            this.generateAFSFilesCheckBox.CheckedChanged += new System.EventHandler(this.generateAFSFilesCheckBox_CheckedChanged);
            // 
            // stitchImageTilesCheckBox
            // 
            this.stitchImageTilesCheckBox.AutoSize = true;
            this.stitchImageTilesCheckBox.Enabled = false;
            this.stitchImageTilesCheckBox.Location = new System.Drawing.Point(19, 95);
            this.stitchImageTilesCheckBox.Name = "stitchImageTilesCheckBox";
            this.stitchImageTilesCheckBox.Size = new System.Drawing.Size(128, 21);
            this.stitchImageTilesCheckBox.TabIndex = 1;
            this.stitchImageTilesCheckBox.Text = "Stitch Image Tiles";
            this.stitchImageTilesCheckBox.UseVisualStyleBackColor = true;
            this.stitchImageTilesCheckBox.CheckedChanged += new System.EventHandler(this.stitchImageTilesCheckBox_CheckedChanged);
            // 
            // downloadImageTileCheckBox
            // 
            this.downloadImageTileCheckBox.AutoSize = true;
            this.downloadImageTileCheckBox.Enabled = false;
            this.downloadImageTileCheckBox.Location = new System.Drawing.Point(19, 68);
            this.downloadImageTileCheckBox.Name = "downloadImageTileCheckBox";
            this.downloadImageTileCheckBox.Size = new System.Drawing.Size(156, 21);
            this.downloadImageTileCheckBox.TabIndex = 0;
            this.downloadImageTileCheckBox.Text = "Download Image Tiles";
            this.downloadImageTileCheckBox.UseVisualStyleBackColor = true;
            this.downloadImageTileCheckBox.CheckedChanged += new System.EventHandler(this.downloadImageTileCheckBox_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Generate AFS2 Levels";
            // 
            // afsLevelsCheckBoxList
            // 
            this.afsLevelsCheckBoxList.CheckOnClick = true;
            this.afsLevelsCheckBoxList.FormattingEnabled = true;
            this.afsLevelsCheckBoxList.Items.AddRange(new object[] {
            "Level 8",
            "Level 9",
            "Level 10",
            "Level 11",
            "Level 12",
            "Level 13",
            "Level 14",
            "Level 15"});
            this.afsLevelsCheckBoxList.Location = new System.Drawing.Point(16, 156);
            this.afsLevelsCheckBoxList.Name = "afsLevelsCheckBoxList";
            this.afsLevelsCheckBoxList.Size = new System.Drawing.Size(339, 164);
            this.afsLevelsCheckBoxList.TabIndex = 5;
            this.afsLevelsCheckBoxList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.gridSquareLevelsCheckBoxList_ItemCheck);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Zoom Level";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Image Source";
            // 
            // imageSourceComboBox
            // 
            this.imageSourceComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.imageSourceComboBox.FormattingEnabled = true;
            this.imageSourceComboBox.Items.AddRange(new object[] {
            "Bing",
            "Google",
            "USGS (Coming Soon)"});
            this.imageSourceComboBox.Location = new System.Drawing.Point(112, 19);
            this.imageSourceComboBox.Name = "imageSourceComboBox";
            this.imageSourceComboBox.Size = new System.Drawing.Size(243, 25);
            this.imageSourceComboBox.TabIndex = 1;
            this.imageSourceComboBox.SelectedIndexChanged += new System.EventHandler(this.imageSourceComboBox_SelectedIndexChanged);
            // 
            // terrainTabPage
            // 
            this.terrainTabPage.Controls.Add(this.label1);
            this.terrainTabPage.Location = new System.Drawing.Point(4, 26);
            this.terrainTabPage.Name = "terrainTabPage";
            this.terrainTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.terrainTabPage.Size = new System.Drawing.Size(371, 625);
            this.terrainTabPage.TabIndex = 1;
            this.terrainTabPage.Text = "Terrain";
            this.terrainTabPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(151, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Coming Soon";
            // 
            // startStopButton
            // 
            this.startStopButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.startStopButton.Enabled = false;
            this.startStopButton.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startStopButton.Location = new System.Drawing.Point(12, 742);
            this.startStopButton.Name = "startStopButton";
            this.startStopButton.Size = new System.Drawing.Size(379, 63);
            this.startStopButton.TabIndex = 3;
            this.startStopButton.Text = "Start";
            this.startStopButton.UseVisualStyleBackColor = true;
            this.startStopButton.Click += new System.EventHandler(this.ButtonStart_Click);
            // 
            // shutdownCheckbox
            // 
            this.shutdownCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.shutdownCheckbox.AutoSize = true;
            this.shutdownCheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.shutdownCheckbox.Location = new System.Drawing.Point(12, 713);
            this.shutdownCheckbox.Name = "shutdownCheckbox";
            this.shutdownCheckbox.Size = new System.Drawing.Size(223, 21);
            this.shutdownCheckbox.TabIndex = 8;
            this.shutdownCheckbox.Text = "Shut Down Computer When Done";
            this.shutdownCheckbox.UseVisualStyleBackColor = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "arrow_down.png");
            this.imageList1.Images.SetKeyName(1, "arrow_down_active.png");
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.Items.AddRange(new object[] {
            "Level 9 - Large",
            "Level 13 - Small",
            "Level 14 - Smallest"});
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(121, 32);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 32);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(85, 29);
            this.toolStripLabel2.Text = "Grid Squre Size";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1461, 843);
            this.Controls.Add(this.shutdownCheckbox);
            this.Controls.Add(this.tabControl2);
            this.Controls.Add(this.startStopButton);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "AeroScenery";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.mapTabPage.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.progressTabPage.ResumeLayout(false);
            this.progressTabPage.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.tabControl2.ResumeLayout(false);
            this.imagesTabPage.ResumeLayout(false);
            this.imagesTabPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.zoomLevelTrackBar)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.terrainTabPage.ResumeLayout(false);
            this.terrainTabPage.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GMap.NET.WindowsForms.GMapControl mainMap;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton settingsButton;
        private System.Windows.Forms.TabControl mainTabControl;
        private System.Windows.Forms.TabPage mapTabPage;
        private System.Windows.Forms.TabPage progressTabPage;
        private System.Windows.Forms.TabControl tabControl2;
        private System.Windows.Forms.TabPage imagesTabPage;
        private System.Windows.Forms.TabPage terrainTabPage;
        private System.Windows.Forms.Button startStopButton;
        private System.Windows.Forms.CheckBox downloadImageTileCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.ToolStripButton getSDKToolStripButton;
        private System.Windows.Forms.ProgressBar currentActionProgressBar;
        private System.Windows.Forms.Label parentTaskLabel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label childTaskLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.TextBox logTextBox;
        private UI.DownloadThreadProgressControl downloadThreadProgress2;
        private UI.DownloadThreadProgressControl downloadThreadProgress1;
        private UI.DownloadThreadProgressControl downloadThreadProgress4;
        private UI.DownloadThreadProgressControl downloadThreadProgress3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox runGeoConvertCheckBox;
        private System.Windows.Forms.CheckBox generateAFSFilesCheckBox;
        private System.Windows.Forms.CheckBox stitchImageTilesCheckBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckedListBox afsLevelsCheckBoxList;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox imageSourceComboBox;
        private System.Windows.Forms.CheckBox installSceneryIntoAFSCheckBox;
        private System.Windows.Forms.CheckBox deleteStitchedImagesCheckBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton deleteImagesToolStripButton;
        private System.Windows.Forms.ToolStripButton openImageFolderToolstripButton;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripButton resetSquareToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel gridSquareLabel;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripLabel toolStripDownloadedLabel;
        private System.Windows.Forms.ToolStripMenuItem openInGoogleMapsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openInBingMApsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ComboBox actionSetComboBox;
        private System.Windows.Forms.TrackBar zoomLevelTrackBar;
        private System.Windows.Forms.Label zoomLevelLabel;
        private System.Windows.Forms.Label generateAFS2LevelsHelpImage;
        private System.Windows.Forms.CheckBox shutdownCheckbox;
        private System.Windows.Forms.ToolStripStatusLabel statusStripLabel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
    }
}

