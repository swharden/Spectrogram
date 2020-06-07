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
    public partial class FormQrssTest : Form
    {
        public FormQrssTest()
        {
            InitializeComponent();

            if (NAudio.Wave.WaveIn.DeviceCount == 0)
            {
                MessageBox.Show("No audio input devices found.\n\nThis program will now exit.",
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

        private Spectrogram spec;
        private Listener listener;
        private void StartListening()
        {
            int sampleRate = 6000;
            int fftSize = 1 << 15;
            int stepSize = fftSize / 20;

            pbSpectrogram.Image?.Dispose();
            pbSpectrogram.Image = null;
            listener?.Dispose();
            listener = new Listener(cbDevice.SelectedIndex, sampleRate);
            spec = new Spectrogram(sampleRate, fftSize, stepSize);
            pbSpectrogram.Height = spec.Height;

            pbScaleVert.Image?.Dispose();
            pbScaleVert.Image = spec.GetVerticalScale(pbScaleVert.Width);
            pbScaleVert.Height = spec.Height;

            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            double[] newAudio = listener.GetNewAudio();
            spec.Add(newAudio);

            if (spec.FftsToProcess > 0)
            {
                Stopwatch sw = Stopwatch.StartNew();
                spec.Process();
                spec.MakeWidth(pbSpectrogram.Width);

                Bitmap bmpSpec = new Bitmap(spec.Width, spec.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                using (var bmpSpecIndexed = spec.GetBitmap((double)nudBrightness.Value, false, true))
                using (var gfx = Graphics.FromImage(bmpSpec))
                using (var pen = new Pen(Color.White))
                {
                    gfx.DrawImage(bmpSpecIndexed, 0, 0);
                    int x = spec.FftsProcessed % pbSpectrogram.Width - 1;
                    gfx.DrawLine(pen, x, 0, x, pbSpectrogram.Height);
                }

                sw.Stop();
                pbSpectrogram.Image?.Dispose();
                pbSpectrogram.Image = bmpSpec;
                Debug.WriteLine($"Render time: {sw.ElapsedMilliseconds:D2} ms");
            }

            pnlAmpInner.Width = (int)(listener.AmplitudeFrac * pnlAmpOuter.Width);
        }
    }
}
