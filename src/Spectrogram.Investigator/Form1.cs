using FftSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectrogram.Investigator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            cbFftSize.SelectedIndex = 3;
            cbWindow.SelectedIndex = 1;
            tbIntensity.Value = 7;
            tbIntensity_Scroll(null, null);

            string sampleFilePath = System.IO.Path.GetFullPath("../../../../data/cant-do-that.mp3");
            if (System.IO.File.Exists(sampleFilePath))
                LoadMp3(sampleFilePath);
        }

        Spectrogram spec;
        double[] audio;
        int sampleRate;
        private void LoadMp3(string filePath)
        {
            (audio, sampleRate) = Read.MP3(filePath);
            btnCalculate.Enabled = true;
            btnCalculate_Click(null, null);
        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "MP3 files (*.mp3)|*.mp3";
            diag.Filter += "|All files (*.*)|*.*";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                LoadMp3(diag.FileName);
            }
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            Application.DoEvents();

            spec = new Spectrogram(
                    signal: audio,
                    sampleRate: sampleRate,
                    fftSize: int.Parse(cbFftSize.Text),
                    stepSize: (int)nudStepSize.Value,
                    freqMax: (double)nudMaxFreq.Value * 1000,
                    freqMin: 0,
                    window: GetCustomWindow(),
                    multiplier: intensity,
                    offset: 0,
                    dB: cbDecibels.Checked
                );

            pictureBox1.Image = spec.GetBitmap();
            pictureBox1.Size = pictureBox1.Image.Size;
            pictureBox1.Visible = true;
            panel1.AutoScrollPosition = new Point(0, panel1.VerticalScroll.Maximum);
            this.Enabled = true;
        }

        private void pictureBox1_SizeChanged(object sender, EventArgs e)
        {
            panel1.AutoScrollMinSize = pictureBox1.Size;
        }

        double intensity;
        private void tbIntensity_Scroll(object sender, EventArgs e)
        {
            intensity = tbIntensity.Value / 5.0;
            lblIntensity.Text = $"Multiplier: {intensity:N1}";
            if (spec is null)
                return;
            spec.Recalculate(intensity, dB: cbDecibels.Checked);
            pictureBox1.Image = spec.GetBitmap();
        }

        private void cbDecibels_CheckedChanged(object sender, EventArgs e)
        {
            tbIntensity_Scroll(null, null);
        }

        private double[] GetCustomWindow()
        {
            if (cbWindow.Text == "Hanning")
                return FftSharp.Window.Hanning(int.Parse(cbFftSize.Text));

            if (cbWindow.Text == "Bartlett")
                return FftSharp.Window.Bartlett(int.Parse(cbFftSize.Text));

            if (cbWindow.Text == "FlatTop")
                return FftSharp.Window.FlatTop(int.Parse(cbFftSize.Text));

            double[] window = new double[int.Parse(cbFftSize.Text)];
            for (int i = 0; i < window.Length; i++)
                window[i] = 1;
            return window;
        }
    }
}
