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
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnSetMicrophone_Click(object sender, EventArgs e)
        {
            AudioMonitorInitialize(DeviceIndex: cbMicrophones.SelectedIndex);
        }

        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            float[] buffer = new float[args.BytesRecorded / bytesPerSample];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
            spec.SignalExtend(buffer);
        }

        private void AudioMonitorInitialize(
            int DeviceIndex = 0,
            int sampleRate = 24000,
            int bitRate = 16,
            int channels = 1,
            int bufferMilliseconds = 10
            )
        {
            spec = new Spectrogram.Spectrogram(sampleRate);

            wvin = new NAudio.Wave.WaveInEvent();
            wvin.DeviceNumber = DeviceIndex;
            wvin.WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bitRate, channels);
            wvin.DataAvailable += OnDataAvailable;
            wvin.BufferMilliseconds = bufferMilliseconds;
            wvin.StartRecording();
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if ((spec != null) && (spec.ffts.Count > 0))
            {
                pictureBox1.BackgroundImage = spec.GetBitmap();
                lblStatus.Text = $"spectrogram has {spec.ffts.Count} FFT columns | last render: {spec.lastRenderMsec} ms";
            }
        }
    }
}
