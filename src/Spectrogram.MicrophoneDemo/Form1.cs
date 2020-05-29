using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            tbStepSize_Scroll(null, null);
            tbMultiply_Scroll(null, null);
            tbOffset_Scroll(null, null);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (NAudio.Wave.WaveIn.DeviceCount == 0)
            {
                MessageBox.Show("No audio input devices were found.\n\n" +
                    "This application will now exit.", "Audio Input Device ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            MessageBox.Show("Using the first audio input device:\n" +
                NAudio.Wave.WaveIn.GetCapabilities(0).ProductName, "Audio Input Device",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            StartAudioMonitor(0);
        }


        private SpectrogramLive spec;
        private NAudio.Wave.WaveInEvent wvin;

        private void StartAudioMonitor(int DeviceIndex, int sampleRate = 8000,
            int bitRate = 16, int channels = 1, int bufferMsec = 100, bool start = true)
        {
            spec = new SpectrogramLive(1024, sampleRate);

            if (wvin != null)
            {
                // shut down the old recorded if it exists
                wvin.StopRecording();
                wvin.Dispose();
            }

            wvin = new NAudio.Wave.WaveInEvent
            {
                DeviceNumber = DeviceIndex,
                WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bitRate, channels),
                BufferMilliseconds = bufferMsec
            };

            wvin.DataAvailable += OnDataAvailable;
            if (start)
                wvin.StartRecording();
        }

        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = args.BytesRecorded / bytesPerSample;
            double[] lastBuffer = new double[samplesRecorded];
            for (int i = 0; i < samplesRecorded; i++)
                lastBuffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
            spec.AddSignal(lastBuffer);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (wvin != null)
            {
                wvin.StopRecording();
                wvin.Dispose();
            }
        }

        private int stepSize = 200;
        private double multiplier = 1;
        private double offset = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            spec.ProcessAll(stepSize, multiplier, offset, dB: cbLog.Checked);
            spec.Trim(pictureBox1.Width);
            Bitmap bmp = spec.GetBitmap();

            pictureBox1.Image?.Dispose();
            pictureBox1.Image = bmp;
        }

        private void tbStepSize_Scroll(object sender, EventArgs e)
        {
            stepSize = tbStepSize.Value * 20;
            lblStepSize.Text = stepSize.ToString();
        }

        private void tbMultiply_Scroll(object sender, EventArgs e)
        {
            multiplier = tbMultiply.Value;
            lblMultiply.Text = multiplier.ToString();
        }

        private void tbOffset_Scroll(object sender, EventArgs e)
        {
            offset = tbOffset.Value;
            lblOffset.Text = offset.ToString();
        }
    }
}
