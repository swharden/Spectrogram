using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Spectrogram
{
    public class Spectrogram
    {
        public int Width { get { return ffts.Count; } }
        public int Height { get { return settings.Height; } }

        private readonly Settings settings;
        private readonly List<double[]> ffts = new List<double[]>();
        private readonly List<double> newAudio = new List<double>();

        public Spectrogram(int sampleRate, int fftSize, int stepSize,
            double minFreq = 0, double maxFreq = double.PositiveInfinity)
        {
            settings = new Settings(sampleRate, fftSize, stepSize, minFreq, maxFreq);
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
                   $"window: {settings.FftLengthSec:N2} sec, " +
                   $"step: {settings.StepLengthSec:N2} sec, " +
                   $"overlap: {settings.StepOverlapFrac * 100:N0}%";
        }

        public void SetWindow(double[] newWindow)
        {
            if (newWindow.Length != settings.FftSize)
                throw new ArgumentException("window length must equal FFT size");

            for (int i = 0; i < settings.FftSize; i++)
                settings.Window[i] = newWindow[i];
        }

        public void Add(double[] audio)
        {
            newAudio.AddRange(audio);
        }

        public int FftsToProcess { get { return (newAudio.Count - settings.FftSize) / settings.StepSize; } }
        public int FftsProcessed { get; private set; }

        public void Process()
        {
            if (FftsToProcess < 1)
                return;

            int newFftCount = FftsToProcess;
            double[][] newFfts = new double[newFftCount][];

            Parallel.For(0, newFftCount, newFftIndex =>
            {
                Complex[] buffer = new Complex[settings.FftSize];
                int sourceIndex = newFftIndex * settings.StepSize;
                for (int i = 0; i < settings.FftSize; i++)
                    buffer[i] = new Complex(newAudio[sourceIndex + i] * settings.Window[i], 0);

                FftSharp.Transform.FFT(buffer);

                newFfts[newFftIndex] = new double[settings.Height];
                for (int i = 0; i < settings.Height; i++)
                    newFfts[newFftIndex][i] = buffer[settings.FftIndex1 + i].Magnitude / settings.FftSize;

                FftsProcessed += 1;
            });

            foreach (var newFft in newFfts)
                ffts.Add(newFft);

            newAudio.RemoveRange(0, newFftCount * settings.StepSize);
        }

        public Bitmap GetBitmap(double multiplier = 1)
        {
            if (Width == 0)
                return null;

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
                    double value = ffts[col][row] * multiplier;
                    value = Math.Min(value, 255);
                    int bytePosition = (Height - 1 - row) * stride + col;
                    bytes[bytePosition] = (byte)value;
                }
            }

            Marshal.Copy(bytes, 0, bitmapData.Scan0, bytes.Length);
            bmp.UnlockBits(bitmapData);

            return bmp;
        }

        public void TrimWidth(int maxWidth)
        {
            int overhang = Width - maxWidth;
            if (overhang > 0)
                ffts.RemoveRange(0, overhang);
        }
    }
}
