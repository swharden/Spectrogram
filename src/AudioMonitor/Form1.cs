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
            spec.Add(buffer);
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
            switch (cbDisplay.Text)
            {
                case "waterfall":
                    spec = new Spectrogram.Spectrogram(sampleRate, fixedSize: pictureBox1.Height, scroll: true, vertical: true, pixelUpper: 200);
                    break;

                case "horizontal repeat":
                    spec = new Spectrogram.Spectrogram(sampleRate, fixedSize: pictureBox1.Width, scroll: false, pixelUpper: 200);
                    break;

                default:
                    throw new NotImplementedException("unknown display type");
            }
            nudIntensity.Value = (decimal)spec.intensity;

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

            pictureBox1.BackgroundImage = spec.GetBitmap();
            lblStatus.Text = $"spectrogram has {spec.ffts.Count} FFT columns | last render: {spec.lastRenderMsec} ms";
            renderNeeded = false;
        }

        private void NudIntensity_ValueChanged(object sender, EventArgs e)
        {
            spec.intensity = (float)nudIntensity.Value;
        }

        private void TbIntensity_Scroll(object sender, EventArgs e)
        {
            nudIntensity.Value = tbIntensity.Value;
        }

        private void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            spec.decibels = checkBox1.Checked;
        }
    }
}
