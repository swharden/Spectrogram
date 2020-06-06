using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;

namespace Spectrogram
{
    public class Spectrogram2
    {
        public int Width { get { return ffts.Count; } }
        public int Height { get { return settings.Height; } }

        private readonly Settings settings;
        private readonly List<double[]> ffts = new List<double[]>();
        private readonly List<double> newAudio = new List<double>();

        public Spectrogram2(int sampleRate, int fftSize, int windowSize, int stepSize,
            double minFreq = 0, double maxFreq = double.PositiveInfinity)
        {
            settings = new Settings(sampleRate, fftSize, windowSize, stepSize, minFreq, maxFreq);
        }

        public void Add(double[] audio)
        {
            newAudio.AddRange(audio);
        }

        public void Process()
        {
            int fftsToProcess = (newAudio.Count - settings.WindowSize) / settings.StepSize;

            for (int newFftIndex = 0; newFftIndex < fftsToProcess; newFftIndex++)
            {
                // copy audio into complex buffer
                Complex[] buffer = new Complex[settings.WindowSize];
                int sourceIndex = newFftIndex * settings.StepSize;
                for (int i = 0; i < settings.WindowSize; i++)
                    buffer[i] = new Complex(newAudio[sourceIndex] * settings.Window[i], 0);

                // perform FFT
                Console.WriteLine(buffer[100]);
                FftSharp.Transform.FFT(buffer);

                // get magnitude just from the region of interest
                var newFft = new double[settings.Height];
                for (int i = 0; i < settings.Height; i++)
                    newFft[i] = buffer[settings.FftIndex1 + i].Magnitude;
                ffts.Add(newFft);
            }
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
