namespace QRSS_Viewer
{
    partial class FormMain
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wAVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frequencyRangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.offsetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFTResolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.brightnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colormapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutQRSSViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.timerInfo = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRange = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImageSize = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblFftInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.spectrogramViewer1 = new QRSS_Viewer.SpectrogramViewer();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.colormapToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1252, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // inputToolStripMenuItem
            // 
            this.inputToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.soundCardToolStripMenuItem,
            this.wAVFileToolStripMenuItem});
            this.inputToolStripMenuItem.Name = "inputToolStripMenuItem";
            this.inputToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.inputToolStripMenuItem.Text = "Input";
            // 
            // soundCardToolStripMenuItem
            // 
            this.soundCardToolStripMenuItem.Name = "soundCardToolStripMenuItem";
            this.soundCardToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.soundCardToolStripMenuItem.Text = "Sound Card";
            // 
            // wAVFileToolStripMenuItem
            // 
            this.wAVFileToolStripMenuItem.Name = "wAVFileToolStripMenuItem";
            this.wAVFileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.wAVFileToolStripMenuItem.Text = "WAV File";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frequencyRangeToolStripMenuItem,
            this.offsetToolStripMenuItem,
            this.fFTResolutionToolStripMenuItem,
            this.brightnessToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // frequencyRangeToolStripMenuItem
            // 
            this.frequencyRangeToolStripMenuItem.Name = "frequencyRangeToolStripMenuItem";
            this.frequencyRangeToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.frequencyRangeToolStripMenuItem.Text = "Frequency Range";
            // 
            // offsetToolStripMenuItem
            // 
            this.offsetToolStripMenuItem.Name = "offsetToolStripMenuItem";
            this.offsetToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.offsetToolStripMenuItem.Text = "Offset";
            // 
            // fFTResolutionToolStripMenuItem
            // 
            this.fFTResolutionToolStripMenuItem.Name = "fFTResolutionToolStripMenuItem";
            this.fFTResolutionToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.fFTResolutionToolStripMenuItem.Text = "FFT Resolution";
            // 
            // brightnessToolStripMenuItem
            // 
            this.brightnessToolStripMenuItem.Name = "brightnessToolStripMenuItem";
            this.brightnessToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.brightnessToolStripMenuItem.Text = "Brightness";
            this.brightnessToolStripMenuItem.Click += new System.EventHandler(this.BrightnessToolStripMenuItem_Click);
            // 
            // colormapToolStripMenuItem
            // 
            this.colormapToolStripMenuItem.Name = "colormapToolStripMenuItem";
            this.colormapToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.colormapToolStripMenuItem.Text = "Colormap";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutQRSSViewerToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutQRSSViewerToolStripMenuItem
            // 
            this.aboutQRSSViewerToolStripMenuItem.Name = "aboutQRSSViewerToolStripMenuItem";
            this.aboutQRSSViewerToolStripMenuItem.Size = new System.Drawing.Size(176, 22);
            this.aboutQRSSViewerToolStripMenuItem.Text = "About QRSS Viewer";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTime,
            this.lblRange,
            this.lblImageSize,
            this.lblFftInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 567);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1252, 24);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // timerInfo
            // 
            this.timerInfo.Enabled = true;
            this.timerInfo.Tick += new System.EventHandler(this.TimerInfo_Tick);
            // 
            // lblTime
            // 
            this.lblTime.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(139, 19);
            this.lblTime.Text = "2019-08-18 23:12:31 UTC";
            // 
            // lblRange
            // 
            this.lblRange.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblRange.Name = "lblRange";
            this.lblRange.Size = new System.Drawing.Size(120, 19);
            this.lblRange.Text = "Range: 900 - 1200 Hz";
            // 
            // lblImageSize
            // 
            this.lblImageSize.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblImageSize.Name = "lblImageSize";
            this.lblImageSize.Size = new System.Drawing.Size(102, 19);
            this.lblImageSize.Text = "Output: 1000x480";
            // 
            // lblFftInfo
            // 
            this.lblFftInfo.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.lblFftInfo.Name = "lblFftInfo";
            this.lblFftInfo.Size = new System.Drawing.Size(132, 19);
            this.lblFftInfo.Text = "FFT: 16,321 (1,523 step)";
            // 
            // spectrogramViewer1
            // 
            this.spectrogramViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.spectrogramViewer1.AutoScroll = true;
            this.spectrogramViewer1.Location = new System.Drawing.Point(0, 27);
            this.spectrogramViewer1.Margin = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.spectrogramViewer1.Name = "spectrogramViewer1";
            this.spectrogramViewer1.Size = new System.Drawing.Size(1252, 537);
            this.spectrogramViewer1.TabIndex = 2;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1252, 591);
            this.Controls.Add(this.spectrogramViewer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "QRSS Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem inputToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem soundCardToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wAVFileToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fFTResolutionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colormapToolStripMenuItem;
        private SpectrogramViewer spectrogramViewer1;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutQRSSViewerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frequencyRangeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem offsetToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem brightnessToolStripMenuItem;
        private System.Windows.Forms.Timer timerInfo;
        private System.Windows.Forms.ToolStripStatusLabel lblTime;
        private System.Windows.Forms.ToolStripStatusLabel lblRange;
        private System.Windows.Forms.ToolStripStatusLabel lblImageSize;
        private System.Windows.Forms.ToolStripStatusLabel lblFftInfo;
    }
}

