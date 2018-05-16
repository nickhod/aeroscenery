namespace AeroScenery.UI
{
    partial class DownloadThreadProgressControl
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
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.percentageLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.imageTileCountLabel = new System.Windows.Forms.Label();
            this.downloadThreadNumberLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(39, 3);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(460, 14);
            this.progressBar.TabIndex = 0;
            // 
            // percentageLabel
            // 
            this.percentageLabel.AutoSize = true;
            this.percentageLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.percentageLabel.Location = new System.Drawing.Point(39, 20);
            this.percentageLabel.Name = "percentageLabel";
            this.percentageLabel.Size = new System.Drawing.Size(26, 17);
            this.percentageLabel.TabIndex = 2;
            this.percentageLabel.Text = "0%";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(442, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "- kb/s";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.label2.Visible = false;
            // 
            // imageTileCountLabel
            // 
            this.imageTileCountLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.imageTileCountLabel.AutoSize = true;
            this.imageTileCountLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.imageTileCountLabel.Location = new System.Drawing.Point(247, 21);
            this.imageTileCountLabel.Name = "imageTileCountLabel";
            this.imageTileCountLabel.Size = new System.Drawing.Size(108, 17);
            this.imageTileCountLabel.TabIndex = 4;
            this.imageTileCountLabel.Text = "- of - Image Tiles";
            this.imageTileCountLabel.Click += new System.EventHandler(this.label3_Click);
            // 
            // downloadThreadNumberLabel
            // 
            this.downloadThreadNumberLabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.downloadThreadNumberLabel.Location = new System.Drawing.Point(3, 3);
            this.downloadThreadNumberLabel.Name = "downloadThreadNumberLabel";
            this.downloadThreadNumberLabel.Size = new System.Drawing.Size(30, 21);
            this.downloadThreadNumberLabel.TabIndex = 5;
            this.downloadThreadNumberLabel.Text = "1";
            this.downloadThreadNumberLabel.Click += new System.EventHandler(this.label4_Click);
            // 
            // DownloadThreadProgressControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Controls.Add(this.downloadThreadNumberLabel);
            this.Controls.Add(this.imageTileCountLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.percentageLabel);
            this.Controls.Add(this.progressBar);
            this.Name = "DownloadThreadProgressControl";
            this.Size = new System.Drawing.Size(586, 42);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label percentageLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label imageTileCountLabel;
        private System.Windows.Forms.Label downloadThreadNumberLabel;
    }
}
