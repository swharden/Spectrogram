using System;
using System.Drawing;
using System.Windows.Forms;

namespace Spectrogram.MicrophoneDemo
{
    public partial class FormAnalysis : Form
    {
        public FormAnalysis()
        {
            InitializeComponent();

            if (NAudio.Wave.WaveIn.DeviceCount == 0)
            {
                MessageBox.Show(
                    "No audio input devices found.\n\nThis program will now exit.",
                    "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
            else
            {
                cbDevice.Items.Clear();
                for (int i = 0; i < NAudio.Wave.WaveIn.DeviceCount; i++)
                    cbDevice.Items.Add(NAudio.Wave.WaveIn.GetCapabilities(i).ProductName);
                cbDevice.SelectedIndex = 0;
            }
        }

        private void FormQrssTest_Load(object sender, EventArgs e) { }
        private void cbDevice_SelectedIndexChanged(object sender, EventArgs e) => StartListening();
        private void cb4x_CheckedChanged(object sender, EventArgs e) => StartListening();

        private Spectrogram spec;
        private Listener listener;
        private byte[,] pixelValues;
        private void StartListening()
        {
            int sampleRate = 6000;
            int fftSize = 1 << 13;
            int stepCount = 1000;
            int stepSize = sampleRate * 60 * 10 / stepCount;

            listener?.Dispose();
            listener = new Listener(cbDevice.SelectedIndex, sampleRate);
            spec = new Spectrogram(sampleRate, fftSize, stepSize, fixedWidth: stepCount);
            pixelValues = new byte[spec.Width, spec.Height];
            pbSpectrogram.Width = spec.Width;
            pbSpectrogram.Height = spec.Height;

            pbScaleVert.Image?.Dispose();
            pbScaleVert.Image = spec.GetVerticalScale(pbScaleVert.Width);
            pbScaleVert.Location = new Point(spec.Width, 0);
            pbScaleVert.Height = spec.Height;

            timer1.Enabled = true;
        }

        private int nextColIndex = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            double[] newAudio = listener.GetNewAudio();
            pnlAmpInner.Width = (int)(listener.AmplitudeFrac * pnlAmpOuter.Width);

            spec.Add(newAudio, process: false);
            double[][] newFfts = spec.Process();
            if (newFfts is null)
                return;

            foreach (double[] fft in newFfts)
            {
                for (int i = 0; i < fft.Length; i++)
                {
                    double value = fft[i] * (double)nudBrightness.Value;
                    value = Math.Min(value, 255);
                    pixelValues[nextColIndex, i] = (byte)value;
                }

                nextColIndex += 1;
                if (nextColIndex >= spec.Width)
                    nextColIndex -= spec.Width;
            }

            Bitmap bmp = new Bitmap(spec.Width, spec.Height);
            using (Bitmap bmpIndexed = Image.Create(pixelValues, nextColIndex))
            using (Graphics gfx = Graphics.FromImage(bmp))
            {
                Colormap.Viridis.Apply(bmpIndexed);
                gfx.DrawImage(bmpIndexed, 0, 0);
                gfx.DrawLine(Pens.White, nextColIndex, 0, nextColIndex, spec.Height);
            }
            pbSpectrogram.Image?.Dispose();
            pbSpectrogram.Image = bmp;
        }
    }
}
