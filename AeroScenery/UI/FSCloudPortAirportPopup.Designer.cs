namespace AeroScenery.UI
{
    partial class FSCloudPortAirportPopup
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.nameLabel = new System.Windows.Forms.Label();
            this.runwaysLabel = new System.Windows.Forms.Label();
            this.buildingsLabel = new System.Windows.Forms.Label();
            this.aircraftLabel = new System.Windows.Forms.Label();
            this.lastUpdatedLabel = new System.Windows.Forms.Label();
            this.downloadButton = new System.Windows.Forms.Button();
            this.closeLabel = new System.Windows.Forms.Label();
            this.icaoLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            this.nameLabel.AutoEllipsis = true;
            this.nameLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nameLabel.Location = new System.Drawing.Point(10, 49);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(348, 30);
            this.nameLabel.TabIndex = 0;
            this.nameLabel.Text = "Wellesbourne Mountford Airfield";
            this.nameLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // runwaysLabel
            // 
            this.runwaysLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runwaysLabel.Location = new System.Drawing.Point(3, 0);
            this.runwaysLabel.Name = "runwaysLabel";
            this.runwaysLabel.Size = new System.Drawing.Size(106, 17);
            this.runwaysLabel.TabIndex = 1;
            this.runwaysLabel.Text = "999 Runways";
            this.runwaysLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // buildingsLabel
            // 
            this.buildingsLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buildingsLabel.Location = new System.Drawing.Point(115, 0);
            this.buildingsLabel.Name = "buildingsLabel";
            this.buildingsLabel.Size = new System.Drawing.Size(106, 17);
            this.buildingsLabel.TabIndex = 2;
            this.buildingsLabel.Text = "999 Buildings";
            this.buildingsLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // aircraftLabel
            // 
            this.aircraftLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aircraftLabel.Location = new System.Drawing.Point(227, 0);
            this.aircraftLabel.Name = "aircraftLabel";
            this.aircraftLabel.Size = new System.Drawing.Size(112, 17);
            this.aircraftLabel.TabIndex = 3;
            this.aircraftLabel.Text = "99 Static Aircraft";
            this.aircraftLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lastUpdatedLabel
            // 
            this.lastUpdatedLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lastUpdatedLabel.Location = new System.Drawing.Point(12, 172);
            this.lastUpdatedLabel.Name = "lastUpdatedLabel";
            this.lastUpdatedLabel.Size = new System.Drawing.Size(346, 19);
            this.lastUpdatedLabel.TabIndex = 4;
            this.lastUpdatedLabel.Text = "Last Updated: 22-Mar 2018 09:28";
            this.lastUpdatedLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // downloadButton
            // 
            this.downloadButton.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.downloadButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadButton.Location = new System.Drawing.Point(15, 127);
            this.downloadButton.Name = "downloadButton";
            this.downloadButton.Size = new System.Drawing.Size(343, 34);
            this.downloadButton.TabIndex = 5;
            this.downloadButton.Text = "View / Download";
            this.downloadButton.UseVisualStyleBackColor = true;
            this.downloadButton.Click += new System.EventHandler(this.DownloadButton_Click);
            // 
            // closeLabel
            // 
            this.closeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeLabel.AutoSize = true;
            this.closeLabel.BackColor = System.Drawing.Color.Transparent;
            this.closeLabel.Font = new System.Drawing.Font("Webdings", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.closeLabel.ForeColor = System.Drawing.Color.Gray;
            this.closeLabel.Location = new System.Drawing.Point(343, 3);
            this.closeLabel.Name = "closeLabel";
            this.closeLabel.Size = new System.Drawing.Size(30, 24);
            this.closeLabel.TabIndex = 6;
            this.closeLabel.Text = "r";
            this.closeLabel.Click += new System.EventHandler(this.CloseLabel_Click);
            // 
            // icaoLabel
            // 
            this.icaoLabel.BackColor = System.Drawing.Color.Transparent;
            this.icaoLabel.Font = new System.Drawing.Font("Segoe UI", 14.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.icaoLabel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.icaoLabel.Location = new System.Drawing.Point(15, 12);
            this.icaoLabel.Name = "icaoLabel";
            this.icaoLabel.Size = new System.Drawing.Size(343, 28);
            this.icaoLabel.TabIndex = 7;
            this.icaoLabel.Text = "FDTD";
            this.icaoLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.runwaysLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.buildingsLabel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.aircraftLabel, 2, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(15, 91);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(343, 23);
            this.tableLayoutPanel1.TabIndex = 8;
            // 
            // FSCloudPortAirportPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.closeLabel);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.icaoLabel);
            this.Controls.Add(this.downloadButton);
            this.Controls.Add(this.lastUpdatedLabel);
            this.Controls.Add(this.nameLabel);
            this.Name = "FSCloudPortAirportPopup";
            this.Size = new System.Drawing.Size(376, 201);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Label runwaysLabel;
        private System.Windows.Forms.Label buildingsLabel;
        private System.Windows.Forms.Label aircraftLabel;
        private System.Windows.Forms.Label lastUpdatedLabel;
        private System.Windows.Forms.Button downloadButton;
        private System.Windows.Forms.Label closeLabel;
        private System.Windows.Forms.Label icaoLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
