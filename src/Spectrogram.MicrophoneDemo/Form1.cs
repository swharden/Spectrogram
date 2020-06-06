using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectrogram.MicrophoneDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            PopulateMicrophoneList();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private Spectrogram spec;
        private Listener listener;
        private void StartListening()
        {
            int sampleRate = 6000;
            int fftSize = 1 << 10;

            int columnsPerSecond = 50;
            int stepSize = sampleRate / columnsPerSecond;

            listener?.Dispose();
            listener = new Listener(cbDevice.SelectedIndex, sampleRate);
            spec = new Spectrogram(sampleRate, fftSize, stepSize);
            pictureBox1.Height = spec.Height;
        }

        private void PopulateMicrophoneList()
        {
            if (NAudio.Wave.WaveIn.DeviceCount == 0)
            {
                MessageBox.Show("No audio input devices found.\n\nThis program will now exit.",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            cbDevice.Items.Clear();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                cbDevice.Items.Add(NAudio.Wave.WaveIn.GetCapabilities(i).ProductName);
            cbDevice.SelectedIndex = 0;
        }

        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartListening();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double[] newAudio = listener.GetNewAudio();
            spec.Add(newAudio);

            double multiplier = tbBrightness.Value / 20.0;

            if (spec.FftsToProcess > 0)
            {
                Stopwatch sw = Stopwatch.StartNew();
                spec.Process();
                spec.TrimWidth(pictureBox1.Width);
                Bitmap bmp = spec.GetBitmap(multiplier, cbDecibels.Checked, cbRoll.Checked);
                Bitmap bmp2 = new Bitmap(spec.Width, spec.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (var gfx = Graphics.FromImage(bmp2))
                using (var pen = new Pen(Color.White))
                {
                    gfx.DrawImage(bmp, 0, 0);
                    if (cbRoll.Checked)
                    {
                        int x = spec.FftsProcessed % pictureBox1.Width - 1;
                        gfx.DrawLine(pen, x, 0, x, pictureBox1.Height);
                    }
                }
                bmp.Dispose();

                sw.Stop();
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = bmp2;
                lblStatus3.Text = $"Render time: {sw.ElapsedMilliseconds:D2} ms";
            }

            lblStatus1.Text = $"Time: {listener.TotalTimeSec:N3} sec";
            lblStatus2.Text = $"FFTs processed: {spec.FftsProcessed:N0}";
            pbAmplitude.Value = (int)(listener.AmplitudeFrac * pbAmplitude.Maximum);
        }
    }
}
