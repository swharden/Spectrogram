namespace QRSS_Viewer
{
    partial class SpectrogramViewer
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
            this.pbSpec = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panelSpec = new System.Windows.Forms.Panel();
            this.panelLevel = new System.Windows.Forms.Panel();
            this.pbLevelMask = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpec)).BeginInit();
            this.panelSpec.SuspendLayout();
            this.panelLevel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelMask)).BeginInit();
            this.SuspendLayout();
            // 
            // pbSpec
            // 
            this.pbSpec.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pbSpec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.pbSpec.Location = new System.Drawing.Point(0, 0);
            this.pbSpec.Name = "pbSpec";
            this.pbSpec.Size = new System.Drawing.Size(322, 233);
            this.pbSpec.TabIndex = 0;
            this.pbSpec.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // panelSpec
            // 
            this.panelSpec.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelSpec.AutoScroll = true;
            this.panelSpec.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panelSpec.Controls.Add(this.pbSpec);
            this.panelSpec.Location = new System.Drawing.Point(11, 4);
            this.panelSpec.Name = "panelSpec";
            this.panelSpec.Size = new System.Drawing.Size(787, 376);
            this.panelSpec.TabIndex = 1;
            // 
            // panelLevel
            // 
            this.panelLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelLevel.BackColor = System.Drawing.Color.Red;
            this.panelLevel.Controls.Add(this.pbLevelMask);
            this.panelLevel.Location = new System.Drawing.Point(3, 4);
            this.panelLevel.Margin = new System.Windows.Forms.Padding(0);
            this.panelLevel.Name = "panelLevel";
            this.panelLevel.Size = new System.Drawing.Size(5, 376);
            this.panelLevel.TabIndex = 2;
            // 
            // pbLevelMask
            // 
            this.pbLevelMask.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pbLevelMask.Location = new System.Drawing.Point(0, 0);
            this.pbLevelMask.Name = "pbLevelMask";
            this.pbLevelMask.Size = new System.Drawing.Size(90, 200);
            this.pbLevelMask.TabIndex = 0;
            this.pbLevelMask.TabStop = false;
            // 
            // SpectrogramViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelLevel);
            this.Controls.Add(this.panelSpec);
            this.Name = "SpectrogramViewer";
            this.Size = new System.Drawing.Size(801, 383);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpec)).EndInit();
            this.panelSpec.ResumeLayout(false);
            this.panelLevel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLevelMask)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSpec;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panelSpec;
        private System.Windows.Forms.Panel panelLevel;
        private System.Windows.Forms.PictureBox pbLevelMask;
    }
}
