namespace QRSS_Viewer
{
    partial class FormConfig
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
            this.nudBrightness = new System.Windows.Forms.NumericUpDown();
            this.btnAuto = new System.Windows.Forms.Button();
            this.trackBrightness = new System.Windows.Forms.TrackBar();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.nudUpper = new System.Windows.Forms.NumericUpDown();
            this.nudLower = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLower)).BeginInit();
            this.SuspendLayout();
            // 
            // nudBrightness
            // 
            this.nudBrightness.DecimalPlaces = 1;
            this.nudBrightness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudBrightness.Location = new System.Drawing.Point(6, 20);
            this.nudBrightness.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudBrightness.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.nudBrightness.Name = "nudBrightness";
            this.nudBrightness.Size = new System.Drawing.Size(68, 20);
            this.nudBrightness.TabIndex = 0;
            this.nudBrightness.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudBrightness.ValueChanged += new System.EventHandler(this.NudBrightness_ValueChanged);
            // 
            // btnAuto
            // 
            this.btnAuto.Location = new System.Drawing.Point(80, 19);
            this.btnAuto.Name = "btnAuto";
            this.btnAuto.Size = new System.Drawing.Size(68, 23);
            this.btnAuto.TabIndex = 1;
            this.btnAuto.Text = "Auto";
            this.btnAuto.UseVisualStyleBackColor = true;
            this.btnAuto.Click += new System.EventHandler(this.BtnAuto_Click);
            // 
            // trackBrightness
            // 
            this.trackBrightness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.trackBrightness.AutoSize = false;
            this.trackBrightness.Location = new System.Drawing.Point(154, 12);
            this.trackBrightness.Maximum = 100;
            this.trackBrightness.Minimum = 1;
            this.trackBrightness.Name = "trackBrightness";
            this.trackBrightness.Size = new System.Drawing.Size(456, 30);
            this.trackBrightness.TabIndex = 2;
            this.trackBrightness.Value = 10;
            this.trackBrightness.Scroll += new System.EventHandler(this.TrackBrightness_Scroll);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.nudBrightness);
            this.groupBox1.Controls.Add(this.trackBrightness);
            this.groupBox1.Controls.Add(this.btnAuto);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(616, 48);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Brightness";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.nudUpper);
            this.groupBox2.Controls.Add(this.nudLower);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(12, 66);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(156, 61);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Frequency Range (Hz)";
            // 
            // nudUpper
            // 
            this.nudUpper.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudUpper.Location = new System.Drawing.Point(81, 34);
            this.nudUpper.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudUpper.Name = "nudUpper";
            this.nudUpper.Size = new System.Drawing.Size(68, 20);
            this.nudUpper.TabIndex = 6;
            this.nudUpper.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudUpper.ValueChanged += new System.EventHandler(this.NudUpper_ValueChanged);
            // 
            // nudLower
            // 
            this.nudLower.Increment = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nudLower.Location = new System.Drawing.Point(6, 34);
            this.nudLower.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudLower.Name = "nudLower";
            this.nudLower.Size = new System.Drawing.Size(68, 20);
            this.nudLower.TabIndex = 5;
            this.nudLower.Value = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudLower.ValueChanged += new System.EventHandler(this.NudLower_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(78, 18);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Upper";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(36, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lower";
            // 
            // FormConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 268);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "FormConfig";
            this.Text = "Configuration";
            this.Load += new System.EventHandler(this.FormBrightness_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nudBrightness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBrightness)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUpper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudLower)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudBrightness;
        private System.Windows.Forms.Button btnAuto;
        private System.Windows.Forms.TrackBar trackBrightness;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudUpper;
        private System.Windows.Forms.NumericUpDown nudLower;
    }
}