using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRSS_Viewer
{
    public partial class FormMain : Form
    {
        FormConfig frmBright;

        public FormMain()
        {
            InitializeComponent();

            foreach (string audioDevice in Audio.GetInputDevices())
            {
                var item = new ToolStripMenuItem();
                item.Text = audioDevice;
                soundCardToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Application.DoEvents();

            //spectrogramViewer1.Start(preLoadWavFile: "qrss.wav");
            spectrogramViewer1.Start();

            spectrogramViewer1.spec.SetDisplayRange(1200, 1500);
            spectrogramViewer1.SetIntensity(5);

            frmBright = new FormConfig(spectrogramViewer1.spec.displaySettings);
        }

        private void BrightnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBright.Show();
        }

        private void TimerInfo_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy'-'MM'-'dd' 'HH':'mm':'ss");
            lblRange.Text = $"Range: {spectrogramViewer1.spec.displaySettings.freqLow} - {spectrogramViewer1.spec.displaySettings.freqHigh} Hz";
            lblImageSize.Text = $"Image: {spectrogramViewer1.spec.displaySettings.width} x {spectrogramViewer1.spec.displaySettings.height}";
            lblFftInfo.Text = $"FFT: {spectrogramViewer1.spec.fftSettings.fftSize} ({spectrogramViewer1.spec.fftSettings.step} step)";
        }
    }
}
