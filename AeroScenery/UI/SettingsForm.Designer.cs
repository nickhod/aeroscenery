namespace AeroScenery.UI
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.afsFolderButton = new System.Windows.Forms.Button();
            this.afsFolderTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.sdkButton = new System.Windows.Forms.Button();
            this.aerosceneryDatabaseFolderButton = new System.Windows.Forms.Button();
            this.workingFolderButton = new System.Windows.Forms.Button();
            this.afsSDKFolderTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.aeroSceneryDatabaseFolderTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.workingFolderTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.downloadWaitRandomTextBox = new System.Windows.Forms.TextBox();
            this.downloadWaitTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.simultaneousDownloadsComboBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.userAgentTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.okButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.afsFolderButton);
            this.groupBox1.Controls.Add(this.afsFolderTextBox);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.sdkButton);
            this.groupBox1.Controls.Add(this.aerosceneryDatabaseFolderButton);
            this.groupBox1.Controls.Add(this.workingFolderButton);
            this.groupBox1.Controls.Add(this.afsSDKFolderTextBox);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.aeroSceneryDatabaseFolderTextBox);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.workingFolderTextBox);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 170);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Folders";
            // 
            // afsFolderButton
            // 
            this.afsFolderButton.Location = new System.Drawing.Point(640, 123);
            this.afsFolderButton.Name = "afsFolderButton";
            this.afsFolderButton.Size = new System.Drawing.Size(33, 25);
            this.afsFolderButton.TabIndex = 11;
            this.afsFolderButton.Text = "...";
            this.afsFolderButton.UseVisualStyleBackColor = true;
            this.afsFolderButton.Click += new System.EventHandler(this.afsFolderButton_Click);
            // 
            // afsFolderTextBox
            // 
            this.afsFolderTextBox.Location = new System.Drawing.Point(216, 123);
            this.afsFolderTextBox.Name = "afsFolderTextBox";
            this.afsFolderTextBox.Size = new System.Drawing.Size(417, 25);
            this.afsFolderTextBox.TabIndex = 10;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(20, 126);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 17);
            this.label9.TabIndex = 9;
            this.label9.Text = "AFS2 Folder";
            // 
            // sdkButton
            // 
            this.sdkButton.Location = new System.Drawing.Point(640, 91);
            this.sdkButton.Name = "sdkButton";
            this.sdkButton.Size = new System.Drawing.Size(33, 25);
            this.sdkButton.TabIndex = 8;
            this.sdkButton.Text = "...";
            this.sdkButton.UseVisualStyleBackColor = true;
            this.sdkButton.Click += new System.EventHandler(this.sdkButton_Click);
            // 
            // aerosceneryDatabaseFolderButton
            // 
            this.aerosceneryDatabaseFolderButton.Location = new System.Drawing.Point(640, 60);
            this.aerosceneryDatabaseFolderButton.Name = "aerosceneryDatabaseFolderButton";
            this.aerosceneryDatabaseFolderButton.Size = new System.Drawing.Size(33, 25);
            this.aerosceneryDatabaseFolderButton.TabIndex = 7;
            this.aerosceneryDatabaseFolderButton.Text = "...";
            this.aerosceneryDatabaseFolderButton.UseVisualStyleBackColor = true;
            this.aerosceneryDatabaseFolderButton.Click += new System.EventHandler(this.aerosceneryDatabaseFolderButton_Click);
            // 
            // workingFolderButton
            // 
            this.workingFolderButton.Location = new System.Drawing.Point(640, 29);
            this.workingFolderButton.Name = "workingFolderButton";
            this.workingFolderButton.Size = new System.Drawing.Size(33, 25);
            this.workingFolderButton.TabIndex = 6;
            this.workingFolderButton.Text = "...";
            this.workingFolderButton.UseVisualStyleBackColor = true;
            this.workingFolderButton.Click += new System.EventHandler(this.workingFolderButton_Click);
            // 
            // afsSDKFolderTextBox
            // 
            this.afsSDKFolderTextBox.Location = new System.Drawing.Point(216, 91);
            this.afsSDKFolderTextBox.Name = "afsSDKFolderTextBox";
            this.afsSDKFolderTextBox.Size = new System.Drawing.Size(417, 25);
            this.afsSDKFolderTextBox.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(20, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "AFS2 SDK Folder";
            // 
            // aeroSceneryDatabaseFolderTextBox
            // 
            this.aeroSceneryDatabaseFolderTextBox.Location = new System.Drawing.Point(216, 60);
            this.aeroSceneryDatabaseFolderTextBox.Name = "aeroSceneryDatabaseFolderTextBox";
            this.aeroSceneryDatabaseFolderTextBox.Size = new System.Drawing.Size(417, 25);
            this.aeroSceneryDatabaseFolderTextBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(20, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "AeroScenery Database Folder";
            // 
            // workingFolderTextBox
            // 
            this.workingFolderTextBox.Location = new System.Drawing.Point(216, 29);
            this.workingFolderTextBox.Name = "workingFolderTextBox";
            this.workingFolderTextBox.Size = new System.Drawing.Size(417, 25);
            this.workingFolderTextBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Working Folder";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.downloadWaitRandomTextBox);
            this.groupBox2.Controls.Add(this.downloadWaitTextBox);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.simultaneousDownloadsComboBox);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.userAgentTextBox);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(12, 192);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(690, 145);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Downloads";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(574, 101);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 17);
            this.label8.TabIndex = 8;
            this.label8.Text = "ms";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(301, 101);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(25, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "ms";
            // 
            // downloadWaitRandomTextBox
            // 
            this.downloadWaitRandomTextBox.Location = new System.Drawing.Point(489, 98);
            this.downloadWaitRandomTextBox.Name = "downloadWaitRandomTextBox";
            this.downloadWaitRandomTextBox.Size = new System.Drawing.Size(79, 25);
            this.downloadWaitRandomTextBox.TabIndex = 6;
            // 
            // downloadWaitTextBox
            // 
            this.downloadWaitTextBox.Location = new System.Drawing.Point(216, 98);
            this.downloadWaitTextBox.Name = "downloadWaitTextBox";
            this.downloadWaitTextBox.Size = new System.Drawing.Size(79, 25);
            this.downloadWaitTextBox.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(20, 101);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(154, 17);
            this.label5.TabIndex = 4;
            this.label5.Text = "Wait Between Downloads";
            // 
            // simultaneousDownloadsComboBox
            // 
            this.simultaneousDownloadsComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.simultaneousDownloadsComboBox.FormattingEnabled = true;
            this.simultaneousDownloadsComboBox.Items.AddRange(new object[] {
            "4",
            "6",
            "8"});
            this.simultaneousDownloadsComboBox.Location = new System.Drawing.Point(216, 63);
            this.simultaneousDownloadsComboBox.Name = "simultaneousDownloadsComboBox";
            this.simultaneousDownloadsComboBox.Size = new System.Drawing.Size(417, 25);
            this.simultaneousDownloadsComboBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(20, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(154, 17);
            this.label4.TabIndex = 2;
            this.label4.Text = "Simultaneous Downloads";
            // 
            // userAgentTextBox
            // 
            this.userAgentTextBox.Location = new System.Drawing.Point(216, 29);
            this.userAgentTextBox.Name = "userAgentTextBox";
            this.userAgentTextBox.Size = new System.Drawing.Size(417, 25);
            this.userAgentTextBox.TabIndex = 1;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(20, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "User Agent";
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(606, 350);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(96, 32);
            this.closeButton.TabIndex = 10;
            this.closeButton.Text = "Cancel";
            this.closeButton.UseVisualStyleBackColor = true;
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // okButton
            // 
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(509, 350);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(91, 32);
            this.okButton.TabIndex = 11;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.HelpRequest += new System.EventHandler(this.folderBrowserDialog1_HelpRequest);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(350, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(127, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "randomize by + or -";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(714, 394);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.SettingsForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox workingFolderTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button sdkButton;
        private System.Windows.Forms.Button aerosceneryDatabaseFolderButton;
        private System.Windows.Forms.Button workingFolderButton;
        private System.Windows.Forms.TextBox afsSDKFolderTextBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox aeroSceneryDatabaseFolderTextBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox downloadWaitRandomTextBox;
        private System.Windows.Forms.TextBox downloadWaitTextBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox simultaneousDownloadsComboBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox userAgentTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button afsFolderButton;
        private System.Windows.Forms.TextBox afsFolderTextBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label10;
    }
}