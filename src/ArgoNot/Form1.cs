using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArgoNot
{
    public partial class Form1 : Form
    {
        private Spectrogram.SpectrogramLive spec;
        private NAudio.Wave.WaveInEvent wvin;

        public Form1()
        {
            InitializeComponent();
            cbColormap.SelectedIndex = 0;
            cbWindow.SelectedIndex = 1;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (NAudio.Wave.WaveIn.DeviceCount == 0)
            {
                MessageBox.Show("No audio input devices found.\n\nThis program will now exit.",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }

            cbSoundCard.Items.Clear();
            for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                cbSoundCard.Items.Add(NAudio.Wave.WaveIn.GetCapabilities(i).ProductName);
            cbSoundCard.SelectedIndex = 0;
        }

        private void cbSoundCard_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartListening();
        }

        private void cbColormap_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            if (cbColormap.Text == "Viridis")
                new Spectrogram.Colormaps.Viridis().Apply(spec.GetBitmap());
            else if (cbColormap.Text == "Grayscale")
                new Spectrogram.Colormaps.Grayscale().Apply(spec.GetBitmap());
            else if (cbColormap.Text == "Plasma")
                new Spectrogram.Colormaps.Plasma().Apply(spec.GetBitmap());
            else if (cbColormap.Text == "Argo")
                new Spectrogram.Colormaps.Grayscale().Apply(spec.GetBitmap());
        }

        private void cbWindow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (spec is null)
                return;

            // TODO: build this into FFTsharp
            double[] rectangular = new double[spec.fftSize];
            for (int i = 0; i < rectangular.Length; i++)
                rectangular[i] = 1;

            if (cbWindow.Text == "Rectangular")
                spec.SetWindow(rectangular);
            else if (cbWindow.Text == "Hanning")
                spec.SetWindow(FftSharp.Window.Hanning(spec.fftSize));
            else if (cbWindow.Text == "Blackman")
                spec.SetWindow(FftSharp.Window.Blackman(spec.fftSize));
            else if (cbWindow.Text == "FlatTop")
                spec.SetWindow(FftSharp.Window.FlatTop(spec.fftSize));
        }

        private void nudBrightness_ValueChanged(object sender, EventArgs e)
        {
            spec.pixelMult = (double)nudBrightness.Value;
        }

        private void StartListening()
        {
            int sampleRate = 6000; // 48kHz / 8
            int fftSize = 1 << 14; // 16384 (2.7 sec)
            int widthPx = 1000;
            int stepSize = sampleRate * 60 * 10 / widthPx; // 10 minutes

            // create spectrogram
            spec = new Spectrogram.SpectrogramLive(sampleRate, fftSize, stepSize, widthPx,
                freqMin: 0, freqMax: 2500);

            // update settings based on GUI
            nudBrightness_ValueChanged(null, null);
            cbColormap_SelectedIndexChanged(null, null);
            cbWindow_SelectedIndexChanged(null, null);

            // move the marker forward to account for not starting at an even 10 minute mark
            double startTimeSec = (DateTime.UtcNow.Minute % 10) * 60 + DateTime.UtcNow.Second;
            int stepsToSkip = (int)(startTimeSec * sampleRate / stepSize);
            spec.SetNextIndex(stepsToSkip);

            // apply spectrogram Bitmap to picturebox
            pbSpectrogram.Image?.Dispose();
            pbSpectrogram.Image = spec.GetBitmap();
            pbSpectrogram.Size = pbSpectrogram.Image.Size;

            // start sound card listener
            wvin?.StopRecording();
            wvin?.Dispose();
            wvin = new NAudio.Wave.WaveInEvent
            {
                DeviceNumber = cbSoundCard.SelectedIndex,
                WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bits: 16, channels: 1),
                BufferMilliseconds = 20
            };
            wvin.DataAvailable += OnNewAudioData;
            wvin.StartRecording();
        }

        private static double amplitudeFrac = 0;
        private void OnNewAudioData(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = args.BytesRecorded / bytesPerSample;
            double[] buffer = new double[samplesRecorded];
            double peak = 0;
            for (int i = 0; i < samplesRecorded; i++)
            {
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);
                peak = Math.Max(peak, buffer[i]);
            }

            spec.Extend(buffer, process: false);
            amplitudeFrac = peak / (1 << 15);
        }

        private void LevelMeterTimer_Tick(object sender, EventArgs e)
        {
            // set audio level meter
            int newWidth = (int)(amplitudeFrac * pnlAmplitude.Width);
            if (newWidth < pbLevel.Width)
                pbLevel.Width -= 1;
            else
                pbLevel.Width = newWidth;
        }

        int lastSeenMinute = -1;
        private void ClockTimer_Tick(object sender, EventArgs e)
        {
            var t = DateTime.UtcNow;
            lblTime.Text = $"{t.Hour:D2}:{t.Minute:D2}:{t.Second:D2} UTC";

            bool isNewMinute = t.Minute != lastSeenMinute;
            if (isNewMinute)
            {
                lastSeenMinute = t.Minute;

                bool isTenMinuteRollover = t.Minute % 10 == 0;
                if (isTenMinuteRollover)
                {
                    spec.SetNextIndex(0);

                    string timeStamp = $"{t.Year:D2}{t.Month:D2}{t.Day:D2}{t.Hour:D2}{t.Minute:D2}";
                    spec.GetBitmap().Save($"{timeStamp}.bmp");
                }
            }
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            spec.ProcessAll();
            pbSpectrogram.Image?.Dispose();
            pbSpectrogram.Image = spec.GetBitmap(highlightIndex: true);
        }
    }
}
