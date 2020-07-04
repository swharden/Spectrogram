namespace SffViewer
{
    partial class Form1
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
            this.lblFileName = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbColormap = new System.Windows.Forms.ComboBox();
            this.tbBrightness = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbDecibels = new System.Windows.Forms.CheckBox();
            this.cbStretch = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFileName
            // 
            this.lblFileName.AutoSize = true;
            this.lblFileName.Location = new System.Drawing.Point(12, 9);
            this.lblFileName.Name = "lblFileName";
            this.lblFileName.Size = new System.Drawing.Size(226, 13);
            this.lblFileName.TabIndex = 0;
            this.lblFileName.Text = "Load a SFF file by dragging it onto this window";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.SystemColors.Control;
            this.pictureBox1.Location = new System.Drawing.Point(12, 96);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(544, 279);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // cbColormap
            // 
            this.cbColormap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColormap.FormattingEnabled = true;
            this.cbColormap.Location = new System.Drawing.Point(15, 56);
            this.cbColormap.Name = "cbColormap";
            this.cbColormap.Size = new System.Drawing.Size(121, 21);
            this.cbColormap.TabIndex = 2;
            this.cbColormap.SelectedIndexChanged += new System.EventHandler(this.cbColormap_SelectedIndexChanged);
            // 
            // tbBrightness
            // 
            this.tbBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbBrightness.AutoSize = false;
            this.tbBrightness.Location = new System.Drawing.Point(274, 56);
            this.tbBrightness.Maximum = 50;
            this.tbBrightness.Name = "tbBrightness";
            this.tbBrightness.Size = new System.Drawing.Size(282, 34);
            this.tbBrightness.TabIndex = 3;
            this.tbBrightness.Value = 5;
            this.tbBrightness.Scroll += new System.EventHandler(this.tbBrightness_Scroll);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Colormap";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(271, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Brightness";
            // 
            // cbDecibels
            // 
            this.cbDecibels.AutoSize = true;
            this.cbDecibels.Location = new System.Drawing.Point(142, 59);
            this.cbDecibels.Name = "cbDecibels";
            this.cbDecibels.Size = new System.Drawing.Size(62, 17);
            this.cbDecibels.TabIndex = 7;
            this.cbDecibels.Text = "log (dB)";
            this.cbDecibels.UseVisualStyleBackColor = true;
            this.cbDecibels.CheckedChanged += new System.EventHandler(this.cbDecibels_CheckedChanged);
            // 
            // cbStretch
            // 
            this.cbStretch.AutoSize = true;
            this.cbStretch.Location = new System.Drawing.Point(210, 59);
            this.cbStretch.Name = "cbStretch";
            this.cbStretch.Size = new System.Drawing.Size(58, 17);
            this.cbStretch.TabIndex = 8;
            this.cbStretch.Text = "stretch";
            this.cbStretch.UseVisualStyleBackColor = true;
            this.cbStretch.CheckedChanged += new System.EventHandler(this.cbStretch_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(139, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Image Settings";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(568, 387);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbStretch);
            this.Controls.Add(this.cbDecibels);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tbBrightness);
            this.Controls.Add(this.cbColormap);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblFileName);
            this.Name = "Form1";
            this.Text = "SFF Viewer";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFileName;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbColormap;
        private System.Windows.Forms.TrackBar tbBrightness;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox cbDecibels;
        private System.Windows.Forms.CheckBox cbStretch;
        private System.Windows.Forms.Label label3;
    }
}

