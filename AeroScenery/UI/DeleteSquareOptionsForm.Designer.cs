namespace AeroScenery.UI
{
    partial class DeleteSquareOptionsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeleteSquareOptionsForm));
            this.label1 = new System.Windows.Forms.Label();
            this.mapImageTilesCheckBox = new System.Windows.Forms.CheckBox();
            this.stitchedImagesCheckBox = new System.Windows.Forms.CheckBox();
            this.rawImagesCheckBox = new System.Windows.Forms.CheckBox();
            this.ttcFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.closeButton = new System.Windows.Forms.Button();
            this.allFilesCheckBox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(187, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "What would you like to delete?";
            // 
            // mapImageTilesCheckBox
            // 
            this.mapImageTilesCheckBox.AutoSize = true;
            this.mapImageTilesCheckBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mapImageTilesCheckBox.Location = new System.Drawing.Point(17, 117);
            this.mapImageTilesCheckBox.Name = "mapImageTilesCheckBox";
            this.mapImageTilesCheckBox.Size = new System.Drawing.Size(124, 21);
            this.mapImageTilesCheckBox.TabIndex = 1;
            this.mapImageTilesCheckBox.Text = "Map Image Tiles";
            this.mapImageTilesCheckBox.UseVisualStyleBackColor = true;
            this.mapImageTilesCheckBox.CheckedChanged += new System.EventHandler(this.mapImageTilesCheckBox_CheckedChanged);
            // 
            // stitchedImagesCheckBox
            // 
            this.stitchedImagesCheckBox.AutoSize = true;
            this.stitchedImagesCheckBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stitchedImagesCheckBox.Location = new System.Drawing.Point(17, 144);
            this.stitchedImagesCheckBox.Name = "stitchedImagesCheckBox";
            this.stitchedImagesCheckBox.Size = new System.Drawing.Size(119, 21);
            this.stitchedImagesCheckBox.TabIndex = 2;
            this.stitchedImagesCheckBox.Text = "Stitched Images";
            this.stitchedImagesCheckBox.UseVisualStyleBackColor = true;
            this.stitchedImagesCheckBox.CheckedChanged += new System.EventHandler(this.stitchedImagesCheckBox_CheckedChanged);
            // 
            // rawImagesCheckBox
            // 
            this.rawImagesCheckBox.AutoSize = true;
            this.rawImagesCheckBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rawImagesCheckBox.Location = new System.Drawing.Point(17, 171);
            this.rawImagesCheckBox.Name = "rawImagesCheckBox";
            this.rawImagesCheckBox.Size = new System.Drawing.Size(168, 21);
            this.rawImagesCheckBox.TabIndex = 3;
            this.rawImagesCheckBox.Text = "Geoconvert Raw Images";
            this.rawImagesCheckBox.UseVisualStyleBackColor = true;
            this.rawImagesCheckBox.CheckedChanged += new System.EventHandler(this.rawImagesCheckBox_CheckedChanged);
            // 
            // ttcFilesCheckBox
            // 
            this.ttcFilesCheckBox.AutoSize = true;
            this.ttcFilesCheckBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ttcFilesCheckBox.Location = new System.Drawing.Point(17, 198);
            this.ttcFilesCheckBox.Name = "ttcFilesCheckBox";
            this.ttcFilesCheckBox.Size = new System.Drawing.Size(77, 21);
            this.ttcFilesCheckBox.TabIndex = 4;
            this.ttcFilesCheckBox.Text = "TTC Files";
            this.ttcFilesCheckBox.UseVisualStyleBackColor = true;
            this.ttcFilesCheckBox.CheckedChanged += new System.EventHandler(this.ttcFilesCheckBox_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(14, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(333, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "(Nothing will be deleted from your AFS Scenery folders)";
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.okButton.Location = new System.Drawing.Point(209, 233);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(91, 32);
            this.okButton.TabIndex = 13;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // closeButton
            // 
            this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeButton.Location = new System.Drawing.Point(306, 233);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(96, 32);
            this.closeButton.TabIndex = 12;
            this.closeButton.Text = "Cancel";
            this.closeButton.UseVisualStyleBackColor = true;
            // 
            // allFilesCheckBox
            // 
            this.allFilesCheckBox.AutoSize = true;
            this.allFilesCheckBox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.allFilesCheckBox.Location = new System.Drawing.Point(17, 78);
            this.allFilesCheckBox.Name = "allFilesCheckBox";
            this.allFilesCheckBox.Size = new System.Drawing.Size(165, 21);
            this.allFilesCheckBox.TabIndex = 14;
            this.allFilesCheckBox.Text = "All Files For This Square";
            this.allFilesCheckBox.UseVisualStyleBackColor = true;
            this.allFilesCheckBox.CheckedChanged += new System.EventHandler(this.allFilesCheckBox_CheckedChanged);
            // 
            // DeleteSquareOptionsForm
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(414, 277);
            this.Controls.Add(this.allFilesCheckBox);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.closeButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ttcFilesCheckBox);
            this.Controls.Add(this.rawImagesCheckBox);
            this.Controls.Add(this.stitchedImagesCheckBox);
            this.Controls.Add(this.mapImageTilesCheckBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DeleteSquareOptionsForm";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Delete Grid Square Files";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox mapImageTilesCheckBox;
        private System.Windows.Forms.CheckBox stitchedImagesCheckBox;
        private System.Windows.Forms.CheckBox rawImagesCheckBox;
        private System.Windows.Forms.CheckBox ttcFilesCheckBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.CheckBox allFilesCheckBox;
    }
}