using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Spectrogram.MicrophoneDemo
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            pictureBox1.Location = new Point(0, 0);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            int[] deviceIndex = new int[1];
            new FormMicrophoneSelect(deviceIndex).ShowDialog();
            int selectedDevice = deviceIndex[0];

            if (selectedDevice >= 0)
            {
                StartAudioMonitor(selectedDevice);
            }
            else
            {
                MessageBox.Show(
                    "No audio input device was selected.\n\n" +
                    "This application will now exit.", "ERROR",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private SpectrogramLive spec;
        private NAudio.Wave.WaveInEvent wvin;

        private void StartAudioMonitor(int DeviceIndex, int sampleRate = 8000)
        {
            spec = new SpectrogramLive(sampleRate, fftSize: 1024, stepSize: 100, width: 600, freqMax: 2500);

            tbIntensity_Scroll(null, null);

            formsPlot1.plt.Clear();
            formsPlot1.plt.PlotSignal(spec.lastFft, spec.fftFreqSpacing);
            formsPlot1.plt.AxisAuto(0);
            formsPlot1.plt.Axis(y2: 255);
            formsPlot1.plt.Ticks(rulerModeX: true);
            formsPlot1.plt.XLabel("Frequency (Hz)");
            formsPlot1.plt.YLabel("Pixel Value");
            formsPlot1.Configure(middleClickMarginX: 0, middleClickMarginY: 0);

            if (wvin != null)
            {
                // shut down the old recorded if it exists
                wvin.StopRecording();
                wvin.Dispose();
            }

            wvin = new NAudio.Wave.WaveInEvent
            {
                DeviceNumber = DeviceIndex,
                WaveFormat = new NAudio.Wave.WaveFormat(sampleRate, bits: 16, channels: 1),
                BufferMilliseconds = 20
            };

            wvin.DataAvailable += OnDataAvailable;
            wvin.StartRecording();
        }

        private void OnDataAvailable(object sender, NAudio.Wave.WaveInEventArgs args)
        {
            int bytesPerSample = wvin.WaveFormat.BitsPerSample / 8;
            int samplesRecorded = args.BytesRecorded / bytesPerSample;
            double[] buffer = new double[samplesRecorded];
            for (int i = 0; i < samplesRecorded; i++)
                buffer[i] = BitConverter.ToInt16(args.Buffer, i * bytesPerSample);

            // process audio data as it comes in
            spec.Extend(buffer, process: true);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (spec is null || spec.isNewImageReady == false)
                return;

            Bitmap bmp = spec.GetBitmap();
            pictureBox1.Image?.Dispose();
            pictureBox1.Size = bmp.Size;
            pictureBox1.Image = bmp;
            panel1.AutoScrollMinSize = pictureBox1.Size;

            // find peak
            int peakIndex = 0;
            double peakValue = 0;
            for (int i = 0; i < spec.lastFft.Length; i++)
            {
                if (spec.lastFft[i] > peakValue)
                {
                    peakIndex = i;
                    peakValue = spec.lastFft[i];
                }
            }
            double peakFreq = peakIndex * spec.fftResolution;

            double noteNumber = 39.86 * Math.Log10(peakFreq / 440) + 49;

            lblStatus1.Text = 
                $"Width: {bmp.Width}px, Height: {bmp.Height} px, " +
                $"FFTs processed: {spec.fftsProcessed:N0}, " +
                $"Resolution (Hz): {spec.fftResolution:N2}, " +
                $"Peak (Hz): {peakFreq:N2} (Note {Math.Round(noteNumber)})";

            formsPlot1.Render();
        }

        private void tbIntensity_Scroll(object sender, EventArgs e)
        {
            spec.pixelMult = Math.Pow(tbIntensity.Value / 50f, 2);
        }

        private void cbDecibels_CheckedChanged(object sender, EventArgs e)
        {
            spec.dB = cbDecibels.Checked;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            formsPlot1.plt.AxisAuto(0);
            formsPlot1.plt.Axis(y1: 0, y2: 255);
        }
    }
}
