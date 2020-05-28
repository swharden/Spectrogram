namespace Spectrogram.MicrophoneDemo
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.tbStepSize = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.lblStepSize = new System.Windows.Forms.Label();
            this.lblMultiply = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbMultiply = new System.Windows.Forms.TrackBar();
            this.lblOffset = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.tbOffset = new System.Windows.Forms.TrackBar();
            this.cbLog = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStepSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMultiply)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbOffset)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Purple;
            this.pictureBox1.Location = new System.Drawing.Point(12, 88);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(776, 359);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // tbStepSize
            // 
            this.tbStepSize.Location = new System.Drawing.Point(12, 33);
            this.tbStepSize.Maximum = 20;
            this.tbStepSize.Minimum = 1;
            this.tbStepSize.Name = "tbStepSize";
            this.tbStepSize.Size = new System.Drawing.Size(181, 45);
            this.tbStepSize.TabIndex = 1;
            this.tbStepSize.Value = 20;
            this.tbStepSize.Scroll += new System.EventHandler(this.tbStepSize_Scroll);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(181, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "Step Size";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblStepSize
            // 
            this.lblStepSize.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStepSize.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblStepSize.Location = new System.Drawing.Point(12, 62);
            this.lblStepSize.Name = "lblStepSize";
            this.lblStepSize.Size = new System.Drawing.Size(181, 23);
            this.lblStepSize.TabIndex = 3;
            this.lblStepSize.Text = "Step Size (points)";
            this.lblStepSize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblMultiply
            // 
            this.lblMultiply.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMultiply.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblMultiply.Location = new System.Drawing.Point(229, 62);
            this.lblMultiply.Name = "lblMultiply";
            this.lblMultiply.Size = new System.Drawing.Size(181, 23);
            this.lblMultiply.TabIndex = 6;
            this.lblMultiply.Text = "Step Size (points)";
            this.lblMultiply.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(229, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(181, 23);
            this.label3.TabIndex = 5;
            this.label3.Text = "Multiply";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbMultiply
            // 
            this.tbMultiply.Location = new System.Drawing.Point(229, 33);
            this.tbMultiply.Maximum = 20;
            this.tbMultiply.Name = "tbMultiply";
            this.tbMultiply.Size = new System.Drawing.Size(181, 45);
            this.tbMultiply.TabIndex = 4;
            this.tbMultiply.Value = 5;
            this.tbMultiply.Scroll += new System.EventHandler(this.tbMultiply_Scroll);
            // 
            // lblOffset
            // 
            this.lblOffset.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOffset.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.lblOffset.Location = new System.Drawing.Point(430, 62);
            this.lblOffset.Name = "lblOffset";
            this.lblOffset.Size = new System.Drawing.Size(181, 23);
            this.lblOffset.TabIndex = 9;
            this.lblOffset.Text = "Step Size (points)";
            this.lblOffset.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(430, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(181, 23);
            this.label5.TabIndex = 8;
            this.label5.Text = "Offset";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tbOffset
            // 
            this.tbOffset.Location = new System.Drawing.Point(430, 33);
            this.tbOffset.Maximum = 20;
            this.tbOffset.Name = "tbOffset";
            this.tbOffset.Size = new System.Drawing.Size(181, 45);
            this.tbOffset.TabIndex = 7;
            this.tbOffset.Scroll += new System.EventHandler(this.tbOffset_Scroll);
            // 
            // cbLog
            // 
            this.cbLog.AutoSize = true;
            this.cbLog.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbLog.Location = new System.Drawing.Point(633, 33);
            this.cbLog.Name = "cbLog";
            this.cbLog.Size = new System.Drawing.Size(55, 25);
            this.cbLog.TabIndex = 10;
            this.cbLog.Text = "Log";
            this.cbLog.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 459);
            this.Controls.Add(this.cbLog);
            this.Controls.Add(this.lblOffset);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tbOffset);
            this.Controls.Add(this.lblMultiply);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbMultiply);
            this.Controls.Add(this.lblStepSize);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbStepSize);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Form1";
            this.Text = "Spectrogram Microphone Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbStepSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbMultiply)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbOffset)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TrackBar tbStepSize;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblStepSize;
        private System.Windows.Forms.Label lblMultiply;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar tbMultiply;
        private System.Windows.Forms.Label lblOffset;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TrackBar tbOffset;
        private System.Windows.Forms.CheckBox cbLog;
    }
}

