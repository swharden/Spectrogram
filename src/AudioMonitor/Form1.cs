using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AudioMonitor
{
    public partial class Form1 : Form
    {
        private NAudio.Wave.WaveInEvent wvin;
        private Spectrogram.Spectrogram spec;

        public Form1()
        {
            InitializeComponent();

            cbMicrophones.Items.AddRange(Listener.GetInputDevices());
            if (cbMicrophones.Items.Count > 0)
                cbMicrophones.SelectedItem = cbMicrophones.Items[0];

            cbDisplay.Items.Add("horizontal repeat");
            cbDisplay.Items.Add("waterfall");
            cbDisplay.SelectedItem = cbDisplay.Items[0];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            BtnSetMicrophone_Click(null, null);
        }

        private void BtnSetMicrophone_Click(object sender, EventArgs e)
        {
            if (btnSetMicrophone.Text == "open")
            {
                AudioMonitorInitialize(DeviceIndex: cbMicrophones.SelectedIndex);
                btnSetMicrophone.Text = "close";
            }
            else
            {
                btnSetMicrophone.Text = "open";
                wvin.StopRecording();
                wvin = null;
            }
        }

        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            float[] buffer = new float[args.BytesRecorded / bytesPerSample];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
            spec.Add(buffer, fixedSize: pictureBox1.Width);
            renderNeeded = true;
        }

        private void AudioMonitorInitialize(
            int DeviceIndex = 0,
            int sampleRate = 24000,
            int bitRate = 16,
            int channels = 1,
            int bufferMilliseconds = 10
            )
        {
            int fftSize = 1024;

            switch (cbDisplay.Text)
            {
                case "waterfall":
                    spec = new Spectrogram.Spectrogram(sampleRate, fftSize);
                    break;

                case "horizontal repeat":
                    spec = new Spectrogram.Spectrogram(sampleRate, fftSize);
                    break;

                default:
                    throw new NotImplementedException("unknown display type");
            }

            wvin = new NAudio.Wave.WaveInEvent();
            wvin.DeviceNumber = DeviceIndex;
            wvin.WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bitRate, channels);
            wvin.DataAvailable += OnDataAvailable;
            wvin.BufferMilliseconds = bufferMilliseconds;
            wvin.StartRecording();
        }

        bool renderNeeded = false;
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (!renderNeeded)
                return;

            if ((spec == null) || (spec.ffts.Count == 0))
                return;

            try
            {
                pictureBox1.BackgroundImage = spec.GetBitmap(
                    intensity: (float)nudIntensity.Value,
                    decibels: cbDecibels.Checked,
                    pixelLower: spec.GetFftIndex(0),
                    pixelUpper: spec.GetFftIndex(4000),
                    vertical: (cbDisplay.Text == "waterfall"),
                    scroll: (cbDisplay.Text == "waterfall")
                    );
                lblStatus.Text = $"spectrogram contains {spec.ffts.Count} FFT samples | last render: {spec.lastRenderMsec} ms";
                renderNeeded = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                lblStatus.Text = ex.ToString();
            }

        }

        private void TbIntensity_Scroll(object sender, EventArgs e)
        {
            nudIntensity.Value = tbIntensity.Value;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(spec.GetConfigDetails(), "Configuration Details");
        }
    }
}
