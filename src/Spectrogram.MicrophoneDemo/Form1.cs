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

            if (spec.FftsToProcess > 0)
            {
                Stopwatch sw = Stopwatch.StartNew();
                spec.Process();
                spec.TrimWidth(pictureBox1.Width);
                Bitmap bmp = spec.GetBitmap();
                sw.Stop();
                pictureBox1.Image?.Dispose();
                pictureBox1.Image = bmp;
                lblStatus3.Text = $"Render time: {sw.ElapsedMilliseconds:D2} ms";
            }
            
            lblStatus1.Text = $"Time: {listener.TotalTimeSec:N3} sec";
            lblStatus2.Text = $"FFTs processed: {spec.FftsProcessed:N0}";
            pbAmplitude.Value = (int)(listener.AmplitudeFrac * pbAmplitude.Maximum);
        }
    }
}
