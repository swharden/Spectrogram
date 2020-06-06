using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArgoNot
{
    public partial class Form1 : Form
    {
        Spectrogram.SpectrogramLive spec;
        NAudio.Wave.WaveInEvent wvin;
        readonly List<WsprSpot> recentSpots = new List<WsprSpot>();

        public Form1()
        {
            InitializeComponent();
            cbColormap.SelectedIndex = 0;
            cbWindow.SelectedIndex = 1;
            cbResolution.SelectedIndex = 1;

            foreach (var band in WsprBands.GetBands())
                cbOffset.Items.Add($"{band.name}: {band.dialFreq:N0} Hz");
            cbOffset.SelectedIndex = 5;

            string devFilePath = @"C:\Users\Scott\Documents\temp\wsprTest\ALL_WSPR.TXT";
            if (File.Exists(devFilePath))
                wsprLogFilePath = devFilePath;
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
                spec.cmap = new Spectrogram.Colormaps.Viridis();
            else if (cbColormap.Text == "Grayscale")
                spec.cmap = new Spectrogram.Colormaps.Grayscale();
            else if (cbColormap.Text == "Plasma")
                spec.cmap = new Spectrogram.Colormaps.Plasma();
            else if (cbColormap.Text == "Argo")
                spec.cmap = new Spectrogram.Colormaps.Grayscale();
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

            int fftSize;
            if (cbResolution.SelectedIndex == 0)
                fftSize = 1 << 14; // 16384 points, 2.73 sec, 0.37 Hz/Px
            else
                fftSize = 1 << 13; // 8192 points, 1.37 sec, 0.73 Hz/Px

            int widthPx = 1000;
            int stepSize = sampleRate * 60 * 10 / widthPx; // 10 minutes
            double freqMin = 0;
            double freqMax = 2500;

            // create spectrogram
            spec = new Spectrogram.SpectrogramLive(sampleRate, fftSize, stepSize, widthPx, freqMax, freqMin);

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
            UpdateTics();

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

        private void UpdateTics()
        {
            if (spec is null || band is null)
                return;

            Bitmap bmp = new Bitmap(spec.Width + 140, spec.Height + 30);
            Ticks.DrawTicks(bmp, spec.Width, spec.Height, spec.fftFreqMin, spec.fftFreqMax, band.dialFreq);
            pbBackground.BackgroundImage?.Dispose();
            pbBackground.BackgroundImage = bmp;
            pbBackground.Size = bmp.Size;
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

            pbLevel.BackColor = ((double)pbLevel.Width / pnlAmplitude.Width > .9) ? Color.Crimson : Color.DarkSeaGreen;
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

                    if (cbSave.Checked)
                    {
                        string timeStamp = $"{t.Year:D2}{t.Month:D2}{t.Day:D2}{t.Hour:D2}{t.Minute:D2}";
                        spec.GetBitmap().Save($"{timeStamp}.bmp");
                    }
                }
            }
        }

        private void RenderTimer_Tick(object sender, EventArgs e)
        {
            spec.ProcessAll();

            Bitmap bmpSource = spec.GetBitmap();
            Bitmap bmp = new Bitmap(bmpSource.Width, bmpSource.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var labelFg = new SolidBrush(Color.White))
            using (var labelBg = new SolidBrush(Color.Black))
            using (var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold))
            using (var sfMiddleCenter = new StringFormat { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center })
            {
                gfx.DrawImage(bmpSource, 0, 0);

                // draw next index line
                pen.Color = Color.White;
                gfx.DrawLine(pen, spec.nextColumnIndex, 0, spec.nextColumnIndex, spec.Height);

                // draw WSPR band limits
                int bandMaxY = spec.PixelY(band.upperFreq - band.dialFreq);
                int bandMinY = spec.PixelY(band.lowerFreq - band.dialFreq);
                int qrssMinY = spec.PixelY(band.lowerFreq - band.dialFreq - 200);
                pen.Color = Color.White;
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                gfx.DrawLine(pen, 0, bandMaxY, spec.Width, bandMaxY);
                gfx.DrawLine(pen, 0, bandMinY, spec.Width, bandMinY);
                gfx.DrawLine(pen, 0, qrssMinY, spec.Width, qrssMinY);

                // draw WSPR calls
                for (int segment = 0; segment < 5; segment++)
                {
                    var segmentSpots = recentSpots
                        .Where(x => x.segment == segment)
                        .OrderBy(x => x.frequencyHz)
                        .Reverse()
                        .ToArray();

                    for (int i = 0; i < segmentSpots.Count(); i++)
                    {
                        WsprSpot spot = segmentSpots[i];
                        int x = bmp.Width / 5 * spot.segment;
                        int y = spec.PixelY(spot.frequencyHz - band.dialFreq);

                        // numbered marker
                        int r = 7;
                        int xNum = x + r * 2 * i + r;
                        gfx.FillEllipse(labelBg, xNum - r, y - r, r * 2, r * 2);
                        gfx.DrawString($"{i + 1}", font, labelFg, xNum, y, sfMiddleCenter);

                        // description with faux outline
                        int yLbl = (int)((i) * font.Size * 1.2) + bandMinY;
                        string lbl = $"{i + 1}: {spot.callsign} ({spot.strength} dB)";
                        gfx.DrawString(lbl, font, labelBg, x + 1, yLbl + 1);
                        gfx.DrawString(lbl, font, labelBg, x - 1, yLbl - 1);
                        gfx.DrawString(lbl, font, labelBg, x - 1, yLbl + 1);
                        gfx.DrawString(lbl, font, labelBg, x + 1, yLbl - 1);
                        gfx.DrawString(lbl, font, labelFg, x, yLbl);
                    }
                }

            }

            pbSpectrogram.Image?.Dispose();
            pbSpectrogram.Image = bmp;
        }

        string wsprLogFilePath = null;
        private void btnWsprPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog diag = new OpenFileDialog();
            diag.Filter = "WSPR Log Files (*.txt)|*.txt";
            diag.FileName = "ALL_WSPR.TXT";
            diag.Title = "Locate WSPR Log File";
            if (diag.ShowDialog() == DialogResult.OK)
            {
                wsprLogFilePath = diag.FileName;
                lblWspr.Text = "WSPR enabled";
            }
            else
            {
                wsprLogFilePath = null;
                lblWspr.Text = "WSPR disabled";
            }
        }

        int lastReadMinute = -1;
        private void timerWspr_Tick(object sender, EventArgs e)
        {
            if (wsprLogFilePath is null)
                return;

            // only read the log 5 seconds after a new minute
            if (DateTime.UtcNow.Minute == lastReadMinute || DateTime.UtcNow.Second < 5)
                return;
            else
                lastReadMinute = DateTime.UtcNow.Minute;

            var reader = new WsprLogWatcher(wsprLogFilePath);
            recentSpots.Clear();
            recentSpots.AddRange(reader.allSpots.Where(x => x.age < 10));
        }

        WsprBand band;
        private void cbOffset_SelectedIndexChanged(object sender, EventArgs e)
        {
            band = WsprBands.GetBands()[cbOffset.SelectedIndex];
            UpdateTics();
        }

        private void cbResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            StartListening();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var grab = new Grab(spec, band.lowerFreq - band.dialFreq - 200, band.upperFreq - band.dialFreq);
        }
    }
}
