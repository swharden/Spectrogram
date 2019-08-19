namespace QRSS_Viewer
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.inputToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.soundCardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wAVFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fFTResolutionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frequencyLimitsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colormapToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.spectrogramViewer1 = new QRSS_Viewer.SpectrogramViewer();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inputToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.colormapToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
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
            this.soundCardToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.soundCardToolStripMenuItem.Text = "Sound Card";
            // 
            // wAVFileToolStripMenuItem
            // 
            this.wAVFileToolStripMenuItem.Name = "wAVFileToolStripMenuItem";
            this.wAVFileToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
            this.wAVFileToolStripMenuItem.Text = "WAV File";
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fFTResolutionToolStripMenuItem,
            this.frequencyLimitsToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.configureToolStripMenuItem.Text = "Configure";
            // 
            // fFTResolutionToolStripMenuItem
            // 
            this.fFTResolutionToolStripMenuItem.Name = "fFTResolutionToolStripMenuItem";
            this.fFTResolutionToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.fFTResolutionToolStripMenuItem.Text = "FFT Resolution";
            // 
            // frequencyLimitsToolStripMenuItem
            // 
            this.frequencyLimitsToolStripMenuItem.Name = "frequencyLimitsToolStripMenuItem";
            this.frequencyLimitsToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.frequencyLimitsToolStripMenuItem.Text = "Frequency Limits";
            // 
            // colormapToolStripMenuItem
            // 
            this.colormapToolStripMenuItem.Name = "colormapToolStripMenuItem";
            this.colormapToolStripMenuItem.Size = new System.Drawing.Size(72, 20);
            this.colormapToolStripMenuItem.Text = "Colormap";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
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
            this.spectrogramViewer1.Size = new System.Drawing.Size(800, 398);
            this.spectrogramViewer1.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.spectrogramViewer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "QRSS Viewer";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem frequencyLimitsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colormapToolStripMenuItem;
        private SpectrogramViewer spectrogramViewer1;
    }
}

