namespace Spectrogram.Investigator
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
            this.btnSelectFile = new System.Windows.Forms.Button();
            this.cbFftSize = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudStepSize = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbWindow = new System.Windows.Forms.ComboBox();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.nudMaxFreq = new System.Windows.Forms.NumericUpDown();
            this.cbDecibels = new System.Windows.Forms.CheckBox();
            this.tbIntensity = new System.Windows.Forms.TrackBar();
            this.lblIntensity = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudStepSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxFreq)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensity)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(12, 12);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(89, 37);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "Select MP3";
            this.btnSelectFile.UseVisualStyleBackColor = true;
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // cbFftSize
            // 
            this.cbFftSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFftSize.FormattingEnabled = true;
            this.cbFftSize.Items.AddRange(new object[] {
            "512",
            "1024",
            "2048",
            "4096",
            "8192",
            "16384",
            "32768"});
            this.cbFftSize.Location = new System.Drawing.Point(110, 28);
            this.cbFftSize.Name = "cbFftSize";
            this.cbFftSize.Size = new System.Drawing.Size(121, 21);
            this.cbFftSize.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(107, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "FFT Size:";
            // 
            // nudStepSize
            // 
            this.nudStepSize.Location = new System.Drawing.Point(237, 29);
            this.nudStepSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudStepSize.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudStepSize.Name = "nudStepSize";
            this.nudStepSize.Size = new System.Drawing.Size(71, 20);
            this.nudStepSize.TabIndex = 3;
            this.nudStepSize.Value = new decimal(new int[] {
            500,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(234, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Window Step:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(314, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Window Shape:";
            // 
            // cbWindow
            // 
            this.cbWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWindow.FormattingEnabled = true;
            this.cbWindow.Items.AddRange(new object[] {
            "None",
            "Hanning",
            "Bartlett",
            "FlatTop"});
            this.cbWindow.Location = new System.Drawing.Point(314, 28);
            this.cbWindow.Name = "cbWindow";
            this.cbWindow.Size = new System.Drawing.Size(121, 21);
            this.cbWindow.TabIndex = 6;
            // 
            // btnCalculate
            // 
            this.btnCalculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCalculate.Enabled = false;
            this.btnCalculate.Location = new System.Drawing.Point(860, 12);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(89, 37);
            this.btnCalculate.TabIndex = 7;
            this.btnCalculate.Text = "Calculate";
            this.btnCalculate.UseVisualStyleBackColor = true;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Purple;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(281, 83);
            this.pictureBox1.TabIndex = 8;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Visible = false;
            this.pictureBox1.SizeChanged += new System.EventHandler(this.pictureBox1_SizeChanged);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(937, 504);
            this.panel1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(438, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(52, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Max kHz:";
            // 
            // nudMaxFreq
            // 
            this.nudMaxFreq.DecimalPlaces = 1;
            this.nudMaxFreq.Location = new System.Drawing.Point(441, 29);
            this.nudMaxFreq.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudMaxFreq.Name = "nudMaxFreq";
            this.nudMaxFreq.Size = new System.Drawing.Size(71, 20);
            this.nudMaxFreq.TabIndex = 11;
            this.nudMaxFreq.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cbDecibels
            // 
            this.cbDecibels.AutoSize = true;
            this.cbDecibels.Location = new System.Drawing.Point(518, 30);
            this.cbDecibels.Name = "cbDecibels";
            this.cbDecibels.Size = new System.Drawing.Size(39, 17);
            this.cbDecibels.TabIndex = 12;
            this.cbDecibels.Text = "dB";
            this.cbDecibels.UseVisualStyleBackColor = true;
            this.cbDecibels.CheckedChanged += new System.EventHandler(this.cbDecibels_CheckedChanged);
            // 
            // tbIntensity
            // 
            this.tbIntensity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbIntensity.AutoSize = false;
            this.tbIntensity.Location = new System.Drawing.Point(563, 25);
            this.tbIntensity.Maximum = 50;
            this.tbIntensity.Name = "tbIntensity";
            this.tbIntensity.Size = new System.Drawing.Size(291, 27);
            this.tbIntensity.TabIndex = 13;
            this.tbIntensity.Scroll += new System.EventHandler(this.tbIntensity_Scroll);
            // 
            // lblIntensity
            // 
            this.lblIntensity.AutoSize = true;
            this.lblIntensity.Location = new System.Drawing.Point(560, 12);
            this.lblIntensity.Name = "lblIntensity";
            this.lblIntensity.Size = new System.Drawing.Size(46, 13);
            this.lblIntensity.TabIndex = 14;
            this.lblIntensity.Text = "Intensity";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(961, 571);
            this.Controls.Add(this.lblIntensity);
            this.Controls.Add(this.cbDecibels);
            this.Controls.Add(this.tbIntensity);
            this.Controls.Add(this.nudMaxFreq);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnCalculate);
            this.Controls.Add(this.cbWindow);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudStepSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbFftSize);
            this.Controls.Add(this.btnSelectFile);
            this.Name = "Form1";
            this.Text = "Spectrogram Investigator";
            ((System.ComponentModel.ISupportInitialize)(this.nudStepSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxFreq)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbIntensity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSelectFile;
        private System.Windows.Forms.ComboBox cbFftSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudStepSize;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbWindow;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudMaxFreq;
        private System.Windows.Forms.CheckBox cbDecibels;
        private System.Windows.Forms.TrackBar tbIntensity;
        private System.Windows.Forms.Label lblIntensity;
    }
}

