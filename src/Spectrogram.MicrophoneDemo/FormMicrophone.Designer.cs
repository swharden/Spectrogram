namespace Spectrogram.MicrophoneDemo
{
    partial class FormMicrophone
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
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pbSpectrogram = new System.Windows.Forms.PictureBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblStatus1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblStatus3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbAmplitude = new System.Windows.Forms.ToolStripProgressBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.tbBrightness = new System.Windows.Forms.TrackBar();
            this.cbDecibels = new System.Windows.Forms.CheckBox();
            this.cbRoll = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbScaleVert = new System.Windows.Forms.PictureBox();
            this.cbFftSize = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbColormap = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrogram)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScaleVert)).BeginInit();
            this.SuspendLayout();
            // 
            // cbDevice
            // 
            this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(12, 25);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(121, 21);
            this.cbDevice.TabIndex = 0;
            this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.cbDevice_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Device";
            // 
            // pbSpectrogram
            // 
            this.pbSpectrogram.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pbSpectrogram.BackColor = System.Drawing.Color.Black;
            this.pbSpectrogram.Location = new System.Drawing.Point(0, 0);
            this.pbSpectrogram.Name = "pbSpectrogram";
            this.pbSpectrogram.Size = new System.Drawing.Size(901, 512);
            this.pbSpectrogram.TabIndex = 2;
            this.pbSpectrogram.TabStop = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus1,
            this.lblStatus2,
            this.lblStatus3,
            this.pbAmplitude});
            this.statusStrip1.Location = new System.Drawing.Point(0, 567);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1032, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblStatus1
            // 
            this.lblStatus1.AutoSize = false;
            this.lblStatus1.Name = "lblStatus1";
            this.lblStatus1.Size = new System.Drawing.Size(200, 17);
            this.lblStatus1.Text = "toolStripStatusLabel1";
            this.lblStatus1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus2
            // 
            this.lblStatus2.AutoSize = false;
            this.lblStatus2.Name = "lblStatus2";
            this.lblStatus2.Size = new System.Drawing.Size(200, 17);
            this.lblStatus2.Text = "toolStripStatusLabel1";
            this.lblStatus2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStatus3
            // 
            this.lblStatus3.AutoSize = false;
            this.lblStatus3.Name = "lblStatus3";
            this.lblStatus3.Size = new System.Drawing.Size(415, 17);
            this.lblStatus3.Spring = true;
            this.lblStatus3.Text = "toolStripStatusLabel1";
            this.lblStatus3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pbAmplitude
            // 
            this.pbAmplitude.Name = "pbAmplitude";
            this.pbAmplitude.Size = new System.Drawing.Size(200, 16);
            this.pbAmplitude.Step = 1;
            this.pbAmplitude.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbAmplitude.Value = 25;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Brightness";
            // 
            // tbBrightness
            // 
            this.tbBrightness.Location = new System.Drawing.Point(393, 24);
            this.tbBrightness.Maximum = 100;
            this.tbBrightness.Name = "tbBrightness";
            this.tbBrightness.Size = new System.Drawing.Size(220, 45);
            this.tbBrightness.TabIndex = 6;
            this.tbBrightness.TickFrequency = 2;
            this.tbBrightness.Value = 5;
            // 
            // cbDecibels
            // 
            this.cbDecibels.AutoSize = true;
            this.cbDecibels.Location = new System.Drawing.Point(620, 28);
            this.cbDecibels.Name = "cbDecibels";
            this.cbDecibels.Size = new System.Drawing.Size(39, 17);
            this.cbDecibels.TabIndex = 7;
            this.cbDecibels.Text = "dB";
            this.cbDecibels.UseVisualStyleBackColor = true;
            // 
            // cbRoll
            // 
            this.cbRoll.AutoSize = true;
            this.cbRoll.Location = new System.Drawing.Point(665, 28);
            this.cbRoll.Name = "cbRoll";
            this.cbRoll.Size = new System.Drawing.Size(44, 17);
            this.cbRoll.TabIndex = 8;
            this.cbRoll.Text = "Roll";
            this.cbRoll.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.Controls.Add(this.pbScaleVert);
            this.panel1.Controls.Add(this.pbSpectrogram);
            this.panel1.Location = new System.Drawing.Point(12, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1008, 512);
            this.panel1.TabIndex = 10;
            // 
            // pbScaleVert
            // 
            this.pbScaleVert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pbScaleVert.BackColor = System.Drawing.Color.Navy;
            this.pbScaleVert.Location = new System.Drawing.Point(901, 0);
            this.pbScaleVert.Name = "pbScaleVert";
            this.pbScaleVert.Size = new System.Drawing.Size(107, 512);
            this.pbScaleVert.TabIndex = 3;
            this.pbScaleVert.TabStop = false;
            // 
            // cbFftSize
            // 
            this.cbFftSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFftSize.FormattingEnabled = true;
            this.cbFftSize.Location = new System.Drawing.Point(139, 25);
            this.cbFftSize.Name = "cbFftSize";
            this.cbFftSize.Size = new System.Drawing.Size(121, 21);
            this.cbFftSize.TabIndex = 11;
            this.cbFftSize.SelectedIndexChanged += new System.EventHandler(this.cbFftSize_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 12;
            this.label3.Text = "FFT Size";
            // 
            // cbColormap
            // 
            this.cbColormap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColormap.FormattingEnabled = true;
            this.cbColormap.Location = new System.Drawing.Point(266, 25);
            this.cbColormap.Name = "cbColormap";
            this.cbColormap.Size = new System.Drawing.Size(121, 21);
            this.cbColormap.TabIndex = 13;
            this.cbColormap.SelectedIndexChanged += new System.EventHandler(this.cbColormap_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(263, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(51, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Colormap";
            // 
            // FormMicrophone
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 589);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbColormap);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbFftSize);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cbRoll);
            this.Controls.Add(this.cbDecibels);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.tbBrightness);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDevice);
            this.Name = "FormMicrophone";
            this.Text = "Spectrogram Microphone Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrogram)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbBrightness)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbScaleVert)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pbSpectrogram;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus2;
        private System.Windows.Forms.ToolStripProgressBar pbAmplitude;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar tbBrightness;
        private System.Windows.Forms.CheckBox cbDecibels;
        private System.Windows.Forms.CheckBox cbRoll;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox cbFftSize;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pbScaleVert;
        private System.Windows.Forms.ComboBox cbColormap;
        private System.Windows.Forms.Label label4;
    }
}

