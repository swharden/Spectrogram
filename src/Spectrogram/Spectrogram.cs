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

        public void Add(double[] audio)
        {
            newAudio.AddRange(audio);
        }

        public void Process()
        {
            int fftsToProcess = (newAudio.Count - settings.FftSize) / settings.StepSize;
            if (fftsToProcess < 1)
                return;

            double[][] newFfts = new double[fftsToProcess][];

            Parallel.For(0, fftsToProcess, newFftIndex =>
            {
                Complex[] buffer = new Complex[settings.FftSize];
                int sourceIndex = newFftIndex * settings.StepSize;
                for (int i = 0; i < settings.FftSize; i++)
                    buffer[i] = new Complex(newAudio[sourceIndex + i] * settings.Window[i], 0);

                FftSharp.Transform.FFT(buffer);

                newFfts[newFftIndex] = new double[settings.Height];
                for (int i = 0; i < settings.Height; i++)
                    newFfts[newFftIndex][i] = buffer[settings.FftIndex1 + i].Magnitude / settings.FftSize;
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
