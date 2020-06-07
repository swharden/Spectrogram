namespace Spectrogram.MicrophoneDemo
{
    partial class FormQrssTest
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
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbDevice = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbScaleVert = new System.Windows.Forms.PictureBox();
            this.pbSpectrogram = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlAmpOuter = new System.Windows.Forms.Panel();
            this.pnlAmpInner = new System.Windows.Forms.Panel();
            this.nudBrightness = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbScaleVert)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrogram)).BeginInit();
            this.pnlAmpOuter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(164, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Brightness";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Device";
            // 
            // cbDevice
            // 
            this.cbDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDevice.FormattingEnabled = true;
            this.cbDevice.Location = new System.Drawing.Point(15, 25);
            this.cbDevice.Name = "cbDevice";
            this.cbDevice.Size = new System.Drawing.Size(146, 21);
            this.cbDevice.TabIndex = 7;
            this.cbDevice.SelectedIndexChanged += new System.EventHandler(this.cbDevice_SelectedIndexChanged);
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
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(977, 542);
            this.panel1.TabIndex = 11;
            // 
            // pbScaleVert
            // 
            this.pbScaleVert.BackColor = System.Drawing.Color.Navy;
            this.pbScaleVert.Location = new System.Drawing.Point(837, 0);
            this.pbScaleVert.Name = "pbScaleVert";
            this.pbScaleVert.Size = new System.Drawing.Size(107, 512);
            this.pbScaleVert.TabIndex = 3;
            this.pbScaleVert.TabStop = false;
            // 
            // pbSpectrogram
            // 
            this.pbSpectrogram.BackColor = System.Drawing.Color.Black;
            this.pbSpectrogram.Location = new System.Drawing.Point(0, 0);
            this.pbSpectrogram.Name = "pbSpectrogram";
            this.pbSpectrogram.Size = new System.Drawing.Size(837, 512);
            this.pbSpectrogram.TabIndex = 2;
            this.pbSpectrogram.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlAmpOuter
            // 
            this.pnlAmpOuter.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlAmpOuter.Controls.Add(this.pnlAmpInner);
            this.pnlAmpOuter.Location = new System.Drawing.Point(54, 11);
            this.pnlAmpOuter.Name = "pnlAmpOuter";
            this.pnlAmpOuter.Size = new System.Drawing.Size(107, 10);
            this.pnlAmpOuter.TabIndex = 13;
            // 
            // pnlAmpInner
            // 
            this.pnlAmpInner.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.pnlAmpInner.Location = new System.Drawing.Point(0, 0);
            this.pnlAmpInner.Name = "pnlAmpInner";
            this.pnlAmpInner.Size = new System.Drawing.Size(87, 10);
            this.pnlAmpInner.TabIndex = 15;
            // 
            // nudBrightness
            // 
            this.nudBrightness.DecimalPlaces = 1;
            this.nudBrightness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudBrightness.Location = new System.Drawing.Point(167, 26);
            this.nudBrightness.Name = "nudBrightness";
            this.nudBrightness.Size = new System.Drawing.Size(68, 20);
            this.nudBrightness.TabIndex = 14;
            this.nudBrightness.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // FormQrssTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(977, 594);
            this.Controls.Add(this.nudBrightness);
            this.Controls.Add(this.pnlAmpOuter);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbDevice);
            this.Name = "FormQrssTest";
            this.Text = "FormQrssTest";
            this.Load += new System.EventHandler(this.FormQrssTest_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbScaleVert)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrogram)).EndInit();
            this.pnlAmpOuter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbDevice;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pbScaleVert;
        private System.Windows.Forms.PictureBox pbSpectrogram;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel pnlAmpOuter;
        private System.Windows.Forms.NumericUpDown nudBrightness;
        private System.Windows.Forms.Panel pnlAmpInner;
    }
}