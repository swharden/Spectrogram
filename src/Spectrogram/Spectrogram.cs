using FftSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace Spectrogram
{
    public class Spectrogram
    {
        public int Width { get { return ffts.Count; } }
        public int Height { get { return settings.Height; } }

        private readonly Settings settings;
        private readonly List<double[]> ffts = new List<double[]>();
        private readonly List<double> newAudio = new List<double>();

        public Spectrogram(int sampleRate, int fftSize, int windowSize, int stepSize,
            double minFreq = 0, double maxFreq = double.PositiveInfinity)
        {
            settings = new Settings(sampleRate, fftSize, windowSize, stepSize, minFreq, maxFreq);
        }

        public override string ToString()
        {
            double processedSamples = ffts.Count * settings.StepSize + settings.FftSize;
            double processedSec = processedSamples / settings.SampleRate;
            string processedTime = (processedSec < 60) ? $"{processedSec:N2} sec" : $"{processedSec / 60.0:N2} min";

            return $"Spectrogram ({Width}, {Height})" +
                   $"\n  Vertical ({Height} px): " +
                   $"{settings.FreqMin:N0} - {settings.FreqMax:N0} Hz, " +
                   $"FFT size: {settings.FftSize:N0} samples, " +
                   $"{settings.HzPerPixel:N2} Hz/px" +
                   $"\n  Horizontal ({Width} px): " +
                   $"{processedTime}, " +
                   $"window: {settings.WindowLengthSec:N2} sec, " +
                   $"step: {settings.StepLengthSec:N2} sec, " +
                   $"overlap: {settings.StepOverlapFrac * 100:N0}%";
        }

        /// <summary>
        /// Add audio to the spectrogram (but don't process it yet)
        /// </summary>
        public void Add(double[] audio)
        {
            newAudio.AddRange(audio);
        }

        public bool isAnalysisNeeded { get { return FftsReadyToProcess() > 0; } }

        private int FftsReadyToProcess()
        {
            int fftsReady = (newAudio.Count - settings.WindowSize) / settings.StepSize;
            return Math.Max(fftsReady, 0);
        }

        /// <summary>
        /// Analyze unanalyzed audio
        /// </summary>
        public void Process()
        {
            int fftsToProcess = FftsReadyToProcess();

            double[][] newFfts = new double[fftsToProcess][];

            Parallel.For(0, fftsToProcess, newFftIndex =>
                {
                    // copy audio into complex buffer
                    Complex[] buffer = new Complex[settings.WindowSize];
                    int sourceIndex = newFftIndex * settings.StepSize;
                    for (int i = 0; i < settings.WindowSize; i++)
                        buffer[i] = new Complex(newAudio[sourceIndex] * settings.Window[i], 0);

                    // perform FFT
                    FftSharp.Transform.FFT(buffer);

                    // get magnitude just from the region of interest
                    newFfts[newFftIndex] = new double[settings.Height];
                    for (int i = 0; i < settings.Height; i++)
                        newFfts[newFftIndex][i] = buffer[settings.FftIndex1 + i].Magnitude;

                });

            foreach (var newFft in newFfts)
                ffts.Add(newFft);

            newAudio.RemoveRange(0, fftsToProcess * settings.StepSize);
        }

        public Bitmap GetBitmap()
        {
            Bitmap bmp = new Bitmap(Width, Height, PixelFormat.Format8bppIndexed);
            new Colormaps.Viridis().Apply(bmp);

            var lockRect = new Rectangle(0, 0, Width, Height);
            BitmapData bitmapData = bmp.LockBits(lockRect, ImageLockMode.ReadOnly, bmp.PixelFormat);
            int stride = bitmapData.Stride;

            byte[] bytes = new byte[bitmapData.Stride * bmp.Height];
            for (int col = 0; col < Width; col++)
            {
                for (int row = 0; row < Height; row++)
                {
                    double value = ffts[col][row] * (1 << 32);
                    value = Math.Min(value, 255);
                    int bytePosition = (Height - 1 - row) * stride + col;
                    bytes[bytePosition] = (byte)value;
                }
            }

            Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }
    }
}
