using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaterfallDemo
{
    public partial class Waterfall : UserControl
    {
        private NAudio.Wave.WaveInEvent wvin;
        public Spectrogram.Spectrogram spec;
        bool renderNeeded;
        bool busyRendering;
        public float intensity = 2;

        public Waterfall()
        {
            InitializeComponent();
        }

        public void StartListening(int deviceIndex = 0, int sampleRate = 8000, int fftSize = 1024)
        {
            spec = new Spectrogram.Spectrogram(sampleRate, fftSize);

            int bitRate = 16;
            int channels = 1;
            int bufferMilliseconds = 10;

            wvin = new NAudio.Wave.WaveInEvent();
            wvin.DeviceNumber = deviceIndex;
            wvin.WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bitRate, channels);
            wvin.DataAvailable += OnDataAvailable;
            wvin.BufferMilliseconds = bufferMilliseconds;
            wvin.StartRecording();
        }

        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            float[] buffer = new float[args.BytesRecorded / bytesPerSample];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);

            try
            {
                spec.AddScroll(buffer, fixedSize: pictureBox1.Height);
                renderNeeded = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            if (!renderNeeded)
                return;

            if ((spec == null) || (spec.fftList.Count == 0))
                return;

            if (busyRendering)
                return;
            else
                busyRendering = true;

            pictureBox1.BackgroundImage = spec.GetBitmap(intensity: intensity, frequencyMax: 4000, vertical: true);
            renderNeeded = false;
            busyRendering = false;
        }
    }
}
