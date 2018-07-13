namespace AeroScenery.Controls
{
    partial class ContextMenuForButton
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
            this.components = new System.ComponentModel.Container();
            this.buttonColor = new System.Windows.Forms.Button();
            this.buttonCut = new System.Windows.Forms.Button();
            this.buttonCopy = new System.Windows.Forms.Button();
            this.buttonPaste = new System.Windows.Forms.Button();
            this.textBoxCaption = new System.Windows.Forms.TextBox();
            this.comboBoxBorder = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonColor
            // 
            this.buttonColor.BackColor = System.Drawing.SystemColors.ControlText;
            this.buttonColor.Location = new System.Drawing.Point(84, 173);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(115, 28);
            this.buttonColor.TabIndex = 19;
            this.buttonColor.UseVisualStyleBackColor = false;
            // 
            // buttonCut
            // 
            this.buttonCut.Location = new System.Drawing.Point(19, 42);
            this.buttonCut.Name = "buttonCut";
            this.buttonCut.Size = new System.Drawing.Size(57, 53);
            this.buttonCut.TabIndex = 11;
            this.buttonCut.Text = "Cu&t";
            this.buttonCut.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCut.UseVisualStyleBackColor = true;
            // 
            // buttonCopy
            // 
            this.buttonCopy.Location = new System.Drawing.Point(80, 42);
            this.buttonCopy.Name = "buttonCopy";
            this.buttonCopy.Size = new System.Drawing.Size(57, 53);
            this.buttonCopy.TabIndex = 12;
            this.buttonCopy.Text = "&Copy";
            this.buttonCopy.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonCopy.UseVisualStyleBackColor = true;
            // 
            // buttonPaste
            // 
            this.buttonPaste.Location = new System.Drawing.Point(142, 42);
            this.buttonPaste.Name = "buttonPaste";
            this.buttonPaste.Size = new System.Drawing.Size(57, 53);
            this.buttonPaste.TabIndex = 13;
            this.buttonPaste.Text = "&Paste";
            this.buttonPaste.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.buttonPaste.UseVisualStyleBackColor = true;
            // 
            // textBoxCaption
            // 
            this.textBoxCaption.Location = new System.Drawing.Point(84, 143);
            this.textBoxCaption.Name = "textBoxCaption";
            this.textBoxCaption.Size = new System.Drawing.Size(115, 22);
            this.textBoxCaption.TabIndex = 17;
            // 
            // comboBoxBorder
            // 
            this.comboBoxBorder.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxBorder.FormattingEnabled = true;
            this.comboBoxBorder.Items.AddRange(new object[] {
            "Flat",
            "Popup",
            "Standared",
            "System"});
            this.comboBoxBorder.Location = new System.Drawing.Point(84, 113);
            this.comboBoxBorder.Name = "comboBoxBorder";
            this.comboBoxBorder.Size = new System.Drawing.Size(115, 22);
            this.comboBoxBorder.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(206, 81);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(227, 84);
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(424, 23);
            this.label1.TabIndex = 10;
            this.label1.Text = "Button: Super Context Menu";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "Captio&n";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(19, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 14);
            this.label4.TabIndex = 14;
            this.label4.Text = "&Flat Style";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 180);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(34, 14);
            this.label3.TabIndex = 18;
            this.label3.Text = "C&olor";
            // 
            // ContextMenuForButton
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.comboBoxBorder);
            this.Controls.Add(this.textBoxCaption);
            this.Controls.Add(this.buttonPaste);
            this.Controls.Add(this.buttonCopy);
            this.Controls.Add(this.buttonCut);
            this.Controls.Add(this.buttonColor);
            this.Name = "ContextMenuForButton";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(444, 212);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Button buttonColor;
        public System.Windows.Forms.Button buttonCut;
        public System.Windows.Forms.Button buttonCopy;
        public System.Windows.Forms.Button buttonPaste;
        public System.Windows.Forms.TextBox textBoxCaption;
        public System.Windows.Forms.ComboBox comboBoxBorder;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolTip toolTip1;

    }
}
