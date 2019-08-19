using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QRSS_Viewer
{
    public partial class SpectrogramViewer : UserControl
    {
        private NAudio.Wave.WaveInEvent wvin;
        public Spectrogram.Spectrogram spec;
        bool busyRendering;

        public SpectrogramViewer()
        {
            InitializeComponent();
        }

        public void Start(int deviceIndex = 0, int fftPower = 14, string preLoadWavFile = null)
        {
            int sampleRate = 8000;
            int fftSize = (int)(Math.Pow(2, fftPower));
            Console.WriteLine($"FFT SIZE: 2^{fftPower} = {fftSize}");

            int tenMinutePixelWidth = 1000;
            int samplesInTenMinutes = sampleRate * 10 * 60;
            int segmentSize = samplesInTenMinutes / tenMinutePixelWidth;
            Console.WriteLine($"SEGMENT SIZE: {segmentSize}");

            spec = new Spectrogram.Spectrogram(sampleRate, fftSize, segmentSize);

            pbSpec.Width = tenMinutePixelWidth;
            pbSpec.Height = spec.displaySettings.height;

            int bitRate = 16;
            int channels = 1;
            int bufferMilliseconds = 20;

            if (preLoadWavFile == null)
            {
                wvin = new NAudio.Wave.WaveInEvent();
                wvin.DeviceNumber = deviceIndex;
                wvin.WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bitRate, channels);
                wvin.DataAvailable += OnDataAvailable;
                wvin.BufferMilliseconds = bufferMilliseconds;
                wvin.StartRecording();
            }
            else
            {
                float[] values = Spectrogram.WavFile.Read(preLoadWavFile);
                spec.AddExtend(values);
            }
        }

        public void SetIntensity(float intensity)
        {
            spec.displaySettings.brightness = intensity;
            spec.displaySettings.renderNeeded = true;
        }

        public double lastAmplitudeFrac = 0;
        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            float[] buffer = new float[args.BytesRecorded / bytesPerSample];
            for (int i = 0; i < buffer.Length; i++)
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
            lastAmplitudeFrac = (double)buffer.Max() / Int16.MaxValue;

            try
            {
                spec.AddCircular(buffer, fixedSize: pbSpec.Width);
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXCEPTION: " + ex);
            }
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {

            pbLevelMask.Height = (int)(panelLevel.Height * (1 - lastAmplitudeFrac));

            if ((spec == null) || (spec.fftList.Count == 0))
                return;

            if (!spec.displaySettings.renderNeeded)
                return;

            if (!busyRendering)
            {
                busyRendering = true;
                spec.displaySettings.renderNeeded = false;
                pbSpec.BackgroundImage = spec.GetBitmap();
                pbSpec.Size = pbSpec.BackgroundImage.Size;
                busyRendering = false;
            }
        }
    }
}
