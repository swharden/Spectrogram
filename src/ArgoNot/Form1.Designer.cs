namespace ArgoNot
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
            this.components = new System.ComponentModel.Container();
            this.pnlSpectrogram = new System.Windows.Forms.Panel();
            this.pbSpectrogram = new System.Windows.Forms.PictureBox();
            this.pbBackground = new System.Windows.Forms.PictureBox();
            this.cbColormap = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbWindow = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbSoundCard = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.pnlAmplitude = new System.Windows.Forms.Panel();
            this.pbLevel = new System.Windows.Forms.PictureBox();
            this.label6 = new System.Windows.Forms.Label();
            this.timerLevelMeter = new System.Windows.Forms.Timer(this.components);
            this.timerRollover = new System.Windows.Forms.Timer(this.components);
            this.timerRender = new System.Windows.Forms.Timer(this.components);
            this.nudBrightness = new System.Windows.Forms.NumericUpDown();
            this.lblTime = new System.Windows.Forms.Label();
            this.cbSave = new System.Windows.Forms.CheckBox();
            this.btnWsprPath = new System.Windows.Forms.Button();
            this.lblWspr = new System.Windows.Forms.Label();
            this.timerWspr = new System.Windows.Forms.Timer(this.components);
            this.cbOffset = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cbResolution = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.pnlSpectrogram.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrogram)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).BeginInit();
            this.pnlAmplitude.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSpectrogram
            // 
            this.pnlSpectrogram.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlSpectrogram.AutoScroll = true;
            this.pnlSpectrogram.BackColor = System.Drawing.Color.White;
            this.pnlSpectrogram.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSpectrogram.Controls.Add(this.pbSpectrogram);
            this.pnlSpectrogram.Controls.Add(this.pbBackground);
            this.pnlSpectrogram.Location = new System.Drawing.Point(12, 51);
            this.pnlSpectrogram.Name = "pnlSpectrogram";
            this.pnlSpectrogram.Size = new System.Drawing.Size(1165, 418);
            this.pnlSpectrogram.TabIndex = 2;
            // 
            // pbSpectrogram
            // 
            this.pbSpectrogram.BackColor = System.Drawing.Color.Navy;
            this.pbSpectrogram.Location = new System.Drawing.Point(0, 0);
            this.pbSpectrogram.Name = "pbSpectrogram";
            this.pbSpectrogram.Size = new System.Drawing.Size(100, 50);
            this.pbSpectrogram.TabIndex = 0;
            this.pbSpectrogram.TabStop = false;
            // 
            // pbBackground
            // 
            this.pbBackground.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pbBackground.Location = new System.Drawing.Point(0, 0);
            this.pbBackground.Name = "pbBackground";
            this.pbBackground.Size = new System.Drawing.Size(140, 106);
            this.pbBackground.TabIndex = 3;
            this.pbBackground.TabStop = false;
            // 
            // cbColormap
            // 
            this.cbColormap.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbColormap.FormattingEnabled = true;
            this.cbColormap.Items.AddRange(new object[] {
            "Viridis",
            "Grayscale",
            "Plasma",
            "Argo"});
            this.cbColormap.Location = new System.Drawing.Point(265, 24);
            this.cbColormap.Name = "cbColormap";
            this.cbColormap.Size = new System.Drawing.Size(121, 21);
            this.cbColormap.TabIndex = 4;
            this.cbColormap.SelectedIndexChanged += new System.EventHandler(this.cbColormap_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(262, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Colormap";
            // 
            // cbWindow
            // 
            this.cbWindow.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWindow.FormattingEnabled = true;
            this.cbWindow.Items.AddRange(new object[] {
            "Rectangular",
            "Hanning",
            "Blackman",
            "FlatTop"});
            this.cbWindow.Location = new System.Drawing.Point(392, 24);
            this.cbWindow.Name = "cbWindow";
            this.cbWindow.Size = new System.Drawing.Size(121, 21);
            this.cbWindow.TabIndex = 6;
            this.cbWindow.SelectedIndexChanged += new System.EventHandler(this.cbWindow_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(390, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Window";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Sound Card";
            // 
            // cbSoundCard
            // 
            this.cbSoundCard.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSoundCard.FormattingEnabled = true;
            this.cbSoundCard.Location = new System.Drawing.Point(12, 24);
            this.cbSoundCard.Name = "cbSoundCard";
            this.cbSoundCard.Size = new System.Drawing.Size(121, 21);
            this.cbSoundCard.TabIndex = 9;
            this.cbSoundCard.SelectedIndexChanged += new System.EventHandler(this.cbSoundCard_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(1073, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 21);
            this.label4.TabIndex = 10;
            this.label4.Text = "ArgoNot";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(516, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Brightness";
            // 
            // pnlAmplitude
            // 
            this.pnlAmplitude.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlAmplitude.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAmplitude.Controls.Add(this.pbLevel);
            this.pnlAmplitude.Location = new System.Drawing.Point(139, 24);
            this.pnlAmplitude.Name = "pnlAmplitude";
            this.pnlAmplitude.Size = new System.Drawing.Size(120, 21);
            this.pnlAmplitude.TabIndex = 14;
            // 
            // pbLevel
            // 
            this.pbLevel.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.pbLevel.Location = new System.Drawing.Point(0, 0);
            this.pbLevel.Name = "pbLevel";
            this.pbLevel.Size = new System.Drawing.Size(60, 21);
            this.pbLevel.TabIndex = 1;
            this.pbLevel.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(136, 8);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 15;
            this.label6.Text = "Audio Level";
            // 
            // timerLevelMeter
            // 
            this.timerLevelMeter.Enabled = true;
            this.timerLevelMeter.Interval = 10;
            this.timerLevelMeter.Tick += new System.EventHandler(this.LevelMeterTimer_Tick);
            // 
            // timerRollover
            // 
            this.timerRollover.Enabled = true;
            this.timerRollover.Interval = 500;
            this.timerRollover.Tick += new System.EventHandler(this.ClockTimer_Tick);
            // 
            // timerRender
            // 
            this.timerRender.Enabled = true;
            this.timerRender.Interval = 1000;
            this.timerRender.Tick += new System.EventHandler(this.RenderTimer_Tick);
            // 
            // nudBrightness
            // 
            this.nudBrightness.DecimalPlaces = 1;
            this.nudBrightness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudBrightness.Location = new System.Drawing.Point(519, 25);
            this.nudBrightness.Name = "nudBrightness";
            this.nudBrightness.Size = new System.Drawing.Size(69, 20);
            this.nudBrightness.TabIndex = 16;
            this.nudBrightness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBrightness.ValueChanged += new System.EventHandler(this.nudBrightness_ValueChanged);
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.Enabled = false;
            this.lblTime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTime.Location = new System.Drawing.Point(1069, 25);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(108, 21);
            this.lblTime.TabIndex = 17;
            this.lblTime.Text = "12:34:56 UTC";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cbSave
            // 
            this.cbSave.AutoSize = true;
            this.cbSave.Checked = true;
            this.cbSave.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbSave.Location = new System.Drawing.Point(802, 28);
            this.cbSave.Name = "cbSave";
            this.cbSave.Size = new System.Drawing.Size(51, 17);
            this.cbSave.TabIndex = 18;
            this.cbSave.Text = "Save";
            this.cbSave.UseVisualStyleBackColor = true;
            // 
            // btnWsprPath
            // 
            this.btnWsprPath.Location = new System.Drawing.Point(721, 23);
            this.btnWsprPath.Name = "btnWsprPath";
            this.btnWsprPath.Size = new System.Drawing.Size(75, 23);
            this.btnWsprPath.TabIndex = 19;
            this.btnWsprPath.Text = "Configure";
            this.btnWsprPath.UseVisualStyleBackColor = true;
            this.btnWsprPath.Click += new System.EventHandler(this.btnWsprPath_Click);
            // 
            // lblWspr
            // 
            this.lblWspr.AutoSize = true;
            this.lblWspr.Location = new System.Drawing.Point(718, 8);
            this.lblWspr.Name = "lblWspr";
            this.lblWspr.Size = new System.Drawing.Size(82, 13);
            this.lblWspr.TabIndex = 20;
            this.lblWspr.Text = "WSPR disabled";
            // 
            // timerWspr
            // 
            this.timerWspr.Enabled = true;
            this.timerWspr.Interval = 500;
            this.timerWspr.Tick += new System.EventHandler(this.timerWspr_Tick);
            // 
            // cbOffset
            // 
            this.cbOffset.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOffset.FormattingEnabled = true;
            this.cbOffset.Location = new System.Drawing.Point(594, 24);
            this.cbOffset.Name = "cbOffset";
            this.cbOffset.Size = new System.Drawing.Size(121, 21);
            this.cbOffset.TabIndex = 21;
            this.cbOffset.SelectedIndexChanged += new System.EventHandler(this.cbOffset_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(591, 8);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Dial Frequency";
            // 
            // cbResolution
            // 
            this.cbResolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbResolution.FormattingEnabled = true;
            this.cbResolution.Items.AddRange(new object[] {
            "0.37 Hz / pixel",
            "0.73 Hz / pixel"});
            this.cbResolution.Location = new System.Drawing.Point(859, 25);
            this.cbResolution.Name = "cbResolution";
            this.cbResolution.Size = new System.Drawing.Size(121, 21);
            this.cbResolution.TabIndex = 23;
            this.cbResolution.SelectedIndexChanged += new System.EventHandler(this.cbResolution_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(856, 8);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Resolution";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(992, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 25;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1189, 481);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlAmplitude);
            this.Controls.Add(this.pnlSpectrogram);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbResolution);
            this.Controls.Add(this.cbSave);
            this.Controls.Add(this.lblWspr);
            this.Controls.Add(this.btnWsprPath);
            this.Controls.Add(this.cbOffset);
            this.Controls.Add(this.nudBrightness);
            this.Controls.Add(this.cbSoundCard);
            this.Controls.Add(this.cbWindow);
            this.Controls.Add(this.cbColormap);
            this.Name = "Form1";
            this.Text = "ArgoNot";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlSpectrogram.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbSpectrogram)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbBackground)).EndInit();
            this.pnlAmplitude.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel pnlSpectrogram;
        private System.Windows.Forms.ComboBox cbColormap;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbWindow;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbSoundCard;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel pnlAmplitude;
        private System.Windows.Forms.PictureBox pbLevel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Timer timerLevelMeter;
        private System.Windows.Forms.Timer timerRollover;
        private System.Windows.Forms.Timer timerRender;
        private System.Windows.Forms.PictureBox pbSpectrogram;
        private System.Windows.Forms.NumericUpDown nudBrightness;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.CheckBox cbSave;
        private System.Windows.Forms.Button btnWsprPath;
        private System.Windows.Forms.Label lblWspr;
        private System.Windows.Forms.Timer timerWspr;
        private System.Windows.Forms.ComboBox cbOffset;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.PictureBox pbBackground;
        private System.Windows.Forms.ComboBox cbResolution;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
    }
}

