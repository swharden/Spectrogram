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
            int fftSize = 1 << 14; // 16384 (2.7 sec)
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

            // update ticks
            Bitmap bmpTicksV = DrawTicksVert(fftSize, sampleRate, freqMin, freqMax);
            pbTicksVert.Location = new Point(pbSpectrogram.Width, 0);
            pbTicksVert.Size = bmpTicksV.Size;
            pbTicksVert.Image?.Dispose();
            pbTicksVert.Image = bmpTicksV;

            // update ticks
            Bitmap bmpTicksH = DrawTicksHoriz(widthPx, durationSec: 60 * 10);
            pbTicksHoriz.Location = new Point(0, pbSpectrogram.Height);
            pbTicksHoriz.Size = bmpTicksH.Size;
            pbTicksHoriz.Image?.Dispose();
            pbTicksHoriz.Image = bmpTicksH;

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

        private Bitmap DrawTicksHoriz(int width, double durationSec)
        {
            Bitmap bmp = new Bitmap(pbSpectrogram.Width, 100);
            int tickSize = 5;
            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font(FontFamily.GenericMonospace, 10))
            using (var sf = new StringFormat { Alignment = StringAlignment.Center })
            {
                gfx.Clear(Color.White);
                for (int i = 0; i < durationSec; i += 60)
                {
                    if (i < 30 || durationSec - i < 30)
                        continue;

                    int xPx = (int)(width * i / durationSec);
                    xPx = pbSpectrogram.Width - xPx - 1;

                    gfx.DrawLine(pen, xPx, 0, xPx, tickSize);
                    gfx.DrawString($"{(durationSec - i) / 60:N0}", font, brush, xPx, tickSize, sf);
                }
            }
            return bmp;
        }

        private Bitmap DrawTicksVert(int fftSize, int sampleRate, double freqMin, double freqMax, double offset = 0, double tickSpacing = 25)
        {
            // TODO: move inside spectrogram class
            Bitmap bmp = new Bitmap(100, pbSpectrogram.Height);
            double maxFreq = sampleRate / 2;
            double hzPerPx = fftSize / 2 / maxFreq;
            int tickSize = 5;
            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var brush = new SolidBrush(Color.Black))
            using (var font = new Font(FontFamily.GenericMonospace, 10))
            using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
            {
                gfx.Clear(Color.White);
                for (double f = freqMin; f < freqMax; f += tickSpacing)
                {
                    int pxY = (int)((f - freqMin) * hzPerPx);

                    if (pxY < 10 || pbSpectrogram.Height - pxY < 10)
                        continue;

                    pxY = pbSpectrogram.Height - pxY;
                    gfx.DrawLine(pen, 0, pxY, tickSize, pxY);
                    gfx.DrawString($"{f + offset:N0}", font, brush, tickSize, pxY, sf);
                }
            }
            return bmp;
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

            Bitmap bmpSource = spec.GetBitmap(highlightIndex: true);
            Bitmap bmp = new Bitmap(bmpSource.Width, bmpSource.Height, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            using (var gfx = Graphics.FromImage(bmp))
            using (var pen = new Pen(Color.Black))
            using (var labelFg = new SolidBrush(Color.White))
            using (var labelBg = new SolidBrush(Color.Black))
            //using (var font = new Font("Segoe UI", 12))
            using (var font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold))
            using (var sf = new StringFormat { LineAlignment = StringAlignment.Center })
            {
                gfx.DrawImage(bmpSource, 0, 0);

                for (int i = 0; i < 5; i++)
                {
                    var segmentSpots = recentSpots
                        .Where(x => x.segment == i)
                        .OrderBy(x=>x.frequencyHz)
                        .Reverse();

                    int lastY = 0;
                    int minY = (int)font.Size;
                    Console.WriteLine();
                    foreach (WsprSpot spot in segmentSpots)
                    {
                        int x = bmp.Width / 5 * spot.segment;
                        int y = bmp.Height - spec.PixelY(spot.frequencyHz - 10_138_700);
                        Console.WriteLine(y);
                        if (y - lastY < minY)
                            y = lastY + minY;
                        lastY = y;
                        gfx.DrawString($"{spot.callsign} ({spot.strength} dB)", font, labelBg, x + 1, y + 1, sf);
                        gfx.DrawString($"{spot.callsign} ({spot.strength} dB)", font, labelBg, x - 1, y - 1, sf);
                        gfx.DrawString($"{spot.callsign} ({spot.strength} dB)", font, labelFg, x, y, sf);
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

        private void timerWspr_Tick(object sender, EventArgs e)
        {
            if (wsprLogFilePath is null)
                return;

            var reader = new WsprLogWatcher(wsprLogFilePath);
            recentSpots.Clear();
            recentSpots.AddRange(reader.allSpots.Where(x => x.age < 10));
        }
    }
}
